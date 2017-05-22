using Query;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// Controller for Monitoring the services
    /// </summary>
    public class ServiceControllerObject
    {
        private static List<IServerObserver> observers = new List<IServerObserver>();
        private static List<Worker> workers = new List<Worker>();
        private static List<ServiceStopped> stoppedServices = new List<ServiceStopped>();
        private static List<Service> helpListServicesOld = new List<Service>();
        private static List<Service> helpListServicesNew = new List<Service>();

        //private WindowService windowService;
        private MainWindow mainWindow;

        private static ServiceControllerObject instance;

        private Thread thread;

        /// <summary>
        /// returns a list of workers which are monitored
        /// </summary>
        public List<Worker> Workers
        {
            get
            {
                return workers;
            }
        }

        private ServiceControllerObject()
        {
            workers = Program.Workers; // workers;
            this.mainWindow = Program.MainWindow; // mainWindow;      
        }


        /// <summary>
        /// returns the instance
        /// </summary>
        public static ServiceControllerObject Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServiceControllerObject();
                }
                return instance;
            }
        }

        /// <summary>
        /// starts the thread for monitoring the services
        /// </summary>
        /// <returns></returns>
        public Thread StartThread()
        {
            RegisterObserver(WindowService.Instance);
            thread = new Thread(() => ServiceThread());
            thread.Start();
            return thread;
        }

        /// <summary>
        /// returns a list of StoppedServices
        /// </summary>
        public static List<ServiceStopped> StoppedServices
        {
            get
            {
                return stoppedServices;
            }
        }

        /// <summary>
        /// registers an observer (WindowService)
        /// </summary>
        /// <param name="serverObserver"></param>
        public static void RegisterObserver(IServerObserver serverObserver)
        {
            observers.Add(serverObserver);
        }

        /// <summary>
        /// unregisters an observer (WindowService)
        /// </summary>
        /// <param name="serverObserver"></param>
        public static void UnregisterObserver(IServerObserver serverObserver)
        {
            observers.Remove(serverObserver);
        }


        private static void Notify()
        {
            foreach (IServerObserver ob in observers)
            {
                ob.ServerUpdate();
            }
        }

        private static void AddService(Service service)
        {
            stoppedServices.Add(new ServiceStopped(service.MachineName, service.Name, DateTime.Now));
            LogFile.Log("Service auf " + service.MachineName + " ausgefallen: " + service.Name);
        }

        private static void RemoveService(Service service)
        {
            for (int i = 0; i < stoppedServices.Count; i++)
            {
                if (stoppedServices[i].ServiceName.Equals(service.Name) && stoppedServices[i].ServerName.Equals(service.MachineName))
                {
                    LogFile.Log("Service auf " + stoppedServices[i].ServerName +
                        " gestartet: " + stoppedServices[i].ServiceName);
                    stoppedServices.Remove(stoppedServices[i]);
                }
            }

        } 

        // refreshs the status of services
        private static bool RefreshStoppedServices()
        {
            bool changed = false;
            helpListServicesNew.Clear();

            // load the new stopped services
            foreach (Worker worker in workers)
            {
                worker.CheckServerConnection();
                if (!worker.ServerDisconect)
                {
                    worker.GetNamesStoppedServices();
                    helpListServicesNew.AddRange(worker.ServicesStopped);
                }
                
               
            }

            // check if they are already there and add them if not
            foreach (Service service in helpListServicesNew)
            {
                if (!Contained(helpListServicesOld,service))
                {
                    AddService(service);
                    changed = true;
                }
            }

            // check if the notified services are still not working and remove them if not
            foreach (Service service in helpListServicesOld)
            {
                if (!Contained(helpListServicesNew, service))
                {
                    RemoveService(service);
                    changed = true;
                }
            }

            helpListServicesOld.Clear();
            helpListServicesOld.AddRange(helpListServicesNew);
            return changed;
        }

        private static bool Contained(List<Service> list, Service service) {
            bool found = false;
            for (int i = 0; i < list.Count; i++)
			{
			    try
                {
                    if (list[i].MachineName == service.MachineName && list[i].ServiceName == service.ServiceName)
                    {
                        found = true;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    LogFile.Log("Error in ServiceControllerObject.Contained: " + ex.Source + " " + ex.Message);
                }
                
			}
            return found;
        }

        static void ServiceThread()
        {
            bool stopped = false;
            foreach (Worker worker in workers)
            {
                worker.LoadAllServices();
            }

            while (Options.RunThreads)
            {
                try
                {
                    Thread.Sleep(Options.CheckStoppedServicesTime);

                    if (RefreshStoppedServices())
                    {

                        stopped = true;
                        Notify();
                    }
                    else if (stopped && StoppedServices.Count == 0)
                    {

                        stopped = false;
                        Notify();
                    }
                }
                catch (Exception ex)
                {
                    LogFile.Log("Error im ServiceThread: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                }
                

            }        

        }

    }
}
