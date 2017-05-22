using Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// abstract class for status panels
    /// </summary>
    public abstract class StatusPanel : InfoPanel
    {
        /// <summary>
        /// label with the information to show on this panel
        /// </summary>
        protected Label infoLabel;

        private delegate void SetInfoLabelCallBack(string text);
        
        /// <summary>
        /// sets the info label on this panel 
        /// </summary>
        /// <param name="text"></param>
        protected void SetInfoLabel(string text)
        {
            if (this.infoLabel.InvokeRequired)
            {
                SetInfoLabelCallBack delegater = new SetInfoLabelCallBack(SetInfoLabel);
                this.infoLabel.Invoke(delegater, new object[] { text });
            }
            else
            {
                infoLabel.Text = text;
            }
        }

        /// <summary>
        /// constructor of status panel
        /// </summary>
        /// <param name="server"></param>
        /// <param name="serverPanel"></param>
        /// <param name="name"></param>
        protected StatusPanel(ServerRZ server, ServerPanel serverPanel, Label name) : base(server, serverPanel)
        {
            this.ColumnCount = 2;
            this.RowCount = 1;


            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85));
            this.Size = new System.Drawing.Size(FactoryPanels.PanelWidth, FactoryPanels.PanelHeight / serverPanel.RowCount);

            this.infoLabel = FactoryPanels.CreateLabel(" ");
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.panelName = name;
            this.PanelName.TextAlign = ContentAlignment.MiddleLeft;

            this.Controls.Add(this.PanelName, 0, 0);
            this.Controls.Add(this.infoLabel, 1, 0);

            this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        }

    }
}
