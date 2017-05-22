using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query
{
    /// <summary>
    /// class to hold the information about an event in one place
    /// </summary>
    public class EventObjectMfI : IEventObject
    {
        private string serverName;
        private int number;
        private string time;

        /// <summary>
        /// returns an instance of an EventObject
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="number"></param>
        /// <param name="time"></param>
        public EventObjectMfI(string serverName, int number, string time)
        {
            this.serverName = serverName;
            this.number = number;
            this.time = time;
        }


        /// <summary>
        /// returns the name of the server on which this event occurs
        /// </summary>
        public string ServerName
        {
            get
            {
                return serverName;
            }
        }


        /// <summary>
        /// returns the message of the event
        /// </summary>
        public int Number
        {
            get
            {
                return number;
            }
        }

        /// <summary>
        /// retrns the time when this event occurs
        /// </summary>
        public string Time
        {
            get
            {
                return time;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Message
        {
            get
            {
                return "";
            }
        }

    }
}
