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
    /// class for the state that the server is connected (state design pattern)
    /// </summary>
    public class ServerPanelConnected : IServerPanelState
    {
        /// <summary>
        /// check the connection to the server
        /// </summary>
        /// <param name="serverPanel"></param>
        public void CheckConnection(ServerPanel serverPanel)
        {
            if (serverPanel.Server.ServerDisconect)
            {
                foreach (InfoPanel panel in serverPanel.Panels)
                {
                    panel.MyState = new InfoPanelDisconnected();
                    panel.SetVisible(false);
                }
                serverPanel.SetBackColor(Options.BackColorServerPanelByProblems);
                AddInfoServerDisconnected(serverPanel);
                serverPanel.MyState = new ServerPanelDisconnected();
                LogFile.Log("Server disconnected: " + serverPanel.Server.ServerName);
            }          
        }

        private void AddInfoServerDisconnected(ServerPanel serverPanel)
        {
            if (serverPanel.InfoServerDisconnected == null)
            {
                serverPanel.Server.TimeOfDisconection = DateTime.Now;
                Label label = new Label();

                label.AutoSize = true;
                label.Location = new System.Drawing.Point(0, 0);
                label.Margin = new System.Windows.Forms.Padding(1);
                label.TabIndex = 0;
                label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                label.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                float currentSize = label.Font.Size;
                currentSize += 4.0F;
                label.Font = new Font(label.Font.Name, currentSize, label.Font.Style);


                label.Text = serverPanel.Server.ServerName + " nicht erreichbar (" + serverPanel.Server.TimeOfDisconection.ToString("HH:mm:ss") + ")";
                serverPanel.InfoServerDisconnected = label;

                serverPanel.RemoveControl(serverPanel.InfoServerConnected);
                serverPanel.AddControl(serverPanel.InfoServerDisconnected, 0, 0);
            }

        }

    }
}
