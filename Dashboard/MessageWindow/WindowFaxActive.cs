using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard
{
    class WindowFaxActive : IWindowFaxState
    {
        /// <summary>
        /// refreshs the information in this window if the window is active
        /// </summary>
        /// <param name="windowFax"></param>
        public void WindowUpdate(WindowFax windowFax)
        {
            windowFax.BringTop();
            windowFax.RefreshWindow();
            windowFax.CloseIfEmpty();
        }
    }
}
