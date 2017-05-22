using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server;

namespace Dashboard
{
    class WindowFaxInactive : IWindowFaxState
    {
        /// <summary>
        /// refreshs the information in this window if the window is inactive
        /// </summary>
        /// <param name="windowFax"></param>
        public void WindowUpdate(WindowFax windowFax)
        {
            windowFax.ReInitialize();
            windowFax.RefreshWindow();
            windowFax.MyState = new WindowFaxActive();
            windowFax.MakeVisible();
            windowFax.BringTop();
            windowFax.CloseIfEmpty();
            //LogFile.Log("Problem in überwachten Ordnern detektiert");
        }

    }
}
