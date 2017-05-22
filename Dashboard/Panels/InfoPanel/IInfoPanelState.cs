using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard
{
    /// <summary>
    /// interface for info panels
    /// </summary>
    public interface IInfoPanelState
    {
        /// <summary>
        /// Refrehs the Info on this InfoPanel if active
        /// </summary>
        void PanelUpdate(InfoPanel infopanel);
    }
}
