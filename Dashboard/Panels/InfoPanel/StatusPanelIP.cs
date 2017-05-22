using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// class for status panel for ip
    /// </summary>
    public class StatusPanelIP : StatusPanel
    {
        /// <summary>
        /// Creates an instance of a StatusPanelIP
        /// </summary>
        /// <param name="server"></param>
        /// <param name="serverPanel"></param>
        public StatusPanelIP(ServerRZ server, ServerPanel serverPanel)
            :  base(server, serverPanel, FactoryPanels.CreateLabel("IP:"))
        {

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
        /// updates info
        /// </summary>
        public override void UpdateInfo()
        {
            SetInfoLabel(server.ServerIP);
        }
    }
}
