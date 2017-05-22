using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// class for the progress bar panel for ram ussage
    /// </summary>
    public class ProgressBarPanelRAM : ProgressBarPanel
    {
        /// <summary>
        /// Refreshs the progress bar
        /// </summary>
        public override void ServerUpdate()
        {
            //serverPanel.MyState.CheckConnection(serverPanel);
            myState.PanelUpdate(this);
            CheckColor();
        }

        private void CheckColor()
        {
            if (progress.Value > Options.RamWarningPercentage && progress.ForeColor != Options.RamWarningColor)
            {
                SetColor(Options.RamWarningColor);
                LogFile.Log("RAM-Auslastung auf " + Server.ServerName + " im kritischen Bereich");
            }
            else if(progress.Value <= Options.RamWarningPercentage && progress.ForeColor != Options.RamColor)
            {
                SetColor(Options.RamColor);
                LogFile.Log("RAM-Auslastung auf " + Server.ServerName + " nicht mehr im kritischen Bereich");
            }
        }

        /// <summary>
        /// updates the ussage
        /// </summary>
        public override void UpdateInfo()
        {
            SetProgressBar(server.RamUsage);
            SetvalueLabel(server.RamUsage.ToString());
        }

        /// <summary>
        /// Creates an instance of a ProgressBarPanelRAM
        /// </summary>
        /// <param name="server"></param>
        /// <param name="serverPanel"></param>
        /// <param name="name"></param>
        public ProgressBarPanelRAM(ServerRZ server, ServerPanel serverPanel, Label name)
            : base(server, serverPanel, name)
        {
            this.SetColor(Options.RamColor);
        }
    }
}
