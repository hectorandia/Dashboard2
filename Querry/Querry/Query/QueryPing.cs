using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;
using System.Windows.Forms;
using System.Management;

namespace Query
{
    /// <summary>
    /// class to query the ping and checking the connection to the server
    /// </summary>
    public class QueryPing : AQuery
    {
        private Ping ping;
        private bool serverDisconect;

        /// <summary>
        /// returns an instance of QueryPing
        /// </summary>
        public QueryPing()
        {
            ping = new Ping();
            serverDisconect = false;
        }


        /// <summary>
        /// checks the connection to the server
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public bool PingToServer(string serverName)
        {
            int timeout = 50;
            try
            {
                if (ping.Send(serverName, timeout).Status == IPStatus.Success)
                {
                    serverDisconect = false; //server Found
                }
                else
                {
                    serverDisconect = true;
                }

            }
            catch (PingException ex)
            {
                serverDisconect = true; //server not Found
                throw ex;
            }

            return serverDisconect;
        }

        /// <summary>
        /// returns the ip
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public string GetServerIP(string serverName)
        {
            try
            {
                IPHostEntry hostname = Dns.GetHostEntry(serverName);
                IPAddress[] ip = hostname.AddressList;
                serverDisconect = false;

                //pick the IPv4 adress from all ip adresses stored in ip and return this one
                foreach (IPAddress ipadress in ip)
                {
                    if (IsIPv4(ipadress.ToString()))
                    {
                        return ipadress.ToString();
                    }
                }

                return ip[0].ToString();
            }
            catch(Exception ex)
            {
                serverDisconect = true;
                throw ex;
            }
        }

        private bool IsIPv4(string ip)
        {
            string[] split = ip.Split('.');
            if (split.Length == 4)
            {
                return true;
            }
            else
            {
                return false;                
            }
        }

        /// <summary>
        /// returns the mac adress
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public string GetMacAddresse(string serverName)
        {
            string macAddresse = "";
            try
            {
                this.connection = ConnectToServer(serverName);
                SelectQuery wmiQuery = new SelectQuery("Select * from Win32_NetworkAdapterConfiguration");
                var searcher = new ManagementObjectSearcher(this.connection, wmiQuery);
                var results = searcher.Get();

                foreach (ManagementObject mac in results)
                {
                    if (mac.GetPropertyValue("MacAddress") == null)
                    {
                        continue;
                    }
                    macAddresse = mac.GetPropertyValue("MacAddress").ToString();
                    break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return macAddresse;
        }

        /// <summary>
        /// returns the value of serverDisconect
        /// </summary>
        public bool ServerDissconect
        {
            get
            {
                return this.serverDisconect;
            }
            set 
            {
                serverDisconect = value;
            }
        }

        
    }
}
