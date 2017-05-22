using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard
{
    class WindowPrioInactive : IWindowPrioState
    {
        /// <summary>
        /// refreshs the information in this window if the window is inactive
        /// </summary>
        /// <param name="windowPrio"></param>
        public void WindowUpdate(WindowPrio windowPrio)
        {
            windowPrio.ReInitialize();
            windowPrio.CreateTable();
            windowPrio.MyState = new WindowPrioActive();
            windowPrio.MakeVisible();
            windowPrio.BringTop();
            windowPrio.CloseIfEmpty();
            windowPrio.AutoSize = true;
        }
    }
}
