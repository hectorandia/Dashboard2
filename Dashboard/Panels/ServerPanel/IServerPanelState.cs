using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard
{
    /// <summary>
    /// interface for the state of the server panel
    /// </summary>
    public interface IServerPanelState
    {
        /// <summary>
        /// checks the connection to the server
        /// </summary>
        /// <param name="serverPanel"></param>
        void CheckConnection(ServerPanel serverPanel);
    }
}
