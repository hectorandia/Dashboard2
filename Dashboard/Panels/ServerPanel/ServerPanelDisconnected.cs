using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard
{
    /// <summary>
    /// class for the state that the server is disconnected
    /// </summary>
    public class ServerPanelDisconnected : IServerPanelState
    {
        /// <summary>
        /// check the connection to the server
        /// </summary>
        /// <param name="serverPanel"></param>
        public void CheckConnection(ServerPanel serverPanel)
        {
            if (!serverPanel.Server.ServerDisconect)
            {
                foreach (InfoPanel panel in serverPanel.Panels)
                {
                    panel.MyState = new InfoPanelConnected();
                    panel.SetVisible(true);
                }
                serverPanel.SetBackColor(serverPanel.BackColorNormal);
                serverPanel.RemoveControl(serverPanel.InfoServerDisconnected);
                serverPanel.InfoServerDisconnected = null;
                serverPanel.AddControl(serverPanel.InfoServerConnected, 0, 0);
                serverPanel.MyState = new ServerPanelConnected();
                serverPanel.Server.GetMac();
                serverPanel.Server.GetServerIP();
                LogFile.Log("Server reconnected: " + serverPanel.Server.ServerName);
            }
        }
    }
}
