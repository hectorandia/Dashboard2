using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Threading;

namespace Dashboard
{
    class WindowServiceInactive : IWindowServiceState
    {
        /// <summary>
        /// refreshs the information in this window if the window is inactive
        /// </summary>
        /// <param name="windowService"></param>
        public void WindowUpdate(WindowService windowService)
        {
            windowService.ReInitialize();
            windowService.CreateTable();
            windowService.MyState = new WindowServiceActive();
            windowService.MakeVisible();
            windowService.BringTop();
            windowService.CloseIfEmpty();
            //LogFile.Log("Service ausgefallen: Service-Fenster wird geöffnet");
        }

        

    }
}
