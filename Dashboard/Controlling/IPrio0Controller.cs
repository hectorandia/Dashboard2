using Query;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Dashboard
{
    /// <summary>
    /// Interface for Prio0Controller
    /// </summary>
    public interface IPrio0Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Thread StartThread();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverObserver"></param>
        void RegisterObserver(IServerObserver serverObserver);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverObserver"></param>
        void UnregisterObserver(IServerObserver serverObserver);

        /// <summary>
        /// 
        /// </summary>
        List<IEventObject> EventObjects { get; }
    }
}
