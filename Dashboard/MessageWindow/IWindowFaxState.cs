using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard
{
    /// <summary>
    /// interface for the state of the fax window
    /// </summary>
    public interface IWindowFaxState
    {
        /// <summary>
        /// update the information in the fax window
        /// </summary>
        /// <param name="windowFax"></param>
        void WindowUpdate(WindowFax windowFax);
    }
}
