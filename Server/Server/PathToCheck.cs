using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Query;

namespace Server
{
    /// <summary>
    /// class to hold all information about an observed path on one place
    /// </summary>
    public class PathToCheck
    {
        private string path;
        private bool checkForfiles;
        private bool checkForDirectories;
        private Int16 timeMinutes;
        private QueryFolder queryFolder;
        private bool statusOk = true;
        private static int numberOfFiles = 0;

        /// <summary>
        /// creates an instance of PathToCheck
        /// </summary>
        /// <param name="path"></param>
        /// <param name="timeMinutes"></param>
        /// <param name="checkForfiles"></param>
        /// <param name="checkForDirectories"></param>
        public PathToCheck(string path, Int16 timeMinutes, bool checkForfiles, bool checkForDirectories)
        {
            this.path = path;
            this.timeMinutes = timeMinutes;
            this.checkForfiles = checkForfiles;
            this.checkForDirectories = checkForDirectories;
        }

        /// <summary>
        /// returns whether the status is ok or not
        /// </summary>
        public bool StatusOk
        {
            get
            {
                return statusOk;
            }
        }

        /// <summary>
        /// returns whether the status of the observed path has changed or not
        /// </summary>
        /// <returns></returns>
        public bool CheckStatusChange()
        {
            try
            {
                if (statusOk && checkForfiles && !CheckFile())
                {
                    this.statusOk = false;
                    numberOfFiles = GetNumberOfFiles();
                    if (numberOfFiles > 0)
                    {
                        LogFile.Log(numberOfFiles + " Datei(en) in überwachten Ordnern detektiert: " + path);
                    }
                    return true;
                }

                if (statusOk && checkForDirectories && !CheckDirectories())
                {
                    this.statusOk = false;
                    numberOfFiles = GetNumberOfFiles();
                    if (numberOfFiles > 0)
                    {
                        LogFile.Log(numberOfFiles + " Datei(en) in überwachten Ordnern detektiert: " + path);
                    }
                    return true;
                }

                if (!statusOk && checkForfiles && CheckFile())
                {
                    if ((checkForDirectories && CheckDirectories()) || !checkForDirectories)
                    {
                        this.statusOk = true;
                        queryFolder.SetOldestTimeBack();
                        LogFile.Log("Kein Problem mehr in überwachten Ordnern detektiert: " + path);
                        return true;
                    }
                }

                if (!statusOk && checkForDirectories && CheckDirectories())
                {
                    if ((checkForfiles && CheckFile()) || !checkForfiles)
                    {
                        this.statusOk = true;
                        queryFolder.SetOldestTimeBack();
                        LogFile.Log("Kein Problem mehr in überwachten Ordnern detektiert: " + path);
                        return true;
                    }
                }

                if (numberOfFiles != GetNumberOfFiles() && !statusOk)
                {
                    numberOfFiles = GetNumberOfFiles();
                    LogFile.Log(numberOfFiles + " Datei(en) in überwachten Ordnern detektiert: " + path);
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogFile.Log("Error in PathToCheck.CheckStatusChange: " + ex.Source + " " + ex.Message);
            }
            
            return false;
        }

        /// <summary>
        /// returns teh number of files which are older than the tolerance time from the csv file
        /// </summary>
        public int NumberOfFiles
        {
            get
            {
                return numberOfFiles;
            }
        }

        private int GetNumberOfFiles()
        {
            int number = 0;
            try
            {
                if (checkForfiles)
                {
                    number += queryFolder.NumberOfFiles();
                }

                if (checkForDirectories)
                {
                    number += queryFolder.NumberOfDirectories();
                }
            }
            catch (Exception)
            {
                number = -1;
            }
            

            return number;
        }

        /// <summary>
        /// returns the time of the oldest file or directory
        /// </summary>
        /// <returns></returns>
        public string OldestDateTime()
        {
            return queryFolder.OldestDateTime;
        }

        private bool CheckDirectories()
        {
            try
            {
                return queryFolder.CheckFolderTime();
            }
            catch (Exception)
            {
                if (statusOk)
                {
                    LogFile.Log("Pfad-Abfrage gescheitert: " + path);
                }
                return false;
            }
        }

        private bool CheckFile()
        {
            try
            {
                return queryFolder.CheckFileTimeFunction();
            }
            catch (Exception)
            {
                if (statusOk)
                {
                    LogFile.Log("Pfad-Abfrage gescheitert: " + path);
                }
                return false;
            }
            
        }

        /// <summary>
        /// returns or sets the instance of QueryFolder
        /// </summary>
        public QueryFolder QueryFolder
        {
            get
            {
                return queryFolder;
            }
            set
            {
                queryFolder = value;
            }
        }

        /// <summary>
        /// returns the observed path
        /// </summary>
        public string Path
        {
            get
            {
                return path;
            }
        }

        /// <summary>
        /// returns the time how old the file/directory may be before warning
        /// </summary>
        public Int16 TimeMinutes
        {
            get
            {
                return timeMinutes;
            }
        }


    }
}
