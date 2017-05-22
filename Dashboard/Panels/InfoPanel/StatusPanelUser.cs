using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard
{
    /// <summary>
    /// class for status panel for user
    /// </summary>
    public class StatusPanelUser : StatusPanel
    {
        /// <summary>
        /// Creates an instance of a StatusPanelUser
        /// </summary>
        /// <param name="server"></param>
        /// <param name="serverPanel"></param>
        public StatusPanelUser(ServerRZ server, ServerPanel serverPanel)
            :  base(server, serverPanel, FactoryPanels.CreateLabel("User:"))
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
        /// update info (help function)
        /// </summary>
        public override void UpdateInfo()
        {
            string users = server.Users[0];
            for (int i = 1; i < server.Users.Count; i++)
            {
                users += ", " + server.Users[i];
            }
            SetInfoLabel(users);
        }
    }
}
