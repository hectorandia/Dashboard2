using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// class for option controls
    /// </summary>
    public abstract class OptionPanel : FlowLayoutPanel
    {


        /// <summary>
        /// creates an instance of OptionPanel
        /// </summary>
        protected OptionPanel() : base()
        {
            this.AutoScroll = true;
        }

        /// <summary>
        /// adds a panel to this OptionPanel
        /// </summary>
        /// <param name="panel"></param>
        public void AddPanel(TableLayoutPanel panel)
        {
            this.Controls.Add(panel);
        }

        /// <summary>
        /// save the actual data of this option control
        /// </summary>
        public abstract void SaveData();

    }
}
