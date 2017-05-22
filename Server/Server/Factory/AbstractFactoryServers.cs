using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    /// <summary>
    /// abstract class to building instances of Server
    /// </summary>
    public abstract class AbstractFactoryServers
    {
        /// <summary>
        /// creates an instance of a CaptureWorker
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public abstract Worker CaptureWorker(string name);

        /// <summary>
        /// creates an instance of an AidaWorker
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public abstract Worker AidaWorker(string name);

        /// <summary>
        /// creates an instance of a Capture
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public abstract Capture CreateCapture(string name);
    }
}
