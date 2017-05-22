using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Query;



namespace Server
{
    /// <summary>
    /// abstract class for a server 
    /// </summary>
    public abstract class ServerRZ : Server
    {
        /// <summary>
        /// cpu ussage in percent (0-100)
        /// </summary>
        protected int cpuUsage;
        /// <summary>
        /// ram ussage in percent (0-100)
        /// </summary>
        protected int ramUsage;
        /// <summary>
        /// new list of HDD
        /// </summary>
        protected List<HDD> logicalDiskNew = new List<HDD>();
        /// <summary>
        /// old list of HDD
        /// </summary>
        protected List<HDD> logicalDiskOld = new List<HDD>();
        /// <summary>
        /// ip of the server
        /// </summary>
        protected string serverIP;
        /// <summary>
        /// mac adress of the server
        /// </summary>
        protected string mac;
        /// <summary>
        /// list of connected users
        /// </summary>
        protected List<string> users;


        /// <summary>
        /// creates an instance of ServerRZ
        /// </summary>
        /// <param name="serverName"></param>
        public ServerRZ(string serverName) : base(serverName)
        {
            queryCPU = new QueryCPU();
            queryHDD = new QueryHDD();
            queryRam = new QueryRAM();
            queryUsers = new QueryUsers();
            GetServerIP();
            GetMac();
        }

        /// <summary>
        /// refreshs the list of connected users
        /// </summary>
        public void GetUsers()
        {
            queryUsers.UsersConnected(this.serverName);
            this.users = queryUsers.ConnectedUsers;
        }

        /// <summary>
        /// returns a list of connected users
        /// </summary>
        public List<string> Users
        {
            get
            {
                GetUsers();
                return users;
            }
        }

        /// <summary>
        /// refreshs the mac adress
        /// </summary>
        public void GetMac()
        {
            try
            {
                this.mac = queryPing.GetMacAddresse(this.serverName);
            }
            catch (Exception ex)
            {
                LogFile.Log("Mac-Abfrage gescheitert: " + ServerName + " " + ex.Source + " " + ex.Message);
                this.CheckServerConnection();
            }
        }

        /// <summary>
        /// refreshs the ip 
        /// </summary>
        public void GetServerIP()
        {
            try
            {
                serverIP = queryPing.GetServerIP(this.serverName);
            }
            catch (Exception ex)
            {
                LogFile.Log("IP-Abfrage gescheitert: " + ServerName + " " + ex.Source + " " + ex.Message);
                this.CheckServerConnection();
            }
            
        }

        /// <summary>
        /// refreshs the ussage of cpu
        /// </summary>
        public void GetCpuUsage()
        {
            try
            {
                this.cpuUsage = queryCPU.getCurrentUssage(this.serverName);
                this.Notify();
            }
            catch (Exception ex)
            {
                LogFile.Log("CPU-Abfrage gescheitert: " + ServerName + " " + ex.Source + " " + ex.Message);
                this.cpuUsage = 0;
                this.CheckServerConnection();
            }
        }

        /// <summary>
        /// refreshs the ussage of ram
        /// </summary>
        public void GetRamUsage()
        {
            try
            {
                this.ramUsage = queryRam.getCurrentUssage(this.serverName);
                this.Notify();
            }
            catch (Exception ex)
            {
                LogFile.Log("RAM-Abfrage gescheitert: " + ServerName + " " + ex.Source + " " + ex.Message);
                this.ramUsage = 0;
                this.CheckServerConnection();
            }
        }

        /// <summary>
        /// refreshs the list of hdds
        /// </summary>
        public void GetLogicalDisk()
        {
            try
            {
                this.logicalDiskNew = queryHDD.GetLogicalDiskInfo(this.serverName);
                if (!Enumerable.SequenceEqual(logicalDiskOld.OrderBy(t => t.Device), logicalDiskNew.OrderBy(t => t.Device))) //Compares the values
                {
                    this.Notify();
                }
                logicalDiskOld = logicalDiskNew; 
            }
            catch (Exception ex)
            {
                LogFile.Log("HDD-Abfrage gescheitert: " + ServerName + " " + ex.Source + " " + ex.Message);
                this.CheckServerConnection();
            }
  
        }

        /// <summary>
        /// returns the actual ussage of cpu
        /// </summary>
        public int CpuUsage
        {
            get
            {
                return this.cpuUsage;
            }
        }

        /// <summary>
        /// returns the actual ussage of ram
        /// </summary>
        public int RamUsage
        {
            get
            {
                return this.ramUsage;
            }
        }

        /// <summary>
        /// returns a list of hdds
        /// </summary>
        public List<HDD> LogicalDisk
        {
            get
            {
                return this.logicalDiskNew;
            }
        }

        /// <summary>
        /// returns the ip of this servers
        /// </summary>
        public string ServerIP
        {
            get
            {
                return this.serverIP;
            }
        }
        
        /// <summary>
        /// returns the mac adress
        /// </summary>
        public string Mac
        {
            get
            {
                return mac;
            }
        }

    }
}
