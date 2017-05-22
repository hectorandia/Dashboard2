using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard
{
    /// <summary>
    /// interface for the state of the service window
    /// </summary>
    public interface IWindowServiceState
    {
        /// <summary>
        /// updates the info on the service window
        /// </summary>
        /// <param name="windowService"></param>
        void WindowUpdate(WindowService windowService);
    }
}
