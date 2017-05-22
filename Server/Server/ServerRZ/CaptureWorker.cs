using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    /// <summary>
    /// class for a capture worker server
    /// </summary>
    public class CaptureWorker : Worker
    {
        /// <summary>
        /// returns an instance of CaptureWorker
        /// </summary>
        /// <param name="serverName"></param>
        public CaptureWorker(string serverName) : base(serverName)
        {

        }

        /// <summary>
        /// loads all services which should be observed
        /// </summary>
        public override void LoadAllServices()
        {
            queryServices.ClearServices();
            try
            {
                for (int i = 0; i < services.Rows.Count; i++)
                {
                    if (services.Rows[i]["ServerTyp"].ToString() == "CaptureWorker")
                    {
                        string service = services.Rows[i]["ServiceName"].ToString();
                        queryServices.AddServices(service, this.serverName);
                    }
                }

                queryServices.ClearServicesList(this.serverName);
            }
            catch (Exception ex)
            {
                LogFile.Log("Exception in CaptureWorkers.LoadAllServices: " + ex.Source + " " + ex.Message);
            }
        }
    }
}
