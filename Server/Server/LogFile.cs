using Server;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    /// <summary>
    /// class to write log file
    /// </summary>
    public class LogFile
    {

        private static LogFile instance = null;
        private static StreamWriter writer; 
        private static string path;
        private static bool active = true;
        private static Object lockLog = new Object();

        DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
        

        private LogFile()
        {
            setPath();
        }


        private void setPath()
        {
            path = Options.LogFilePath;
            if (!Directory.Exists(Options.LogFilePath))
            {
                Directory.CreateDirectory(path);
            }
            Calendar cal = dfi.Calendar;
            path = path + "KW" + cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule,
                                          dfi.FirstDayOfWeek) + ".log";
        }

        /// <summary>
        /// returns the instance of the logFile
        /// </summary>
        public static LogFile Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogFile();
                }
                return instance;
            }
        }

        /// <summary>
        /// writes the message to the log file
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            if (!active)
            {
                return;
            }

            lock (lockLog)
            {
                try
                {
                    writer = File.AppendText(path);
                    writer.WriteLine(DateTime.Now.ToString("ddd H:mm:ss") + " -> " + message);
                    writer.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error beim Schreiben folgender Message in die log-Datei: "
                        + message + " Exception: " + ex.Message);
                }
            }      
        }

        /// <summary>
        /// sets the log writer inactive
        /// </summary>
        public static void setInactive()
        {
            active = false;
        }

    }
}
