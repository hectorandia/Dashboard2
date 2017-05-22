using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query
{
    /// <summary>
    /// Class to hold all the information about a stopped service together
    /// </summary>
    public class ServiceStopped
    {
        private String serverName;
        private String serviceName;
        private String timeOfStop;

        /// <summary>
        /// Creates an instance of an stopped service
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="serviceName"></param>
        /// <param name="timeOfFailing"></param>
        public ServiceStopped(String serverName, String serviceName, DateTime timeOfFailing)
        {
            this.serverName = serverName;
            this.serviceName = serviceName;
            this.timeOfStop = timeOfFailing.ToString("HH:mm:ss");
        }


        /// <summary>
        /// returns the servername where the service ist stopped
        /// </summary>
        public String ServerName
        {
            get
            {
                return serverName;
            }
        }

        /// <summary>
        /// returns the name of the service whcih is stopped
        /// </summary>
        public String ServiceName
        {
            get
            {
                return serviceName;
            }
        }

        /// <summary>
        /// returns the time when the service stopped
        /// </summary>
        public String TimeOfStop
        {
            get
            {
                return timeOfStop;
            }
        }

    }
}
