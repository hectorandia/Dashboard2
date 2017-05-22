using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query
{
    /// <summary>
    /// class to have all information about the database in one place
    /// </summary>
    public class SQLSetting
    {
        private string dataConnection = "";
        private string serverNameColumn = "";
        private string numberOfPrio0Column = "";
        private string timestampColumn = "";
        private string database = "";

        /// <summary>
        /// get or set the dataconnection string
        /// </summary>
        public string DataConnection
        {
            get
            {
                return dataConnection;
            }
            set
            {
                dataConnection = value;
            }
        }


        /// <summary>
        /// get or set the name of the server name column
        /// </summary>
        public string ServerNameColumn
        {
            get
            {
                return serverNameColumn;
            }

            set
            {
                serverNameColumn = value;
            }
        }

        /// <summary>
        /// get or set the name of the column in which the number od prio0 is stored
        /// </summary>
        public string NumberOfPrio0Column
        {
            get
            {
                return numberOfPrio0Column;
            }

            set
            {
                numberOfPrio0Column = value;
            }
        }

        /// <summary>
        /// get or set the name of the column in which the timestamp is stored
        /// </summary>
        public string TimestampColumn
        {
            get
            {
                return timestampColumn;
            }

            set
            {
                timestampColumn = value;
            }
        }

        /// <summary>
        /// get or set the name of the database
        /// </summary>
        public string Database
        {
            get
            {
                return database;
            }

            set
            {
                database = value;
            }
        }


    }
}
