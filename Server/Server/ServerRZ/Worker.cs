using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Query;
using System.ServiceProcess;
using System.Data;

namespace Server
{
    /// <summary>
    /// abstract class for Worker
    /// </summary>
    public abstract class Worker : ServerRZ
    {
        /// <summary>
        /// whether there are stopped services or not
        /// </summary>
        protected bool stoppedServices = false;
        /// <summary>
        /// list of stopped services
        /// </summary>
        protected List<Service> servicesStopped = new List<Service>();
        /// <summary>
        /// DataTable of services to observe
        /// </summary>
        protected DataTable services;

        /// <summary>
        /// returns an instance of a Worker
        /// </summary>
        /// <param name="serverName"></param>
        public Worker(string serverName) : base(serverName)
        {
            this.serverName = serverName;
            queryServices = new QueryService();
            services = Options.GetDataTableFromCsV(Options.PathServices);
            //LoadAllServices();
        }

        /// <summary>
        /// loads all services which should be observed
        /// </summary>
        public abstract void LoadAllServices();

        /// <summary>
        /// returns a list of stopped services
        /// </summary>
        public List<Service> ServicesStopped
        {
            get
            {
                 return servicesStopped;
            }
        }

        /// <summary>
        /// refreshs the status of stopped services and the list of stopped services
        /// </summary>
        public void GetNamesStoppedServices()
        {
            this.stoppedServices = queryServices.CheckStoppedServices(this.serverName);
            servicesStopped = queryServices.NamesStoppedServices;
        }

        /// <summary>
        /// returns the status whether there are stopped services
        /// </summary>
        public bool StoppedServices
        {
            get
            {
                return this.stoppedServices;
            }
        }

        /// <summary>
        /// returns the status of the service
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public string GetServiceStatus(string serviceName)
        {
            return queryServices.GetServiceStatus(this.serverName, serviceName);
        }

        /// <summary>
        /// checks the connection to the server
        /// </summary>
        public override void CheckServerConnection()
        {
            lock (thislock)
            {
                bool help = this.serverDisconect;
                try
                {
                    this.serverDisconect = queryPing.PingToServer(this.serverName);
                }
                catch (Exception)
                {
                    this.serverDisconect = true;
                    //MessageBox.Show("Error in CheckServerConnection in Server.cs :" + ex.Message);
                }
                finally
                {
                    this.NotifyServerPanelObserver();
                    if (help == true && !this.serverDisconect)
                    {
                        this.LoadAllServices();
                    }
                }     
            }
        }


           

    }
}
