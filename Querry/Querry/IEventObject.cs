using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventObject
    {
        /// <summary>
        /// 
        /// </summary>
      
        string ServerName { get; }
        /// <summary>
        /// 
        /// </summary>
        string Message { get; }

        /// <summary>
        /// 
        /// </summary>
        string Time { get; }

        /// <summary>
        /// 
        /// </summary>
        int Number { get; }
    }
}
