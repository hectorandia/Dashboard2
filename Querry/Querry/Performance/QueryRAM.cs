using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Query
{
    /// <summary>
    /// class for query the ram ussage
    /// </summary>
    public class QueryRAM : QueryPerformance
    {
        private PerformanceCounter memoryAvailable;
        private PerformanceCounter memoryUsedPercentage;

        /// <summary>
        /// returns the current ussage of the ram
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public override int getCurrentUssage(string serverName)
        {
            int value = 0;
            try
            {
                memoryUsedPercentage = new PerformanceCounter("Memory", "% Committed Bytes In Use", string.Empty, serverName);
                value = ToFileSizeInt(memoryUsedPercentage.NextValue());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return value;
        }

        /// <summary>
        /// returns 
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public double GetMemoryAvailable(String serverName)
        {
            memoryAvailable = new PerformanceCounter("Memory", "Available Bytes", string.Empty, serverName);
            return ToFileSizeDouble(memoryAvailable.NextValue());
        }

        /*
         * Use WMI to get Computer System information
         * 
         * classObj: ComputerSystem
         * value: TotalPhysicalMemory = Total ram;
         * */
         /// <summary>
         /// returns the total physical memory
         /// </summary>
         /// <param name="serverName"></param>
         /// <param name="value"></param>
         /// <returns></returns>
        public double GetTotalPhysicalMemory(string serverName, string value)
        {
            
            double totalMemory = 0.0;
            try
            {
            ManagementScope connection = ConnectToServer(serverName); //connects to the server

            ObjectQuery query = new ObjectQuery("Select * from Win32_ComputerSystem"); //the question is created
            
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(connection, query);

                foreach (ManagementObject search in searcher.Get())
                {
                    totalMemory = ToFileSizeDouble(Convert.ToDouble(search[value]));
                }

                
                
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return totalMemory;
        }



    }
}
