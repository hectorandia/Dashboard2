using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// class for status panel for server name
    /// </summary>
    public class StatusPanelServerName : StatusPanel
    {
        /// <summary>
        /// Creates an instance of a StatusPanelServerName
        /// </summary>
        /// <param name="server"></param>
        /// <param name="serverPanel"></param>
        public StatusPanelServerName(ServerRZ server, ServerPanel serverPanel)
            :  base(server, serverPanel, FactoryPanels.CreateLabel(""))
        {
            this.ColumnCount = 1;
            this.RowCount = 1;

            this.Size = new System.Drawing.Size(FactoryPanels.PanelWidth, FactoryPanels.PanelHeight / serverPanel.RowCount);

            this.infoLabel = FactoryPanels.CreateLabel(" ");
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.Controls.Add(this.infoLabel, 0, 0);

            this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

        }

        /// <summary>
        /// updates the info shown on this panel
        /// </summary>
        public override void ServerUpdate()
        {
            //serverPanel.MyState.CheckConnection(serverPanel);
            myState.PanelUpdate(this);
        }

        /// <summary>
        /// upfate info (help function)
        /// </summary>
        public override void UpdateInfo()
        {
            SetInfoLabel(server.ServerIP);
        }

        private delegate void SetInfoLabelCallBack(string text);

        private new void SetInfoLabel(string text)
        {
            if (this.infoLabel.InvokeRequired)
            {
                SetInfoLabelCallBack delegater = new SetInfoLabelCallBack(SetInfoLabel);
                this.Invoke(delegater, new object[] { text });
            }
            else
            {
                infoLabel.Text = server.ServerName;
            }
        }
    }
}
