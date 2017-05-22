using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Diagnostics;
using System.Management;

namespace Query
{
    /// <summary>
    /// class to check service status
    /// </summary>
    public class QueryService : AQuery
    {
        private List<Service> services;
        private List<Service> namesStoppedServices;

        /// <summary>
        /// creates an instance of QueryService
        /// </summary>
        public QueryService()
        {
            this.services = new List<Service>();
            this.namesStoppedServices = new List<Service>();
        }


        /// <summary>
        /// creates an instance of Service and adds it to the list of observed services
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serverName"></param>
        public void AddServices(string serviceName, string serverName)
        {           
            this.services.Add(new Service(serviceName, serverName));
        }

        /// <summary>
        /// clears all services
        /// </summary>
        public void ClearServices()
        {
            this.services.Clear();
        }



        /// <summary>
        /// remove disabled services
        /// </summary>
        /// <param name="serverName"></param>
        public void ClearServicesList(string serverName)
        {
            try
            {
                this.connection = ConnectToServer(serverName);

                for (int i = 0; i < services.Count; i++)
                {
                    if (StartModeService(serverName, services[i].ServiceName) == "Disabled")
                    {
                        services.Remove(services[i]);
                    }
                }
            }
            catch
            {

            }

        }

        /// <summary>
        /// returns the Start Mode of a Service
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public string StartModeService(string serverName, string serviceName)
        {
            string mode = "";
            this.connection = ConnectToServer(serverName);
            SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_Service WHERE Name='" + serviceName + "'");
            var searcher = new ManagementObjectSearcher(this.connection, wmiQuery);
            var results = searcher.Get();

            foreach (ManagementObject service in results)
            {
                mode = service["StartMode"].ToString();
            }

            return mode;
        }


        /// <summary>
        /// Check if any service is Stopped
        /// return true if a service is stopped
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public bool CheckStoppedServices(string serverName)
        {
            bool stopped = false;
            namesStoppedServices.Clear();
            foreach (Service service in this.services)
            {
                try
                {
                    service.Refresh();
                    if (service.Status.ToString().Equals("Stopped"))
                    {
                        this.namesStoppedServices.Add(service);
                        stopped = true;
                    }
                }
                 catch (Exception)
                {
                    this.namesStoppedServices.Add(service);
                    stopped = true;
                }
            }
            return stopped;
        }


        /// <summary>
        /// returns a list of stopped services
        /// </summary>
        public List<Service> NamesStoppedServices
        {
            get
            {
                return this.namesStoppedServices;
            }
        }


        /// <summary>
        /// returns the status of the service
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public string GetServiceStatus(string serviceName, string serverName)
        {
            Service sc = new Service(serviceName, serverName);           
            return sc.Status.ToString(); 
        }

        /// <summary>
        /// returns the useg memory of this process
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public long GetMemoryUsage(string processName, string serverName)
        {
            Process[] myProcess = Process.GetProcessesByName(processName, serverName);
            long memoryUsed = myProcess[0].PrivateMemorySize64;
            return memoryUsed;
        }

        /// <summary>
        /// returns the instance name of this process
        /// </summary>
        /// <param name="nameProcess"></param>
        /// <param name="server"></param>
        /// <returns></returns>
        public string GetProcessInstanceName(string nameProcess, string server)
        {
            Process[] myProcess = Process.GetProcessesByName(nameProcess, server);
            string nam = myProcess[0].ProcessName;
            return nam;
        }

        
    }
}
