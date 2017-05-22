using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Query;

namespace Server
{
    /// <summary>
    /// class for a capture server
    /// </summary>
    public class Capture : ServerRZ 
    {
        private bool statusPrio0;
        private List<IEventObject> eventObjects = new List<IEventObject>();

        /// <summary>
        /// returns an instance of Capture
        /// </summary>
        /// <param name="serverName"></param>
        public Capture(string serverName) : base(serverName) 
        {
            this.serverName = serverName;
            queryEvent = new QueryEvent(Options.EventLogDelay);
        }

        /// <summary>
        /// check for the event e.g. prio0
        /// </summary>
        /// <param name="eventID"></param>
        public void CheckPrio0(int eventID)
        {
            try
            {
                statusPrio0 = queryEvent.GetEventStatusPrio0("Application", eventID, this.serverName);
                if (statusPrio0)
                {
                    eventObjects.Clear();
                    eventObjects.AddRange(queryEvent.GetEventObjects("Application", eventID, this.serverName));
                }
            }
            catch (Exception ex)
            {
                LogFile.Log("Event-Abfrage gescheitert: " + serverName + " "
                    + ex.Source + " " + ex.Message);
                this.CheckServerConnection();
            }
            
        }

        /// <summary>
        /// returns a list of occured EventObjects 
        /// </summary>
        /// <returns></returns>
        public List<IEventObject> GetEventObjects()
        {
            return eventObjects;
        }

        /// <summary>
        /// checks for events
        /// returns true if an event occurs e.g. Prio0
        /// </summary>
        public bool StatusPrio0
        {
            get
            {
                return this.statusPrio0;
            }
        }

        /// <summary>
        /// checks the connection to the server
        /// </summary>
        public override void CheckServerConnection()
        {
            lock (thislock)
            {

                bool help = this.serverDisconect;
                try
                {
                    this.serverDisconect = queryPing.PingToServer(this.serverName);
                }
                catch (Exception)
                {
                    this.serverDisconect = true;
                    //MessageBox.Show("Error in CheckServerConnection in Server.cs :" + ex.Message);
                }
                finally
                {
                    this.NotifyServerPanelObserver();
                }     
            }
        }

    }
}
