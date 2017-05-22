using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Dashboard
{
    class WindowServiceActive : IWindowServiceState
    {
        /// <summary>
        /// refreshs the information in this window if the window is active
        /// </summary>
        /// <param name="windowService"></param>
        public void WindowUpdate(WindowService windowService)
        {
            windowService.BringTop();
            windowService.RefreshWindow();
            windowService.CloseIfEmpty();
        }
    }
}
