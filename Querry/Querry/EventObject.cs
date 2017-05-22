using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query
{
    /// <summary>
    /// class to hold the information about an event in one place
    /// </summary>
    public class EventObject : IEventObject
    {
        private string serverName;
        private string message;
        private string time;


        /// <summary>
        /// returns an instance of an EventObject
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="message"></param>
        /// <param name="time"></param>
        public EventObject(string serverName, string message, string time)
        {
            this.serverName = serverName;
            this.message = message;
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
                return 0;
            }
        }


        /// <summary>
        /// returns the message of the event
        /// </summary>
        public string Message
        {
            get
            {
                return message;
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

    }
}
