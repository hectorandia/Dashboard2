using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Query
{
    /// <summary>
    /// class for query the cpu ussage
    /// </summary>
    public class QueryCPU : QueryPerformance
    {

        private PerformanceCounter cpuCounter;

        /// <summary>
        /// returns the current ussage of the cpu
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public override int getCurrentUssage(string serverName)
        {
            int value = 0;
            try
            {
                cpuCounter = new PerformanceCounter("Processor information", "% Processor Time", "_Total", serverName);
                cpuCounter.NextValue();
                System.Threading.Thread.Sleep(500);
                value = Convert.ToInt16(cpuCounter.NextValue());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return value;
        }

    }
}
