using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// specific OptionPanel for color options
    /// </summary>
    public class OptionColor : OptionPanel
    {

        private ColorDialogPanel dialogPanel;

        /// <summary>
        /// creates an instance of OptionColor
        /// </summary>
        public OptionColor()
        {
            dialogPanel = new ColorDialogPanel();

            this.AddPanel(dialogPanel);

            this.Width = OptionWindow.WidthOptionElements;

            this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        }


        /// <summary>
        /// saves all the data which belongts to this OptionPanel
        /// </summary>
        public override void SaveData()
        {
            dialogPanel.SaveData();
        }
    }
}
