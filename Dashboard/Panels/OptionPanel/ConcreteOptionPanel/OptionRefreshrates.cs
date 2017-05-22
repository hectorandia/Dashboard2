using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// specific optionPanel for the refresh rates
    /// </summary>
    public class OptionRefreshrates : OptionPanel
    {

        private TextBoxPanel checkPrio0TimePanel;
        private TextBoxPanel checkStoppedServicesTimePanel;
        private TextBoxPanel checkFolderTimePanel;
        private TableTimingPanel serverTiming;

        /// <summary>
        /// creates an instance of OptionRefreshrates
        /// </summary>
        public OptionRefreshrates() : base() 
        {
            checkPrio0TimePanel = new TextBoxPanel("Aktualisierungsrate Prio0: ",
                (int)(Options.CheckPrio0Time/1000), "Zeit in s, wie oft auf Prio0-Events geprüft werden soll");
            checkStoppedServicesTimePanel = new TextBoxPanel("Aktualisierungsrate Service",
                (int)(Options.CheckStoppedServicesTime/1000), "Zeit in s, wie oft auf ausgefallene Service geprüft werden soll");
            checkFolderTimePanel = new TextBoxPanel("Aktualisierungsrate Ordnerüberwachung: ",
                (int)(Options.CheckFolderTime/1000), "Zeit in s, wie oft die zu überwachenden Ordner überprüft werden");

            serverTiming = new TableTimingPanel();

            this.AddPanel(checkPrio0TimePanel);
            this.AddPanel(checkStoppedServicesTimePanel);
            this.AddPanel(checkFolderTimePanel);

            this.AddPanel(serverTiming);


            this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        }

        /// <summary>
        /// saves all the data which belongts to this OptionPanel
        /// </summary>
        public override void SaveData() 
        {
            try
            {
                Options.CheckPrio0Time = 1000*checkPrio0TimePanel.ActualValue;
                Options.CheckFolderTime = 1000*checkFolderTimePanel.ActualValue;
                Options.CheckStoppedServicesTime = 1000*checkStoppedServicesTimePanel.ActualValue;

                Options.CheckCapturePerformanceTime = int.Parse(serverTiming.capturePerformance);
                Options.CheckCaptureWorkerPerformanceTime = int.Parse(serverTiming.captureWorkerPerformance);
                Options.CheckAidaWorkerPerformanceTime = int.Parse(serverTiming.aidaWorkerPerformance);
                Options.CheckCaptureWorkerHDDTime = int.Parse(serverTiming.captureWorkerHDD);
                Options.CheckCaptureHDDTime = int.Parse(serverTiming.captureHDD);
                Options.CheckAidaWorkerHDDTime = int.Parse(serverTiming.aidaWorkerHDD);
            }
            catch (Exception ex)
            {
                LogFile.Log("Error beim Speichern der Refreshrates-Optionen: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                MessageBox.Show("Error beim Speichern der Refreshrates-Optionen: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
            }
        }
    }
}
