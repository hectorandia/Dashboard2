using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Server
{
    /// <summary>
    /// class for an aida worker server
    /// </summary>
    public class AidaWorker : Worker
    {
        /// <summary>
        /// returns an instance of an AidaWorker
        /// </summary>
        /// <param name="serverName"></param>
        public AidaWorker(string serverName) : base(serverName)
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
                    if (services.Rows[i]["ServerTyp"].ToString() == "AidaWorker")
                    {
                        string service = services.Rows[i]["ServiceName"].ToString();
                        queryServices.AddServices(service, this.serverName);
                    }
                }
                queryServices.ClearServicesList(this.serverName);
            }
            catch (Exception ex)
            {
                LogFile.Log("Exception in AidaWorkers.LoadAllServices: " + ex.Source + " " + ex.Message);
            }
        }
        
    }

 
}
