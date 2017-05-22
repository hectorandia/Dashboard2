using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// Class to have all information for one HDD together in one place
    /// </summary>
    public class HDDLabel
    {
        private Label device;
        private Label message;

        /// <summary>
        /// creates an instance of an HDD
        /// </summary>
        /// <param name="deviceText"></param>
        /// <param name="messageText"></param>
        public HDDLabel(string deviceText, string messageText)
        {
            this.device = FactoryPanels.CreateLabel(deviceText);
            this.message = FactoryPanels.CreateLabel(messageText);
        }

        /// <summary>
        /// returns the label for the name of the HDD
        /// </summary>
        public Label Device
        {
            get
            {
                return device;
            }
        }

        /// <summary>
        /// returns the label with the message of this hdd
        /// </summary>
        public Label Message
        {
            get
            {
                return message;
            }
        }

        /// <summary>
        /// returns the name of this hdd
        /// </summary>
        public string DeviceText
        {
            get
            {
                return device.Text;
            }
            set
            {
                device.Text = value;
            }
        }

        /// <summary>
        /// returns the message of this hdd
        /// </summary>
        public string MessageText
        {
            get
            {
                return message.Text;
            }
            set
            {
                message.Text = value;
            }
        }


    }
}
