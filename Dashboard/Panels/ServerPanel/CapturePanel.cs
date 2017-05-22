using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard
{
    /// <summary>
    /// class for the panel of a Capture server
    /// </summary>
    public class CapturePanel : ServerPanel, IServerObserver
    {
        /// <summary>
        /// Creates an instance of a CapturePanel
        /// </summary>
        /// <param name="server"></param>
        public CapturePanel(ServerRZ server) : base(server)
        {
            this.SetBackColor(Options.BackColorCapturePanel);
            this.backColorNormal = Options.BackColorCapturePanel;
            Program.PrioController.RegisterObserver(this);
        }

        /// <summary>
        /// update the information on this panel
        /// </summary>
        public void ServerUpdate()
        {
            if (Options.UseMfIMonitor)
            {
                ((MfIControllerPrio0)(Program.PrioController)).RefreshBackColorPanels();
            }
            else
            {
                ((Capture)Server).CheckPrio0(Options.EventIDPrio0);

                if (((Capture)Server).StatusPrio0)
                {
                    this.SetBackColor(Options.BackColorServerPanelByProblems);
                }
                else
                {
                    this.SetBackColor(Options.BackColorCapturePanel);
                }
            }           
        }
    }
}
