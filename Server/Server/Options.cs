using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace Server
{
    /// <summary>
    /// class for all options
    /// </summary>
    public static class Options
    {
        private static Color backColorCapturePanel;
        private static Color backColorWorkerPanel;

        private static Color backColorServerPanelByProblems;

        private static Color backColorServerDisconnect;

        private static Color fontColor;

        private static Color backColorServiceWindow;
        private static Color backColorServiceWindow2;
        private static Color gridColorServiceWindow;

        private static Color backColorPrioWindow;
        private static Color backColorPrioWindow2;
        private static Color gridColorPrioWindow;

        private static Color backColorFaxWindow;
        private static Color backColorFaxWindow2;
        private static Color gridColorFaxWindow;

        private static Font fontMainWindow; 
        private static Font fontServiceWindow;
        private static Font fontPrioWindow;
        private static Font fontFaxWindow;

        private static int eventIDPrio0;

        private static int screenNumber;

        private static double opacityServiceWindow;
        private static double opacityPrioWindow;
        private static double opacityFaxWindow;

        private static int checkPrio0Time;
        private static int checkStoppedServicesTime;
        private static int checkFolderTime;
        private static int checkCapturePerformanceTime;
        private static int checkAidaWorkerPerformanceTime;
        private static int checkCaptureWorkerPerformanceTime;
        private static int checkAidaWorkerHDDTime;
        private static int checkCaptureWorkerHDDTime;
        private static int checkCaptureHDDTime;

        private static List<PathToCheck> pathsToCheck;
        private static string logFilePath;


        private static bool closeServiceWindowAutomatic;
        private static bool closePrioWindowAutomatic;
        private static bool closeFaxWindowAutomatic;

        private static int ramWarningPercentage;
        private static int cpuWarningPercentage;
        private static int hddWarningPercentage;
        private static Color ramColor;
        private static Color cpuColor;
        private static Color ramWarningColor;
        private static Color cpuWarningColor;
        private static Color hddWarningColor;

        private static DataTable csvData;

        private static string pathServers;
        private static string pathServices;
        private static string pathForPathToCheck;

        private static Int16 eventLogDelay;
        private static bool runThreads = true;
        private static LogFile log;

        

        private static bool useMfIMonitor;

        private static string dataConnection;
        private static string driver;
        private static string server;
        private static string dsn;
        private static string database;

        private static string serverNameColumn;
        private static string numberOfPrio0Column = "";
        private static string timestampColumn = "";
        private static string databaseTable = "";

        private static void LoadPathsToCheck(string path)
        {
            try
            {
                DataTable pathDataTable = new DataTable();
                pathDataTable = GetDataTableFromCsV(path);

                pathsToCheck = new List<PathToCheck>();

                string pathFolder;
                Int16 time;
                bool files;
                bool directories;

                for (int i = 0; i < pathDataTable.Rows.Count; i++)
                {
                    pathFolder = pathDataTable.Rows[i]["Path"].ToString();
                    time = Int16.Parse(pathDataTable.Rows[i]["Time"].ToString());
                    files = bool.Parse(pathDataTable.Rows[i]["CheckForFiles"].ToString());
                    directories = bool.Parse(pathDataTable.Rows[i]["CheckForDirectories"].ToString());
                    pathsToCheck.Add(new PathToCheck(pathFolder, time, files, directories));
                }
            }
            catch (Exception ex)
            {
                ex.Source = "Options.LoadPathsToCheck " + ex.Source;
                throw ex;
            }
            

        }

        private static void SetConfig(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
        }


        /// <summary>
        /// load all options from the config file
        /// </summary>
        public static void LoadOptions()
        {
            try
            {
                logFilePath = ConfigurationManager.AppSettings["logFilePath"];
                log = LogFile.Instance;
            }
            catch (Exception ex)
            {
                LogFile.setInactive();
                throw ex;
            }

            try
            {
                eventLogDelay = Int16.Parse(ConfigurationManager.AppSettings["EventLogDelay"]);
                pathServers = ConfigurationManager.AppSettings["ServersPath"];
                pathServices = ConfigurationManager.AppSettings["ServicesPath"];
                pathForPathToCheck = ConfigurationManager.AppSettings["PathsCheckingPath"];
                LoadPathsToCheck(pathForPathToCheck);

                Int32.TryParse(ConfigurationManager.AppSettings["ScreenNumber"],
                out screenNumber);
                backColorCapturePanel = Color.FromArgb ( int.Parse(ConfigurationManager.AppSettings["backColorCapturePanel"],
                    System.Globalization.NumberStyles.HexNumber));
                backColorWorkerPanel = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["backColorWorkerPanel"],
                    System.Globalization.NumberStyles.HexNumber));
                backColorServerDisconnect = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["backColorServerDisconnect"],
                    System.Globalization.NumberStyles.HexNumber));
                fontColor = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["fontColor"],
                    System.Globalization.NumberStyles.HexNumber));
                fontMainWindow = new Font(ConfigurationManager.AppSettings["fontMainWindowName"],
                    float.Parse(ConfigurationManager.AppSettings["fontMainWindowSize"]),
                    (FontStyle)Int32.Parse(ConfigurationManager.AppSettings["fontMainWindowStyle"]));
                fontServiceWindow = new Font(ConfigurationManager.AppSettings["fontServiceWindowName"],
                    float.Parse(ConfigurationManager.AppSettings["fontServiceWindowSize"]),
                    (FontStyle)Int32.Parse(ConfigurationManager.AppSettings["fontServiceWindowStyle"]));
                fontPrioWindow = new Font(ConfigurationManager.AppSettings["fontPrioWindowName"],
                    float.Parse(ConfigurationManager.AppSettings["fontPrioWindowSize"]),
                    (FontStyle)Int32.Parse(ConfigurationManager.AppSettings["fontPrioWindowStyle"]));
                fontFaxWindow = new Font(ConfigurationManager.AppSettings["fontFaxWindowName"],
                    float.Parse(ConfigurationManager.AppSettings["fontFaxWindowSize"]),
                    (FontStyle)Int32.Parse(ConfigurationManager.AppSettings["fontFaxWindowStyle"]));
                eventIDPrio0 = Int32.Parse(ConfigurationManager.AppSettings["eventIdPrio0"]);
                backColorServiceWindow = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["backColorServiceWindow"],
                    System.Globalization.NumberStyles.HexNumber));
                backColorServiceWindow2 = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["backColorServiceWindow2"],
                    System.Globalization.NumberStyles.HexNumber));
                gridColorServiceWindow = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["gridColorServiceWindow"],
                    System.Globalization.NumberStyles.HexNumber));
                gridColorFaxWindow = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["gridColorFaxWindow"],
                    System.Globalization.NumberStyles.HexNumber));
                backColorPrioWindow = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["backColorPrioWindow"],
                    System.Globalization.NumberStyles.HexNumber));
                backColorFaxWindow = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["backColorFaxWindow"],
                    System.Globalization.NumberStyles.HexNumber));
                backColorFaxWindow2 = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["backColorFaxWindow2"],
                    System.Globalization.NumberStyles.HexNumber));
                backColorPrioWindow2 = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["backColorPrioWindow2"],
                    System.Globalization.NumberStyles.HexNumber));
                gridColorPrioWindow = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["gridColorPrioWindow"],
                    System.Globalization.NumberStyles.HexNumber));
                opacityServiceWindow = double.Parse(
                    ConfigurationManager.AppSettings["opacityServiceWindow"]);
                opacityPrioWindow = double.Parse(
                    ConfigurationManager.AppSettings["opacityPrioWindow"]);
                opacityFaxWindow = double.Parse(
                    ConfigurationManager.AppSettings["opacityFaxWindow"]);
                checkPrio0Time = Int32.Parse(
                    ConfigurationManager.AppSettings["checkPrio0Time"]);
                backColorServerPanelByProblems = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["backColorServerPanelByProblems"],
                    System.Globalization.NumberStyles.HexNumber));
                closeServiceWindowAutomatic = bool.Parse(
                    ConfigurationManager.AppSettings["closeServiceWindowAutomatic"]);
                closePrioWindowAutomatic = bool.Parse(
                    ConfigurationManager.AppSettings["closePrioWindowAutomatic"]);
                closeFaxWindowAutomatic = bool.Parse(
                    ConfigurationManager.AppSettings["closeFaxWindowAutomatic"]);
                ramWarningPercentage = Int32.Parse(
                    ConfigurationManager.AppSettings["ramWarningPercentage"]);
                cpuWarningPercentage = Int32.Parse(
                    ConfigurationManager.AppSettings["cpuWarningPercentage"]);
                ramWarningColor = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["ramWarningColor"],
                    System.Globalization.NumberStyles.HexNumber));
                cpuWarningColor = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["cpuWarningColor"],
                    System.Globalization.NumberStyles.HexNumber));
                ramColor = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["ramColor"],
                    System.Globalization.NumberStyles.HexNumber));
                cpuColor = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["cpuColor"],
                    System.Globalization.NumberStyles.HexNumber));
                hddWarningPercentage = Int32.Parse(
                    ConfigurationManager.AppSettings["hddWarningPercentage"]);
                hddWarningColor = Color.FromArgb(int.Parse(ConfigurationManager.AppSettings["hddWarningColor"],
                    System.Globalization.NumberStyles.HexNumber));
                checkCapturePerformanceTime = Int32.Parse(
                    ConfigurationManager.AppSettings["checkCapturePerformanceTime"]);
                checkAidaWorkerPerformanceTime = Int32.Parse(
                    ConfigurationManager.AppSettings["checkAidaWorkerPerformanceTime"]);
                checkCaptureWorkerPerformanceTime = Int32.Parse(
                    ConfigurationManager.AppSettings["checkCaptureWorkerPerformanceTime"]);
                checkAidaWorkerHDDTime = Int32.Parse(
                    ConfigurationManager.AppSettings["checkAidaWorkerHDDTime"]);
                checkCaptureWorkerHDDTime = Int32.Parse(
                    ConfigurationManager.AppSettings["checkCaptureWorkerHDDTime"]);
                checkCaptureHDDTime = Int32.Parse(
                    ConfigurationManager.AppSettings["checkCaptureHDDTime"]);
                checkStoppedServicesTime = Int32.Parse(
                    ConfigurationManager.AppSettings["checkStoppedServicesTime"]);
                checkFolderTime = Int32.Parse(
                    ConfigurationManager.AppSettings["checkFolderTime"]);
                dataConnection = ConfigurationManager.AppSettings["dataConnection"];
                ServerNameColumn = ConfigurationManager.AppSettings["serverNameColumn"];
                NumberOfPrio0Column = ConfigurationManager.AppSettings["numberOfPrio0Column"];
                TimestampColumn = ConfigurationManager.AppSettings["timestampColumn"];
                DatabaseTable = ConfigurationManager.AppSettings["databaseTable"];
                useMfIMonitor = bool.Parse(
                    ConfigurationManager.AppSettings["useMfIMonitor"]);

                driver = ConfigurationManager.AppSettings["driver"];
                server = ConfigurationManager.AppSettings["server"];
                dsn = ConfigurationManager.AppSettings["dsn"];
                database = ConfigurationManager.AppSettings["database"];
                CreateDataConnectionString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CreateDataConnectionString()
        {
            dataConnection = "Driver=" + Options.Driver + ";Server=" + Options.Server + ";DSN=" + Options.DSN + ";Trusted_Connection=Yes;Database=" + Options.Database;
        }

        /// <summary>
        /// returns a DataTabe with the information from the .csv file
        /// </summary>
        /// <param name="csv_file_path"></param>
        /// <returns></returns>
        public static DataTable GetDataTableFromCsV(string csv_file_path)
        {
            csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { ";" });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    //Read column names
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Source = "Options.GetDataTableFromCsV " + ex.Source;
                throw ex;
            }
            return csvData;
        }

        /// <summary>
        /// returns the path to the csv file for the server
        /// </summary>
        public static string PathServers
        {
            get
            {
                return pathServers;
            }
            set
            {
                pathServers = value;
                Options.SetConfig("ServersPath", pathServers);
            }
        }

        /// <summary>
        /// return the path to the csv file for the services
        /// </summary>
        public static string PathServices
        {
            get
            {
                return pathServices; 
            }
            set
            {
                pathServices = value;
                Options.SetConfig("ServicesPath", pathServices);
            }
        }

        /// <summary>
        /// returns the delay for the event log (in hours)
        /// </summary>
        public static Int16 EventLogDelay
        {
            get
            {
                return eventLogDelay;
            }
            set
            {
                eventLogDelay = value;
                Options.SetConfig("eventLogDelay", eventLogDelay.ToString());
            }
        }

        /// <summary>
        /// returns the back color for the capture panel
        /// </summary>
        public static Color BackColorCapturePanel
        {
            get
            {
                return backColorCapturePanel;
            }
            set
            {
                backColorCapturePanel = value;
                Options.SetConfig("backColorCapturePanel", backColorCapturePanel.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// returns the back color fpr the worker panel
        /// </summary>
        public static Color BackColorWorkerPanel
        {
            get
            {
                return backColorWorkerPanel;
            }
            set
            {
                backColorWorkerPanel = value;
                Options.SetConfig("backColorWorkerPanel", backColorWorkerPanel.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// returns the color for the server panels if the server is disconnected
        /// </summary>
        public static Color BackColorServerDisconnect
        {
            get
            {
                return backColorServerDisconnect;
            }
            set
            {
                backColorServerDisconnect = value;
                Options.SetConfig("backColorServerDisconnect", backColorServerDisconnect.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// returns the font color
        /// </summary>
        public static Color FontColor
        {
            get
            {
                return fontColor;
            }
            set
            {
                fontColor = value;
                Options.SetConfig("fontColor", fontColor.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// returns the back color fpr the service window
        /// </summary>
        public static Color BackColorServiceWindow
        {
            get
            {
                return backColorServiceWindow;
            }
            set
            {
                backColorServiceWindow = value;
                Options.SetConfig("backColorServiceWindow", backColorServiceWindow.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// returns the 2. back color for the service window (every 2. line)
        /// </summary>
        public static Color BackColorServiceWindow2
        {
            get
            {
                return backColorServiceWindow2;
            }
            set
            {
                backColorServiceWindow2 = value;
                Options.SetConfig("backColorServiceWindow2", backColorServiceWindow2.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// returns the grid color for the service window
        /// </summary>
        public static Color GridColorServiceWindow
        {
            get
            {
                return gridColorServiceWindow;
            }
            set
            {
                gridColorServiceWindow = value;
                Options.SetConfig("gridColorServiceWindow", gridColorServiceWindow.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// returns the back color for the prio window
        /// </summary>
        public static Color BackColorPrioWindow
        {
            get
            {
                return backColorPrioWindow;
            }
            set
            {
                backColorPrioWindow = value;
                Options.SetConfig("backColorPrioWindow", backColorPrioWindow.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// return the 2. back color for the prio window (every 2. line)
        /// </summary>
        public static Color BackColorPrioWindow2
        {
            get
            {
                return backColorPrioWindow2;
            }
            set
            {
                backColorPrioWindow2 = value;
                Options.SetConfig("backColorPrioWindow2", backColorPrioWindow2.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// returns the grid color for the prio window
        /// </summary>
        public static Color GridColorPrioWindow
        {
            get
            {
                return gridColorPrioWindow;
            }
            set
            {
                gridColorPrioWindow = value;
                Options.SetConfig("gridColorPrioWindow", gridColorPrioWindow.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// returns the font for the main window
        /// </summary>
        public static Font FontMainWindow
        {
            get
            {
                return fontMainWindow;
            }
            set
            {
                fontMainWindow = value;
                Options.SetConfig("fontMainWindowSize", fontMainWindow.Size.ToString());
                Options.SetConfig("fontMainWindowName", fontMainWindow.FontFamily.Name);
                Options.SetConfig("fontMainWindowStyle", ((int)(fontMainWindow.Style)).ToString());
            }
        }

        /// <summary>
        /// returns the font for theservice window
        /// </summary>
        public static Font FontServiceWindow
        {
            get
            {
                return fontServiceWindow;
            }
            set
            {
                fontServiceWindow = value;
                Options.SetConfig("fontServiceWindowSize", fontServiceWindow.Size.ToString());
                Options.SetConfig("fontServiceWindowName", fontServiceWindow.FontFamily.Name);
                Options.SetConfig("fontServiceWindowStyle", ((int)(fontServiceWindow.Style)).ToString());
            }
        }

        /// <summary>
        /// returns the font for the prio window
        /// </summary>
        public static Font FontPrioWindow
        {
            get
            {
                return fontPrioWindow;
            }
            set
            {
                fontPrioWindow = value;
                Options.SetConfig("fontPrioWindowSize", fontPrioWindow.Size.ToString());
                Options.SetConfig("fontPrioWindowName", fontPrioWindow.FontFamily.Name);
                Options.SetConfig("fontPrioWindowStyle", ((int)(fontPrioWindow.Style)).ToString());
            }
        }

        /// <summary>
        /// returns the event id to observe
        /// </summary>
        public static int EventIDPrio0
        {
            get
            {
                return eventIDPrio0;
            }
            set
            {
                eventIDPrio0 = value;
                Options.SetConfig("eventIDPrio0", eventIDPrio0.ToString());
            }
        }

        /// <summary>
        /// returns the number of the screen to display the dashboard
        /// </summary>
        public static int ScreenNumber
        {
            get
            {
                return screenNumber;
            }
            set
            {
                screenNumber = value;
                Options.SetConfig("ScreenNumber", screenNumber.ToString());
            }
        }

        /// <summary>
        /// returns the opacity of the service window
        /// </summary>
        public static double OpacityServiceWindow
        {
            get
            {
                return opacityServiceWindow;
            }
            set
            {
                opacityServiceWindow = value;
                Options.SetConfig("opacityServiceWindow", opacityServiceWindow.ToString());
            }
        }

        /// <summary>
        /// returns the opacity of the prio window
        /// </summary>
        public static double OpacityPrioWindow
        {
            get
            {
                return opacityPrioWindow;
            }
            set
            {
                opacityPrioWindow = value;
                Options.SetConfig("opacityPrioWindow", opacityPrioWindow.ToString());
            }
        }

        /// <summary>
        /// returns the time how often to check for the event e.g. for prio0
        /// </summary>
        public static int CheckPrio0Time
        {
            get
            {
                return checkPrio0Time;
            }
            set
            {
                checkPrio0Time = value;
                Options.SetConfig("checkPrio0Time", checkPrio0Time.ToString());
            }
        }

        /// <summary>
        /// returns the back color for the server panel by problems
        /// </summary>
        public static Color BackColorServerPanelByProblems
        {
            get
            {
                return backColorServerPanelByProblems;
            }
            set
            {
                backColorServerPanelByProblems = value;
                Options.SetConfig("backColorServerPanelByProblems", backColorServerPanelByProblems.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// returns whether the service window should be closed automatically if there are no more stopped services or not
        /// </summary>
        public static bool CloseServiceWindowAutomatic
        {
            get
            {
                return closeServiceWindowAutomatic;
            }
            set
            {
                closeServiceWindowAutomatic = value;
                Options.SetConfig("closeServiceWindowAutomatic", closeServiceWindowAutomatic.ToString());
            }
        }

        /// <summary>
        /// returns whether the prio window should be closed automatically if there are no more observed events or not
        /// </summary>
        public static bool ClosePrioWindowAutomatic
        {
            get
            {
                return closePrioWindowAutomatic;
            }
            set
            {
                closePrioWindowAutomatic = value;
                Options.SetConfig("closePrioWindowAutomatic", closePrioWindowAutomatic.ToString());
            }
        }

        /// <summary>
        /// returns the percentage when the color of the progress bar for ram ussage should be changed
        /// </summary>
        public static int RamWarningPercentage
        {
            get
            {
                return ramWarningPercentage;
            }
            set
            {
                ramWarningPercentage = value;
                Options.SetConfig("ramWarningPercentage", ramWarningPercentage.ToString());
            }
        }

        /// <summary>
        /// returns the percentage when the color of the progress bar for cpu ussage should be changed
        /// </summary>
        public static int CpuWarningPercentage
        {
            get
            {
                return cpuWarningPercentage;
            }
            set
            {
                cpuWarningPercentage = value;
                Options.SetConfig("cpuWarningPercentage", cpuWarningPercentage.ToString());
            }
        }

        /// <summary>
        /// returns the warning color for cpu ussage
        /// </summary>
        public static Color CpuWarningColor
        {
            get
            {
                return cpuWarningColor;
            }
            set
            {
                cpuWarningColor = value;
                Options.SetConfig("cpuWarningColor", cpuWarningColor.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// returns the warning color for ram ussage
        /// </summary>
        public static Color RamWarningColor
        {
            get
            {
                return ramWarningColor;
            }
            set
            {
                ramWarningColor = value;
                Options.SetConfig("ramWarningColor", ramWarningColor.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// returns the color for the progress bar for ram ussage
        /// </summary>
        public static Color RamColor
        {
            get
            {
                return ramColor;
            }
            set
            {
                ramColor = value;
                Options.SetConfig("ramColor", ramColor.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// returns the color for the progress bar for cpu ussage
        /// </summary>
        public static Color CpuColor
        {
            get
            {
                return cpuColor;
            }
            set
            {
                cpuColor = value;
                Options.SetConfig("cpuColor", cpuColor.ToArgb().ToString("X"));
            }
        }
        
        /// <summary>
        /// returns the warning color for hdd ussage
        /// </summary>
        public static Color HddWarningColor
        {
            get
            {
                return hddWarningColor;
            }
            set
            {
                hddWarningColor = value;
                Options.SetConfig("hddWarningColor", hddWarningColor.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// returns the percentage when the color of the hdd message should be changed
        /// </summary>
        public static int HddWarningPercentage
        {
            get
            {
                return hddWarningPercentage;
            }
            set
            {
                hddWarningPercentage = value;
                Options.SetConfig("hddWarningPercentage", hddWarningPercentage.ToString());
            }
        }

        /// <summary>
        /// returns the font for the fax window (observed paths)
        /// </summary>
        public static Font FontFaxWindow
        {
            get
            {
                return fontFaxWindow;
            }
            set
            {
                fontFaxWindow = value;
                Options.SetConfig("fontFaxWindowSize", fontFaxWindow.Size.ToString());
                Options.SetConfig("fontFaxWindowName", fontFaxWindow.FontFamily.Name);
                Options.SetConfig("fontFaxWindowStyle", ((int)(fontFaxWindow.Style)).ToString());
            }
        }

        /// <summary>
        /// returns the back color of the fax window
        /// </summary>
        public static Color BackColorFaxWindow
        {
            get
            {
                return backColorFaxWindow;
            }
            set
            {
                backColorFaxWindow = value;
                Options.SetConfig("backColorFaxWindow", backColorFaxWindow.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// returns the opacity of the fax window
        /// </summary>
        public static double OpacityFaxWindow
        {
            get
            {
                return opacityFaxWindow;
            }
            set
            {
                opacityFaxWindow = value;
                Options.SetConfig("opacityFaxWindow", opacityFaxWindow.ToString());
            }
        }

        /// <summary>
        /// returns the path to the csv file for the paths to observe
        /// </summary>
        public static string PathForPathToCheck
        {
            get
            {
                return pathForPathToCheck;
            }
            set
            {
                pathForPathToCheck = value;
                Options.SetConfig("PathsCheckingPath", pathForPathToCheck);
            }
        }

        /// <summary>
        /// returns the list of paths to observe
        /// </summary>
        public static List<PathToCheck> PathsToCheck
        {
            get
            {
                return pathsToCheck;
            }
        }

        /// <summary>
        /// returns the grid color of the fax window
        /// </summary>
        public static Color GridColorFaxWindow
        {
            get
            {
                return gridColorFaxWindow;
            }
            set
            {
                gridColorFaxWindow = value;
                Options.SetConfig("gridColorFaxWindow", gridColorFaxWindow.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// returns the 2. back color of the fax window (every 2. line=
        /// </summary>
        public static Color BackColorFaxWindow2
        {
            get
            {
                return backColorFaxWindow2;
            }
            set
            {
                backColorFaxWindow2 = value;
                Options.SetConfig("backColorFaxWindow2", backColorFaxWindow2.ToArgb().ToString("X"));
            }
        }

        /// <summary>
        /// returns whether the fax window should be closed if there is no problem in any observed path
        /// </summary>
        public static bool CloseFaxWindowAutomatic
        {
            get
            {
                return closeFaxWindowAutomatic;
            }
            set
            {
                closeFaxWindowAutomatic = value;
                Options.SetConfig("closeFaxWindowAutomatic", closeFaxWindowAutomatic.ToString());
            }
        }

        /// <summary>
        /// returns the time how often to check the performance for captures
        /// </summary>
        public static int CheckCapturePerformanceTime
        {
            get
            {
                return checkCapturePerformanceTime;
            }
            set
            {
                checkCapturePerformanceTime = value;
                Options.SetConfig("checkCapturePerformanceTime", checkCapturePerformanceTime.ToString());
            }
        }

        /// <summary>
        /// returns the time how often to check the performance for aida workers
        /// </summary>
        public static int CheckAidaWorkerPerformanceTime
        {
            get
            {
                return checkAidaWorkerPerformanceTime;
            }
            set
            {
                checkAidaWorkerPerformanceTime = value;
                Options.SetConfig("checkAidaWorkerPerformanceTime", checkAidaWorkerPerformanceTime.ToString());
            }
        }

        /// <summary>
        /// returns the time how often to check the performance for aida workers
        /// </summary>
        public static int CheckCaptureWorkerPerformanceTime
        {
            get
            {
                return checkCaptureWorkerPerformanceTime;
            }
            set
            {
                checkCaptureWorkerPerformanceTime = value;
                Options.SetConfig("checkCaptureWorkerPerformanceTime", checkCaptureWorkerPerformanceTime.ToString());
            }
        }



        /// <summary>
        /// returns the time how often to check the hdd for captures
        /// </summary>
        public static int CheckCaptureHDDTime
        {
            get
            {
                return checkCaptureHDDTime;
            }
            set
            {
                checkCaptureHDDTime = value;
                Options.SetConfig("checkCaptureHDDTime", checkCaptureHDDTime.ToString());
            }
        }

        /// <summary>
        /// returns the time how often to check the hdd for aida workers
        /// </summary>
        public static int CheckAidaWorkerHDDTime
        {
            get
            {
                return checkAidaWorkerHDDTime;
            }
            set
            {
                checkAidaWorkerHDDTime = value;
                Options.SetConfig("checkAidaWorkerHDDTime", checkAidaWorkerHDDTime.ToString());
            }
        }

        /// <summary>
        /// returns the time how often to check the hdd for aida workers
        /// </summary>
        public static int CheckCaptureWorkerHDDTime
        {
            get
            {
                return checkCaptureWorkerHDDTime;
            }
            set
            {
                checkCaptureWorkerHDDTime = value;
                Options.SetConfig("checkCaptureWorkerHDDTime", checkCaptureWorkerHDDTime.ToString());
            }
        }

        /// <summary>
        /// returns how often to check for stopped services
        /// </summary>
        public static int CheckStoppedServicesTime
        {
            get
            {
                return checkStoppedServicesTime;
            }
            set
            {
                checkStoppedServicesTime = value;
                Options.SetConfig("checkStoppedServicesTime", checkStoppedServicesTime.ToString());
            }
        }

        /// <summary>
        /// returns how often to check the observed paths
        /// </summary>
        public static int CheckFolderTime
        {
            get
            {
                return checkFolderTime;
            }
            set
            {
                checkFolderTime = value;
                Options.SetConfig("checkFolderTime", checkFolderTime.ToString());
            }
        }

        /// <summary>
        /// retrurns the path to the log file
        /// </summary>
        public static string LogFilePath
        {
            get
            {
                return logFilePath;
            }
            set
            {
                logFilePath = value;
                Options.SetConfig("logFilePath", logFilePath);
            }
        }

        /// <summary>
        /// gives back whether the threads should running or not
        /// </summary>
        public static bool RunThreads
        {
            get
            {
                return runThreads;
            }
            set
            {
                runThreads = value;
            }
        }

        /// <summary>
        /// gives back the dataConnection string for the database connection
        /// </summary>
        public static string DataConnection
        {
            get
            {
                return dataConnection;
            }
            //set
            //{
            //    dataConnection = value;
            //    Options.SetConfig("dataConnection", dataConnection.ToString());
            //}
        }

        /// <summary>
        /// gives back whether MfIMonitor should be used for checking for Prio0 or not
        /// </summary>
        public static bool UseMfIMonitor
        {
            get
            {
                return useMfIMonitor;
            }
            set
            {
                useMfIMonitor = value;
                Options.SetConfig("useMfIMonitor", useMfIMonitor.ToString());
            }
        }

        /// <summary>
        /// returns the name of the column for the servername
        /// </summary>
        public static string ServerNameColumn
        {
            get
            {
                return serverNameColumn;
            }

            set
            {
                serverNameColumn = value;
                Options.SetConfig("serverNameColumn", serverNameColumn);
            }
        }

        /// <summary>
        /// returns the name of the column NumberOfPrio0
        /// </summary>
        public static string NumberOfPrio0Column
        {
            get
            {
                return numberOfPrio0Column;
            }

            set
            {
                numberOfPrio0Column = value;
                Options.SetConfig("numberOfPrio0Column", numberOfPrio0Column);
            }
        }


        /// <summary>
        /// returns the name of the column timestamp
        /// </summary>
        public static string TimestampColumn
        {
            get
            {
                return timestampColumn;
            }

            set
            {
                timestampColumn = value;
                Options.SetConfig("timestampColumn", timestampColumn);
            }
        }


        /// <summary>
        /// returns the name of the database
        /// </summary>
        public static string DatabaseTable
        {
            get
            {
                return databaseTable;
            }

            set
            {
                databaseTable = value;
                Options.SetConfig("databaseTable", databaseTable);
            }
        }

        /// <summary>
        /// returns the driver used for the database query (MfIMonitor)
        /// </summary>
        public static string Driver
        {
            get
            {
                return driver;
            }
            set
            {
                driver = value;
                Options.SetConfig("driver", driver);
                Options.CreateDataConnectionString();
            }
        }

        /// <summary>
        /// returns the server for the database query (MfIMonitor)
        /// </summary>
        public static string Server
        {
            get
            {
                return server;
            }
            set
            {
                server = value;
                Options.SetConfig("server", server);
                Options.CreateDataConnectionString();
            }
        }

        /// <summary>
        /// returns the dsn (data source name) for the database query (MfIMonitor)
        /// </summary>
        public static string DSN
        {
            get
            {
                return dsn;
            }
            set
            {
                dsn = value;
                Options.SetConfig("dsn", dsn);
                Options.CreateDataConnectionString();
            }
        }

        /// <summary>
        /// returns the database for the database query (MfIMonitor)
        /// </summary>
        public static string Database
        {
            get
            {
                return database;
            }
            set
            {
                database = value;
                Options.SetConfig("database", database);
                Options.CreateDataConnectionString();
            }
        }

        /// <summary>
        /// set the size of the font for the main window
        /// </summary>
        public static int FontMainWindowSize
        {
            set
            {
                Options.SetConfig("fontMainWindowSize", value.ToString());
            }
        }

        /// <summary>
        /// set the size of the font for the service window
        /// </summary>
        public static int FontServiceWindowSize
        {
            set
            {
                Options.SetConfig("fontServiceWindowSize", value.ToString());
            }
        }

        /// <summary>
        /// set the size of the font for the prio window
        /// </summary>
        public static int FontPrioWindowSize
        {
            set
            {
                Options.SetConfig("fontPrioWindowSize", value.ToString());
            }
        }

        /// <summary>
        /// set the size of the font for the fax window
        /// </summary>
        public static int FontFaxWindowSize
        {
            set
            {
                Options.SetConfig("fontFaxWindowSize", value.ToString());
            }
        }

        /// <summary>
        /// set the name of the font for the main window
        /// </summary>
        public static string FontMainWindowName
        {
            set
            {
                Options.SetConfig("fontMainWindowName", value);
            }
        }

        /// <summary>
        /// set the name of the font for the service window
        /// </summary>
        public static string FontServiceWindowName
        {
            set
            {
                Options.SetConfig("fontServiceWindowName", value);
            }
        }

        /// <summary>
        /// set the name of the font for the prio window
        /// </summary>
        public static string FontPrioWindowName
        {
            set
            {
                Options.SetConfig("fontPrioWindowName", value);
            }
        }

        /// <summary>
        /// set the name of the font for the fax window
        /// </summary>
        public static string FontFaxWindowName
        {
            set
            {
                Options.SetConfig("fontFaxWindowName", value);
            }
        }

        /// <summary>
        /// set the style of the font for the main window
        /// </summary>
        public static int FontMainWindowStyle
        {
            set
            {
                Options.SetConfig("fontMainWindowStyle", value.ToString());
            }
        }

        /// <summary>
        /// set the style of the font for the service window
        /// </summary>
        public static int FontServiceWindowStyle
        {
            set
            {
                Options.SetConfig("fontServiceWindowStyle", value.ToString());
            }
        }

        /// <summary>
        /// set the style of the font for the prio window
        /// </summary>
        public static int FontPrioWindowStyle
        {
            set
            {
                Options.SetConfig("fontPrioWindowStyle", value.ToString());
            }
        }

        /// <summary>
        /// set the style of the font for the fax window
        /// </summary>
        public static int FontFaxWindowStyle
        {
            set
            {
                Options.SetConfig("fontFaxWindowStyle", value.ToString());
            }
        }


    }
}
