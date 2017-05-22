using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard
{
    /// <summary>
    /// class for status panel for mac adress
    /// </summary>
    public class StatusPanelMac : StatusPanel
    {
        /// <summary>
        /// Creates an instance of a StatusPanelMac
        /// </summary>
        /// <param name="server"></param>
        /// <param name="serverPanel"></param>
        public StatusPanelMac(ServerRZ server, ServerPanel serverPanel)
            :  base(server, serverPanel, FactoryPanels.CreateLabel("MAC:"))
        {

        }

        /// <summary>
        /// updates info shown on this panel
        /// </summary>
        public override void ServerUpdate()
        {
            //serverPanel.MyState.CheckConnection(serverPanel);
            myState.PanelUpdate(this);
        }

        /// <summary>
        /// updates info (help function)
        /// </summary>
        public override void UpdateInfo()
        {
            SetInfoLabel(server.Mac);
        }

    }
}
