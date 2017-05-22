using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard
{
    /// <summary>
    /// interface for the state of the prio window
    /// </summary>
    public interface IWindowPrioState
    {
        /// <summary>
        /// update the info on the prio window
        /// </summary>
        /// <param name="windowPrio"></param>
        void WindowUpdate(WindowPrio windowPrio);
    }
}
