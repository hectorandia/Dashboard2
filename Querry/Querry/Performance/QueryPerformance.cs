using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Query
{
    /// <summary>
    /// abstract class for performing querries e.g. cpu or ram ussage
    /// </summary>
    public abstract class QueryPerformance : AQuery
    {
        /// <summary>
        /// refreshs the current ussage
        /// </summary>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public abstract int getCurrentUssage(string serverName);


        /// <summary>
        /// refreshs the ip
        /// </summary>
        public void GetIpAddress()
        {
            string localComputerName = Dns.GetHostName();
            IPAddress[] localIps = Dns.GetHostAddresses(Dns.GetHostName());

            Console.WriteLine(localComputerName);
            foreach (IPAddress ip in localIps)
            {
                Console.WriteLine(ip);

            }

        }

        /// <summary>
        /// Convert byte to GB or MB and return a String whith the size info
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string ToFileSizeString(double source)
        {
            const int byteConversion = 1024;
            double bytes = Convert.ToDouble(source);

            if (bytes >= Math.Pow(byteConversion, 3)) //GB range
            {
                return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 3), 2), "GB");
            }
            else if (bytes >= Math.Pow(byteConversion, 2)) //MB range
            {
                return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 2), 2), "MB");
            }
            else
            {
                return string.Concat(bytes, "Bytes");
            }
        }



        /// <summary>
        /// Convert byte to GB or MB and return a Integer whith the size info
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public int ToFileSizeInt(double source)
        {
            const int byteConversion = 1024;
            double bytes = Convert.ToDouble(source);

            if (bytes >= Math.Pow(byteConversion, 3)) //GB range
            {
                return Convert.ToInt32(Math.Round(bytes / Math.Pow(byteConversion, 3), 2));
            }
            else if (bytes >= Math.Pow(byteConversion, 2)) //MB range
            {
                return Convert.ToInt32(Math.Round(bytes / Math.Pow(byteConversion, 2), 2));
            }
            else
            {
                return Convert.ToInt16(bytes);
            }
        }



        /// <summary>
        /// Convert byte to GB or MB and return a Double whith the size info
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public double ToFileSizeDouble(double source)
        {
            const int byteConversion = 1024;
            double bytes = Convert.ToDouble(source);

            if (bytes >= Math.Pow(byteConversion, 3)) //GB range
            {
                return (Math.Round(bytes / Math.Pow(byteConversion, 3), 2));
            }
            else if (bytes >= Math.Pow(byteConversion, 2)) //MB range
            {
                return (Math.Round(bytes / Math.Pow(byteConversion, 2), 2));
            }
            else
            {
                return bytes;
            }
        }

    }
}
