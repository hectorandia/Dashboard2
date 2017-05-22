using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    /// <summary>
    /// interface for a server observer
    /// </summary>
    public interface IServerObserver
    {
        /// <summary>
        /// Refreshs the information on this panel
        /// </summary>
        void ServerUpdate();
    }
}
