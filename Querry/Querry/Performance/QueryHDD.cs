using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Query
{
    /// <summary>
    /// class for query the hdd ussage
    /// </summary>
    public class QueryHDD : QueryPerformance
    {

        private List<HDD> hdds = new List<HDD>();
        private double freeSpace;
        private double space;

        /// <summary>
        /// refreshs the current ussage
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public override int getCurrentUssage(string serverName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// returns a list of HDD with the information about the hdd
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public List<HDD> GetLogicalDiskInfo(string serverName)
        {
             try
             {
                 ManagementScope connection = ConnectToServer(serverName);

                 ObjectQuery query = new ObjectQuery("Select * from Win32_LogicalDisk where DriveType = 3"); // 

                 hdds.Clear();
                 ManagementObjectSearcher searcher = new ManagementObjectSearcher(connection, query);

                 ManagementObjectCollection x = searcher.Get();
                 foreach (ManagementObject search in x)
                 {
                    freeSpace = Convert.ToDouble(search["Freespace"]);
                    space = Convert.ToDouble(search["Size"]);
                    hdds.Add(new HDD( (int)((space - freeSpace) / space * 100) ,
                        search["DeviceID"] + "\\ ",
                        ToFileSizeString(freeSpace) + " frei von " + ToFileSizeString(space)));
                 }
             }
             catch (Exception ex)
             {
                 throw ex;             
             }
            return hdds;
        }


    }
}
