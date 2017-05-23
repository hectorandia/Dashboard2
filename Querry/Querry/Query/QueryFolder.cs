using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Query
{
    /// <summary>
    /// class to check for folder status
    /// </summary>
    public class QueryFolder : AQuery
    {
        private List<FileInfo> files; //saves all names of all files that are in the folder Import
        private DirectoryInfo dirInfo;
        private Int16 maximumFileTime;
        private DirectoryInfo directory;
        private string path;
        private DateTime oldestFileTime = DateTime.Now.AddYears(999);

        /// <summary>
        /// returns the path for the query
        /// </summary>
        public string Path
        {
            get
            {
                return path;
            }
        }

        /// <summary>
        /// returns an instance of QueryFolder
        /// </summary>
        /// <param name="path"></param>
        /// <param name="maximumFileTime"></param>
        public QueryFolder(string path, Int16 maximumFileTime)
        {
            files = new List<FileInfo>();
            this.path = path;
            this.maximumFileTime = maximumFileTime;         
        }

        /// <summary>
        /// set the oldestFileTime back
        /// </summary>
        public void SetOldestTimeBack()
        {
            oldestFileTime = DateTime.Now.AddYears(999);
        }

        /**
         * Save a List the files in the folder Import
         * Returns a list with all the files found in the directory "path"
         * */
        private void GetDirectoryInfo()
        {
            dirInfo = new DirectoryInfo(path);

            try
            {
                FileInfo[] filNames = dirInfo.GetFiles("*.*"); //All files are considered

                files.Clear();
                foreach (FileInfo file in filNames)
                {
                    files.Add(file);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /**
         * Checks if a file takes more time than allowed in a directory, 
         * comparing the value returned by the CreationTime method of the FileInfo class
         * 
         * @DateTime time: Indicates the maximum time a file can be in the directory
         * @return true: When the file is within the allowed time
         * @return falso: When the file has exceeded the maximum time
         * */
        private bool CheckFileTime(DateTime time)
        {
            if (time < oldestFileTime)
            {
                oldestFileTime = time;
            }
            if (time <= DateTime.Now.AddMinutes(-maximumFileTime)) //Compares the creation time of the file with the current time minus maximumFileTime
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckFileTime(FileInfo file)
        {
            if (file.CreationTime <= DateTime.Now.AddMinutes(-maximumFileTime))
            {
                return false;
            }
            return true;
        }

        private bool CheckDirectoryTime(DirectoryInfo directory)
        {
            if (directory.CreationTime < oldestFileTime)
            {
                oldestFileTime = directory.CreationTime;
            }

            if (directory.CreationTime < DateTime.Now.AddMinutes(-maximumFileTime)) 
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// check for files
        /// returns true if everything is fine
        /// </summary>
        /// <returns></returns>
        public bool CheckFileTimeFunction()
        {
            bool check = true;

            try
            {
                GetDirectoryInfo();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            foreach (FileInfo file in files)
            {
                if (!CheckFileTime(file.CreationTime))
                {
                    check = false;
                }
                
            }
            return check;
        }



        /**
         * Verify that a directory does not contain any subdirectory or file within the directory.
         * 
         * @string path: Directory to control
         * @return true: When the directory is empty
         * @return false: When a subdirectory is found 
         * */
        public bool CheckFolderTime()
        {
            try
            {
                directory = new DirectoryInfo(path);

                foreach (DirectoryInfo directoryy in directory.GetDirectories("*.*"))
                {
                    if (!CheckDirectoryTime(directoryy))
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
            return true;
        }


        /// <summary>
        /// returns the maximum file time before warning
        /// </summary>
        public Int16 MaximumFileTime
        {
            get
            {
                return maximumFileTime;
            }
        }

        /// <summary>
        /// returns the number of files which are older than allowed
        /// </summary>
        /// <returns></returns>
        public int NumberOfFiles()
        {
            int number = 0;

            foreach (FileInfo file in files)
            {
                if (!CheckFileTime(file))
                {
                    number++;
                }
            }
            MessageBox.Show("Number of Files: " + number.ToString());
            return number;
        }

        /// <summary>
        /// returns the number of directories which are older than allowed
        /// </summary>
        /// <returns></returns>
        public int NumberOfDirectories()
        {
            int number = 0;
            
            foreach (DirectoryInfo director in directory.GetDirectories("*.*"))
            {
                MessageBox.Show("Number of Directorys: " + number.ToString());
                if (!CheckDirectoryTime(director))
                {
                    number++;
                }
            }
            
            return number;
        }

        /// <summary>
        /// returns the time stamp of the oldest file/directory
        /// </summary>
        public string OldestDateTime
        {
            get
            {
                return oldestFileTime.ToString("ddd HH:mm:ss");
            }
        }

    }
}
