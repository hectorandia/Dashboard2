using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Query;
using Server;
using System.Data;
using System.Diagnostics;

namespace Dashboard
{
    class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        //[STAThread]
        private static List<Worker> captureWorkers;
        private static List<Worker> aidaWorkers;
        private static List<Capture> captureServers = new List<Capture>();

        private static List<Thread> threads = new List<Thread>();

        private static ServiceControllerObject serviceController;
        private static IPrio0Controller prioController;
        private static FaxController faxController;

        private static DataTable servers;

        private static MotherPanel motherPanel;

        private static FactoryPanels factoryPanel;

        private static MainWindow mainWindow;

        private static AbstractFactoryServers factoryServer;

        private static List<Worker> workers;

        private static List<CapturePanel> capturePanels = new List<CapturePanel>();
        private static List<WorkerPanel> workerPanels = new List<WorkerPanel>();

        private static Object thisLock = new Object();

        [STAThread] // neccessary for the folderDialog in the option window
        static void Main(string[] args)
        {
            
            try
            {
                Options.LoadOptions();
                LogFile.Log(" --------------------------------------- ");
                LogFile.Log(" --------- Dashboard gestartet --------- ");
                LogFile.Log(" --------------------------------------- ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error beim Einlesen der Konfigurationsdatei: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                LogFile.Log("Error beim Einlesen der Konfigurationsdatei: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                MainWindow.exit_Click(new Object(), new EventArgs());
            }

            
            try
            {
                servers = Options.GetDataTableFromCsV(Options.PathServers);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error beim Einlesen der CSV-dateien: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                LogFile.Log("Error beim Einlesen der CSV-dateien: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                MainWindow.exit_Click(new Object(), new EventArgs());
            }


            bool windowsLogs = false;
            if (Options.UseMfIMonitor)
            {
                try
                {
                    prioController = new MfIControllerPrio0();
                }
                catch
                {
                    DialogResult dialogResult = MessageBox.Show("Soll statt dessen direkt über die Windows-Logs eine Abfrage gemacht werden?",
                        "Verbindungsfehler zur Datenbank", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        windowsLogs = true;
                    }
                }
                
            }
            else
            {
                windowsLogs = true;
            }

            if (windowsLogs)
            {
                Options.UseMfIMonitor = false;
                prioController = new PrioController();
            }

            try
            {
                BuildingServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error beim Erstellen der Instanzen der Server: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                LogFile.Log("Error beim Erstellen der Instanzen der Server: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                MainWindow.exit_Click(new Object(), new EventArgs());
            }

            mainWindow = MainWindow.Instance;

            serviceController = ServiceControllerObject.Instance; //new ServiceController(workers, mainWindow);
            faxController = FaxController.Instance;

            try
            {
                threads.Add(serviceController.StartThread());
                threads.Add(prioController.StartThread());
                threads.Add(faxController.StartThread());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error beim Erstellen der Threads: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                LogFile.Log("Error beim Erstellen der Threads: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                MainWindow.exit_Click(new Object(), new EventArgs());
            }

            try
            {
                StartServerThreads();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error beim Starten der ServerThreads: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                LogFile.Log("Error beim Erstellen der Threads: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                MainWindow.exit_Click(new Object(), new EventArgs());
            }

            try
            {
                //Application.EnableVisualStyles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error beim EnableVisualStyles: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                LogFile.Log("Error beim EnableVisualStyles: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                MainWindow.exit_Click(new Object(), new EventArgs());
            }

            try
            {
                Application.Run(mainWindow);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error beim Starten des Hauptfensters: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                LogFile.Log("Error beim Starten des Hauptfensters: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                MainWindow.exit_Click(new Object(), new EventArgs());
            }
            

        }


        public static void Restart(Object sender, EventArgs e)
        {
            try
            {
                //run the program again and close this one
                Process.Start(Application.StartupPath + "\\" + Process.GetCurrentProcess().ProcessName + ".exe");
                //or you can use Application.ExecutablePath

                //close this one
                MainWindow.Restart();
                Process.GetCurrentProcess().Kill();
            }
            catch
            { }
        }


        private static void StartServerThreads()
        {
            try
            {
                foreach (Worker worker in aidaWorkers)
                {
                    Thread thread = new Thread(() => AidaWorkerPerformanceThread(worker));
                    thread.Start();
                    threads.Add(thread);
                    Thread thread2 = new Thread(() => AidaWorkerHDDThread(worker));
                    thread2.Start();
                    threads.Add(thread2);
                }

                foreach (Worker worker in captureWorkers)
                {
                    Thread thread = new Thread(() => CaptureWorkerPerformanceThread(worker));
                    thread.Start();
                    threads.Add(thread);
                    Thread thread2 = new Thread(() => CaptureWorkerHDDThread(worker));
                    thread2.Start();
                    threads.Add(thread2);
                }

                foreach (ServerRZ worker in captureServers)
                {
                    Thread thread = new Thread(() => CapturePerformanceThread(worker));
                    thread.Start();
                    threads.Add(thread);
                    Thread thread2 = new Thread(() => CaptureHDDThread(worker));
                    thread2.Start();
                    threads.Add(thread2);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void AidaWorkerPerformanceThread(Worker aida)
        {
            try
            {
                while (Options.RunThreads)
                {
                    aida.GetRamUsage();
                    aida.GetCpuUsage();
                    Thread.Sleep(Options.CheckAidaWorkerPerformanceTime);
                }
            }
            catch
            {

            }
            
        }

        private static void CaptureWorkerPerformanceThread(Worker capture)
        {
            try
            {
                while (Options.RunThreads)
                {
                    capture.GetRamUsage();
                    capture.GetCpuUsage();
                    Thread.Sleep(Options.CheckCapturePerformanceTime);
                }
            }
            catch
            {

            }
            
        }


        private static void CapturePerformanceThread(ServerRZ capture)
        {
            try
            {
                while (Options.RunThreads)
                {
                    capture.GetCpuUsage();
                    capture.GetRamUsage();
                    Thread.Sleep(Options.CheckCapturePerformanceTime);
                }
            }
            catch 
            {

            }
            
        }

        private static void AidaWorkerHDDThread(Worker aida)
        {
            try
            {
                while (Options.RunThreads)
                {
                    aida.GetLogicalDisk();
                    Thread.Sleep(Options.CheckAidaWorkerHDDTime);
                }
            }
            catch
            {

            }
            
        }

        private static void CaptureWorkerHDDThread(Worker capture)
        {
            try
            {
                while (Options.RunThreads)
                {
                    capture.GetLogicalDisk();
                    Thread.Sleep(Options.CheckCaptureWorkerHDDTime);
                }
            }
            catch
            { 

            }
            
        }

        private static void CaptureHDDThread(ServerRZ capture)
        {
            try
            {
                while (Options.RunThreads)
                {
                    capture.GetLogicalDisk();
                    Thread.Sleep(Options.CheckCaptureHDDTime);
                }
            }
            catch
            {

            }
            
        }

        private static void BuildingServer()
        {
            try
            {
                captureWorkers = new List<Worker>();
                aidaWorkers = new List<Worker>();
                captureServers = new List<Capture>();

                factoryServer = new ConcreteFactoryServer();



                factoryPanel = FactoryPanels.Instance(servers.Rows.Count);
                motherPanel = factoryPanel.CreateMotherPanel();

                for (int i = 0; i < servers.Rows.Count; i++)
                {
                    if (servers.Rows[i]["ServerTyp"].ToString() == "CaptureWorker")
                    {
                        Worker worker = factoryServer.CaptureWorker(servers.Rows[i]["ServerName"].ToString());
                        captureWorkers.Add(worker);
                    }
                }

                for (int i = 0; i < servers.Rows.Count; i++)
                {
                    if (servers.Rows[i]["ServerTyp"].ToString() == "AidaWorker")
                    {
                        Worker worker = factoryServer.AidaWorker(servers.Rows[i]["ServerName"].ToString());
                        aidaWorkers.Add(worker);
                    }
                }

                for (int i = 0; i < servers.Rows.Count; i++)
                {
                    if (servers.Rows[i]["ServerTyp"].ToString() == "Capture")
                    {
                        Capture capture = factoryServer.CreateCapture(servers.Rows[i]["ServerName"].ToString());
                        captureServers.Add(capture);
                    }
                }




                workers = new List<Worker>();
                workers.AddRange(captureWorkers);
                workers.AddRange(aidaWorkers);

                foreach (Worker worker in workers)
                {
                    workerPanels.Add(factoryPanel.CreateWorkerPanel(worker));
                }

                foreach (WorkerPanel workerPanel in workerPanels)
                {
                    motherPanel.AddPanel(workerPanel);
                }

                foreach (Capture capture in captureServers)
                {
                    capturePanels.Add(factoryPanel.CreateCapturePanel(capture));
                }

                foreach (CapturePanel capturePanel in capturePanels)
                {
                    motherPanel.AddPanel(capturePanel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// returns a list of Worker
        /// </summary>
        public static List<Worker> Workers
        {
            get
            {
                return workers;
            }
        }

        /// <summary>
        /// returns a list of threads
        /// </summary>
        public static List<Thread> Threads
        {
            get
            {
                return threads;
            }
        }

        /// <summary>
        /// returns the main window
        /// </summary>
        public static MainWindow MainWindow
        {
            get
            {
                return mainWindow;
            }
        }

        /// <summary>
        /// returns the motherPanel
        /// </summary>
        public static MotherPanel MotherPanel
        {
            get
            {
                return motherPanel;
            }
        }

        /// <summary>
        /// returns the service controller
        /// </summary>
        public static ServiceControllerObject ServiceController
        {
            get
            {
                return serviceController;
            }
        }

        /// <summary>
        /// returns the prio controller
        /// </summary>
        public static IPrio0Controller PrioController
        {
            get
            {
                return prioController;
            }
        }

        /// <summary>
        /// returns a list of catures
        /// </summary>
        public static List<Capture> CaptureServer
        {
            get
            {
                lock (thisLock)
                {
                    return captureServers;
                }
            }
        } 

        /// <summary>
        /// returns the fax controller
        /// </summary>
        public static FaxController FaxController
        {
            get
            {
                return faxController;
            }
        }

        /// <summary>
        /// returns all the capturePanels
        /// </summary>
        public static List<CapturePanel> CapturePanels
        {
            get
            {
                return capturePanels;
            }
        }


        public static List<WorkerPanel> WorkerPanels
        {
            get
            {
                return workerPanels;
            }
        }


    }
}
