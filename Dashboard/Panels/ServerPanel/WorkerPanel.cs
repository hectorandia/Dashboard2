using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard
{
    /// <summary>
    /// class for worker panel
    /// </summary>
    public class WorkerPanel : ServerPanel, IServerObserver
    {
        /// <summary>
        /// creates an instance of an worker panel
        /// </summary>
        /// <param name="server"></param>
        public WorkerPanel(ServerRZ server) : base(server)
        {
            this.SetBackColor(Options.BackColorWorkerPanel);
            this.backColorNormal = Options.BackColorWorkerPanel;
            ServiceControllerObject.RegisterObserver(this);
        }

        /// <summary>
        /// updates info shown on this panel
        /// </summary>
        public void ServerUpdate()
        {
            ((Worker)Server).GetNamesStoppedServices();
            if (((Worker)Server).StoppedServices)
            {
                this.SetBackColor(Options.BackColorServerPanelByProblems);
            }
            else
            {
                this.SetBackColor(Options.BackColorWorkerPanel);
            }
        }
    }
}
