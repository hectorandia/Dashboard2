using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// class for the progress bar panel for cpu ussage
    /// </summary>
    public class ProgressBarPanelCPU : ProgressBarPanel
    {
        /// <summary>
        /// Refreshs the progress bar
        /// </summary>
        public override void ServerUpdate()
        {
            myState.PanelUpdate(this);
            CheckColor();
        }

        private void CheckColor()
        {
            if (progress.Value > Options.CpuWarningPercentage &&
                progress.ForeColor.Equals(Options.CpuColor))
            {
                SetColor(Options.CpuWarningColor);
                LogFile.Log("CPU-Auslastung auf " + this.server.ServerName + " im kritischen Bereich");
            }
            else if (progress.ForeColor.Equals(Options.CpuWarningColor))
            {
                SetColor(Options.CpuColor);
                LogFile.Log("CPU-Auslastung auf " + this.server.ServerName + " im normalen Bereich");
            }
        }

        /// <summary>
        /// Creates an instance of a ProgressBarPanelCPU
        /// </summary>
        /// <param name="server"></param>
        /// <param name="serverPanel"></param>
        /// <param name="name"></param>
        public ProgressBarPanelCPU(ServerRZ server, ServerPanel serverPanel, Label name) 
            : base(server, serverPanel, name)
        {
            this.SetColor(Options.CpuColor);
        }

        /// <summary>
        /// updates the ussage
        /// </summary>
        public override void UpdateInfo()
        {
            SetProgressBar(server.CpuUsage);
            SetvalueLabel(server.CpuUsage.ToString());
        }

    }
}
