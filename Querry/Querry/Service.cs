using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Query
{
    /// <summary>
    /// class extented from the class Service so that one can access the name of the service even if it is not running
    /// </summary>
    public class Service : ServiceController
    {
        /// <summary>
        /// name of the service
        /// </summary>
        protected string name;

        /// <summary>
        /// returns an instance of Service
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serverName"></param>
        public Service(string serviceName, string serverName) : base(serviceName, serverName)
        {
            this.name = serviceName;
        }

        /// <summary>
        /// returns or sets the name of the service
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
    }
}
