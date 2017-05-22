using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Query;

namespace Dashboard
{
    /// <summary>
    /// Controller for Monitoring the prio events
    /// </summary>
    public class PrioController : IPrio0Controller
    {
        private List<IServerObserver> observers = new List<IServerObserver>();
        private List<IEventObject> eventObjects = new List<IEventObject>();
        private List<IEventObject> helpEventObjectsNew = new List<IEventObject>();
        private List<IEventObject> helpEventObjectsOld = new List<IEventObject>();

        private Thread thread;

        /// <summary>
        /// 
        /// </summary>
        public PrioController()
        {

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

        void PrioThread()
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
                    LogFile.Log("Error in PrioThread: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
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
            thread.Start();
            return thread;
        }

        //Refreshs the status of prio
        private bool RefreshPrioStatus()
        {
            bool changed = false;

            try
            {
                helpEventObjectsNew.Clear();

                // get all the new events
                foreach (Capture capture in Program.CaptureServer)
                {
                    if (!capture.ServerDisconect)
                    {
                        capture.CheckPrio0(Options.EventIDPrio0);
                        helpEventObjectsNew.AddRange(capture.GetEventObjects());
                    }
                }

                // check if they are already there and add if not
                foreach (IEventObject eventObject in helpEventObjectsNew)
                {
                    if (!CheckDuplicity(eventObject, eventObjects))
                    {
                        AddEventObject(eventObject);
                        LogFile.Log("Prio0 auf " + eventObject.ServerName + " detektiert: " + eventObject.Message);
                        changed = true;
                    }
                }

                // check if the notified events are still there and remove if not
                foreach (EventObject eventObject in helpEventObjectsOld)
                {
                    if (!CheckDuplicity(eventObject, helpEventObjectsNew))
                    {
                        RemoveEventObject(eventObject);
                        changed = true;
                    }
                }

                helpEventObjectsOld.Clear();
                helpEventObjectsOld.AddRange(helpEventObjectsNew);
            }
            catch (Exception ex)
            {
                LogFile.Log("Error in PrioController.RefreshPrioStatus: " + ex.Source + " " + ex.Message);
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
                    eventObject.Message == events.Message &&
                    eventObject.Time == events.Time)
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
