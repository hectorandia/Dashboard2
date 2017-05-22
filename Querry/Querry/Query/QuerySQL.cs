using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;

namespace Query
{
    /// <summary>
    /// class to make a query to the database with the prio0 information
    /// </summary>
    public class QuerySQL
    {

        private static OdbcConnection connection;
        private static QuerySQL instance = null;
        private static List<IEventObject> prio0 = new List<IEventObject>();
        private static string statement;


        private QuerySQL(SQLSetting sqlSetting)
        {
            QuerySQL.connection = new OdbcConnection(sqlSetting.DataConnection);
            statement = "SELECT " + sqlSetting.ServerNameColumn + ", " + 
                sqlSetting.NumberOfPrio0Column + ", " + sqlSetting.TimestampColumn + " FROM " + sqlSetting.Database;
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


        /// <summary>
        /// returns the instance of QuerySQL
        /// </summary>
        /// <param name="sqlSetting"></param>
        /// <returns></returns>
        public static QuerySQL Instance(SQLSetting sqlSetting)
        {
            if (instance == null)
            {
                instance = new QuerySQL(sqlSetting);                    
            }
            return instance;
        }

        private OdbcDataReader SQLStatement()
        {
            OdbcCommand command = new OdbcCommand(statement, connection);    
            OdbcDataReader reader = command.ExecuteReader();
            return reader;
        }

        private void filter() {
            for (int i = 0; i < prio0.Count; i++)
            {
                if (prio0[i].Number==0)
                {
                    prio0.Remove(prio0[i]);
                }
            }
            
        }


        private void transfer(OdbcDataReader reader)
        {
            prio0.Clear();
            while (reader.Read())
            {
                prio0.Add(new EventObjectMfI(reader.GetString(0), Int32.Parse(reader.GetString(1)), reader.GetString(2)));
            }
        }

        /// <summary>
        /// gives back a list of prio0 from the database (only this entries which have at most one Prio0)
        /// </summary>
        /// <returns></returns>
        public List<IEventObject> getPrio0()
        {
            OdbcDataReader reader = SQLStatement();
            transfer(reader);
            filter();
            return prio0;
        }



    }
}
