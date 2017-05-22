using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Query;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// Controller for Monitoring the prio events with the help of MfIMonitor
    /// </summary>
    public class MfIControllerPrio0 : IPrio0Controller
    {
        private List<IServerObserver> observers = new List<IServerObserver>();
        private List<IEventObject> eventObjects = new List<IEventObject>();
        private List<IEventObject> helpEventObjectsNew = new List<IEventObject>();
        private List<IEventObject> helpEventObjectsOld = new List<IEventObject>();
        private QuerySQL querry;
        private Thread thread;
        private bool active = true;

        /// <summary>
        /// 
        /// </summary>
        public MfIControllerPrio0()
        {
            try
            {
                querry = QuerySQL.Instance(CreateSQLSetting());
            }
            catch (Exception ex)
            {
                LogFile.Log("Error beim Verbindungsaufbau mit der Datenbank: " + ex.Source + " " + ex.Message);
                active = false;
                throw ex;
            }
            
        }

        private SQLSetting CreateSQLSetting()
        {
            SQLSetting sqlSetting = new SQLSetting();

            sqlSetting.DataConnection = Options.DataConnection;
            sqlSetting.ServerNameColumn = Options.ServerNameColumn;
            sqlSetting.NumberOfPrio0Column = Options.NumberOfPrio0Column;
            sqlSetting.TimestampColumn = Options.TimestampColumn;
            sqlSetting.Database = Options.DatabaseTable;

            return sqlSetting;
        }



        /// <summary>
        /// Registers the obersver (WindowPrio)
        /// </summary>
        /// <param name="serverObserver"></param>
        public void RegisterObserver(IServerObserver serverObserver)
        {
            observers.Add(serverObserver);
        }

        /// <summary>
        /// Unregisters the obersver (WindowPrio)
        /// </summary>
        /// <param name="serverObserver"></param>
        public void UnregisterObserver(IServerObserver serverObserver)
        {
            observers.Remove(serverObserver);
        }

        private void NotifyPrioWindow()
        {
            foreach (IServerObserver ob in observers)
            {
                ob.ServerUpdate();
            }
        }

        private void PrioThread()
        {
            bool stopped = false;
            while (Options.RunThreads)
            {
                try
                {
                    Thread.Sleep(Options.CheckPrio0Time);

                    if (RefreshPrioStatus())
                    {
                        NotifyPrioWindow();
                        RefreshBackColorPanels();
                        stopped = true;
                    }
                    else if (stopped && eventObjects.Count == 0)
                    {
                        NotifyPrioWindow();
                        stopped = false;
                    }
                }
                catch (Exception ex)
                {
                    LogFile.Log("Error im MfIControllerPrio0-Thread: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                }
                
            }     
        }


        /// <summary>
        /// refreshs the backcolor of the serverPanels depending Prio0-Status
        /// </summary>
        public void RefreshBackColorPanels()
        {
            bool found = false;
            foreach (CapturePanel capturePanel in Program.CapturePanels)
            {
                foreach (IEventObject eventObject in eventObjects)
                {
                    if (capturePanel.Server.ServerName == eventObject.ServerName)
                    {
                        found = true;
                        break;
                    }
                }
                if (found && capturePanel.BackColor != Options.BackColorServerPanelByProblems)
                {
                    capturePanel.SetBackColor(Options.BackColorServerPanelByProblems);
                }
                else if (capturePanel.BackColor != capturePanel.BackColorNormal)
                {
                    capturePanel.SetBackColor(capturePanel.BackColorNormal);
                }
            }
        }

        /// <summary>
        /// Starts the thread for monitoring the prio events
        /// </summary>
        /// <returns></returns>
        public Thread StartThread()
        {
            RegisterObserver(WindowPrio.Instance);
            thread = new Thread(() => PrioThread());
            if (active)
            {
                thread.Start();
            }   
            return thread;
        }

        //Refreshs the status of prio
        private bool RefreshPrioStatus()
        {
            bool changed = false;

            try
            {
                helpEventObjectsNew.Clear();

                helpEventObjectsNew = querry.getPrio0();

                // check if they are already there and add if not
                foreach (IEventObject eventObject in helpEventObjectsNew)
                {
                    if (!CheckDuplicity(eventObject, eventObjects))
                    {
                        LogFile.Log("Anzahl Prio0 auf " + eventObject.ServerName + " neu detektiert: " + eventObject.Number);
                        changed = true;
                    }
                }

                // check if the notified events are still there and remove if not
                foreach (IEventObject eventObject in helpEventObjectsOld)
                {
                    if (!CheckDuplicity(eventObject, helpEventObjectsNew))
                    {
                        LogFile.Log("Anzahl Prio0 auf " + eventObject.ServerName + " neu detektiert: " + "0");
                        changed = true;
                    }
                }

                if (changed)
                {
                    eventObjects.Clear();
                    eventObjects.AddRange(helpEventObjectsNew);
                }

                helpEventObjectsOld.Clear();
                helpEventObjectsOld.AddRange(helpEventObjectsNew);
            }
            catch (Exception ex)
            {
                LogFile.Log("Error in MfIControllerPrio0.RefreshPrioStatus: " + ex.Source + " " + ex.Message);
            }

            return changed;
        }


        private bool CheckDuplicity(IEventObject eventObject, List<IEventObject> eventObjectss)
        {
            bool duplicate = false;
            if (eventObject == null)
            {
                return true;
            }

            foreach (IEventObject events in eventObjectss)
            {
                if (eventObject.ServerName == events.ServerName &&
                    eventObject.Number == events.Number)
                {
                    duplicate = true;
                    break;
                }
            }
            return duplicate;
        }

        private void AddEventObject(IEventObject eventObject)
        {
            eventObjects.Add(eventObject);
        }

        private void RemoveEventObject(IEventObject eventObject)
        {
            eventObjects.Remove(eventObject);
        }

        /// <summary>
        /// returns a list of EventObjects
        /// </summary>
        public List<IEventObject> EventObjects
        {
            get
            {
                return eventObjects;
            }
        }
    }
}
