using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard
{
    class WindowPrioActive : IWindowPrioState
    {
        /// <summary>
        /// refreshs the information in this window if the window is active
        /// </summary>
        /// <param name="windowPrio"></param>
        public void WindowUpdate(WindowPrio windowPrio)
        {
            windowPrio.BringTop();
            windowPrio.RefreshWindow();
            windowPrio.CloseIfEmpty();
        }
    }
}
