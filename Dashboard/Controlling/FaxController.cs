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
    /// organizing the observing of certain paths defined in an extern csv file
    /// </summary>
    public class FaxController
    {
        private static List<IServerObserver> observers = new List<IServerObserver>();
        private static FaxController instance;
        private Thread thread;
        private static List<PathToCheck> pathsToCheck;

        private static WindowFax faxWindow;

        private FaxController()
        {
            pathsToCheck = Options.PathsToCheck;
            foreach (PathToCheck pathToCheck in pathsToCheck)
            {
                pathToCheck.QueryFolder = new QueryFolder(pathToCheck.Path, pathToCheck.TimeMinutes);               
            }
        }

        /// <summary>
        /// returns the instance of FaxController
        /// </summary>
        public static FaxController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FaxController();
                }
                return instance;
            }
        }

        // 
        /// <summary>
        /// register the observer (the windowFax) for this controller
        /// </summary>
        /// <param name="serverObserver"></param>
        public static void RegisterObserver(IServerObserver serverObserver)
        {
            observers.Add(serverObserver);
        }

        /// <summary>
        /// unregister the observer (the windowFax) for this controller
        /// </summary>
        /// <param name="serverObserver"></param>
        public static void UnregisterObserver(IServerObserver serverObserver)
        {
            observers.Remove(serverObserver);
        }

        private static void NotifyFaxWindow()
        {
            foreach (IServerObserver ob in observers)
            {
                ob.ServerUpdate();
            }
        }

        /// <summary>
        /// Starts the Thread to monitoring the fax-paths
        /// </summary>
        /// <returns></returns>
        public Thread StartThread()
        {
            faxWindow = WindowFax.Instance;
            RegisterObserver(faxWindow);
            thread = new Thread(FaxThread);
            thread.Start();
            return thread;
        }

        private static void FaxThread()
        {
            bool changed;
            while (Options.RunThreads)
            {
                try
                {
                    Thread.Sleep(Options.CheckFolderTime);
                    changed = false;
                    foreach (PathToCheck pathToCheck in pathsToCheck)
                    {
                        if (pathToCheck.CheckStatusChange())
                        {
                            changed = true;
                        }
                    }

                    if (changed)
                    {
                        NotifyFaxWindow();
                    }
                }
                catch (Exception ex)
                {
                    LogFile.Log("Error im FaxController-Thread: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                }  
            }           
        }

        /// <summary>
        /// returns a list PathsToCheck
        /// </summary>
        public List<PathToCheck> PathsToCheck
        {
            get
            {
                return pathsToCheck;
            }
        }

        


    }
}
