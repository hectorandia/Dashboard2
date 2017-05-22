using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    /// <summary>
    /// class for building instances of Server
    /// </summary>
    public class ConcreteFactoryServer : AbstractFactoryServers
    {
        /// <summary>
        /// creates an instance of a CaptureWorker
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override Worker CaptureWorker(string name)
        {
            return new CaptureWorker(name);
        }

        /// <summary>
        /// creates an instance of a AidaWorker
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override Worker AidaWorker(string name)
        {
            return new AidaWorker(name);
        }

        /// <summary>
        /// creates an instance of a Capture
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override Capture CreateCapture(string name)
        {
            return new Capture(name);
        }

    }

}
