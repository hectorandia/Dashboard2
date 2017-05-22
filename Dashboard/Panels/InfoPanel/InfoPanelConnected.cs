using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Server;

namespace Dashboard
{
    class InfoPanelConnected : IInfoPanelState
    {
        public void PanelUpdate(InfoPanel infopanel)
        {
            infopanel.UpdateInfo();            
        }

       

    }
}
