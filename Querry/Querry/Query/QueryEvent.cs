using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Query
{
    /// <summary>
    /// class to query for the event specified in the config file
    /// </summary>
    public class QueryEvent : AQuery
    {
        private EventLog eventing;
        private DateTime startDate;
        private int ignoreHours;

        private static Object thisLock = new Object();

        /// <summary>
        /// returns a instance of QueryEvent
        /// </summary>
        /// <param name="hours"></param>
        public QueryEvent(int hours)
        {
            this.ignoreHours = hours; // set hours to ignore
            eventing = new EventLog(); // Variable that records the event
            startDate = DateTime.Now.AddHours(-hours); //Current event to avoid taking events prior to when the program was opened
        }


        /// <summary>
        /// returns a list of EventObjects
        /// </summary>
        /// <param name="eventLog"></param>
        /// <param name="eventID"></param>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public List<IEventObject> GetEventObjects(string eventLog, int eventID, string serverName)
        {
            try
            {
                List<IEventObject> eventObjects = new List<IEventObject>();
                eventing.Log = eventLog; //Type of event to be controlled(Application o System)
                eventing.MachineName = serverName; //Name of server where the event is searched

                lock (thisLock)
                {
                    for (int i = eventing.Entries.Count; i > 0; i--)
                    {
                        // doesn' work with InstanceId so we use better the deprecated EventId
#pragma warning disable CS0618 // Type or member is obsolete
                        if (eventing.Entries[i - 1].EventID == eventID && eventing.Entries[i - 1].TimeGenerated > startDate)
#pragma warning restore CS0618 // Type or member is obsolete
                        {
                            eventObjects.Add(new EventObject(serverName, eventing.Entries[i - 1]
                                .Message, eventing.Entries[i - 1].TimeGenerated
                                .ToString("HH:mm:ss")));
                        }
                    }
                    RestartDateTime();
                    return eventObjects;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


        /// <summary>
        /// returns whether there occured the observed event or not
        /// </summary>
        /// <param name="eventLog"></param>
        /// <param name="eventID"></param>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public bool GetEventStatusPrio0(string eventLog, int eventID, string serverName)
        {
            bool eventStatus = false;
            try
            {
                eventing.Log = eventLog; //Type of event to be controlled(Application o System)
                eventing.MachineName = serverName; //Name of the server where the event is searched

                for (int i = eventing.Entries.Count; i > 0; i--)
                {
                    if (eventing.Entries[i - 1].TimeGenerated > startDate)
                    {
                        // doesn' work with InstanceId so we use better the deprecated EventId
#pragma warning disable CS0618 // Type or member is obsolete
                        if (eventing.Entries[i - 1].EventID == eventID)
#pragma warning restore CS0618 // Type or member is obsolete
                        {
                            eventStatus = true;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            RestartDateTime();
            return eventStatus;
        }



        private void RestartDateTime()
        {
            startDate = DateTime.Now.AddHours(-ignoreHours);
        }
    }
}
