using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace Query
{
    /// <summary>
    /// class to query the connected users
    /// </summary>
    public class QueryUsers : AQuery 
    {
        private List<string> connectedUsers;

        /// <summary>
        /// returns an instance of QueryUsers
        /// </summary>
        public QueryUsers()
        {
            connectedUsers = new List<string>();
        }

        /// <summary>
        /// Check if a user is connected
        /// </summary>
        /// <param name="servername"></param>
        public void UsersConnected(string servername)
        {
            try
            {
                this.connection = ConnectToServer(servername); //make connection to server
                SelectQuery wmiQuery = new SelectQuery("SELECT LogonId FROM Win32_LogonSession"); //query to WMI
                var searcher = new ManagementObjectSearcher(this.connection, wmiQuery);
                var results = searcher.Get();

                connectedUsers.Clear();

                foreach (ManagementObject user in results)
                {
                    string name = user.GetPropertyValue("UserName").ToString();
                    name = (name.Substring(name.IndexOf("\\") + 1)).Substring(0, 3) + "*******";
                    connectedUsers.Add(name); // add User
                }
            }
            catch (Exception)
            {

            }

        }

        /// <summary>
        /// returns a list of connected users
        /// </summary>
        public List<string> ConnectedUsers
        {
            get 
            {
                return connectedUsers;
            }
        }

    }
}
