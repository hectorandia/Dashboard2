using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    /// <summary>
    /// Interface for Server
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// registers an observer (observer design pattern)
        /// </summary>
        /// <param name="serverObserver"></param>
        void RegisterObserver(IServerObserver serverObserver);

        /// <summary>
        /// unregisters an observer (observer design pattern)
        /// </summary>
        /// <param name="serverObserver"></param>
        void UnregisterObserver(IServerObserver serverObserver);

        /// <summary>
        /// notifies all observers (observer design pattern)
        /// </summary>
        void Notify();
    }
}
