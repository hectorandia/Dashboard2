using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    /// <summary>
    /// interface for an server panel observer
    /// </summary>
    public interface IServerPanelOberserver
    {
        /// <summary>
        /// checks the connection to the server
        /// </summary>
        void CheckConnection();
    }
}
