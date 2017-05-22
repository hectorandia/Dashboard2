using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query
{
    /// <summary>
    /// class to hold all information baout a hdd in one place
    /// </summary>
    public class HDD
    {
        private int ussage;
        private string device;
        private string message;

        /// <summary>
        /// returns an instance of HDD
        /// </summary>
        /// <param name="ussage"></param>
        /// <param name="device"></param>
        /// <param name="message"></param>
        public HDD(int ussage,string device, string message)
        {
            this.ussage = ussage;
            this.device = device;
            this.message = message;
        }

        /// <summary>
        /// returns the actual ussage of this hdd in percent (0-100)
        /// </summary>
        public int Ussage
        {
            get
            {
                return ussage;
            }
        }

        /// <summary>
        /// returns the information about this hdd in form of a string
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }
        }

        /// <summary>
        /// returns the name of the hdd e.g. C:/
        /// </summary>
        public string Device
        {
            get
            {
                return device;
            }
        }


    }
}
