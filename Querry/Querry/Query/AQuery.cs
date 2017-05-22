using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Windows.Forms;

namespace Query
{
    /// <summary>
    /// abstract class for queries
    /// </summary>
    public abstract class AQuery
    {
        /// <summary>
        /// member to handle the connection to a server
        /// </summary>
        protected ManagementScope connection;

        /**
         * Generates a remote connection to a server in order to use ManagementObject
         * */
        public ManagementScope ConnectToServer(string serverName)
        {
            try
            {
                ConnectionOptions conectionsOptions = new ConnectionOptions();
                //Set the route to be administered
                ManagementScope scope = new ManagementScope(@"\\" + serverName + @"\root\cimv2");
                scope.Options = conectionsOptions;
                return scope;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
