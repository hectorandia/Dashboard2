using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// class for options for paths
    /// </summary>
    public class OptionPaths : OptionPanel
    {
        private FileBoxPanel serverPathPanel;
        private FileBoxPanel servicesPathPanel;
        private FileBoxPanel pathCheckingPathPanel;
        private FolderBoxPanel logFilePathPanel;
        

        /// <summary>
        /// returns an instance of OptionPaths
        /// </summary>
        public OptionPaths() : base()
        {

            this.serverPathPanel = new FileBoxPanel("Server: ", RemoveDoubleSlash(Options.PathServers), OptionMessages.OptionServerPathHint);
            this.servicesPathPanel = new FileBoxPanel("Services: ", RemoveDoubleSlash(Options.PathServices), OptionMessages.OptionServicePathHint);
            this.pathCheckingPathPanel = new FileBoxPanel("Pfade zur Überwachung: ", RemoveDoubleSlash(Options.PathForPathToCheck), OptionMessages.OptionPathToOberservPathHint);
            this.logFilePathPanel = new FolderBoxPanel("Logfile: ", RemoveDoubleSlash(Options.LogFilePath), OptionMessages.OptionLogFilePathHint);

            this.AddPanel(serverPathPanel);
            this.AddPanel(servicesPathPanel);
            this.AddPanel(pathCheckingPathPanel);
            this.AddPanel(logFilePathPanel);
            //this.Controls.Add(new OptionButtonsPanel(this));
            //this.Size = OptionWindow.SplitContainer1.Panel2.ClientSize;

            this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        }



        /// <summary>
        /// save the current data to the config file
        /// </summary>
        public override void SaveData()
        {
            try
            {
                Options.PathServers = AddDoubleSlash(serverPathPanel.ActualText);
                Options.PathServices = AddDoubleSlash(servicesPathPanel.ActualText);
                Options.PathForPathToCheck = AddDoubleSlash(pathCheckingPathPanel.ActualText);
                Options.LogFilePath = AddDoubleSlash(logFilePathPanel.ActualText);
            }
            catch (Exception ex)
            {
                LogFile.Log("Error beim Speichern der Pfad-Optionen: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                MessageBox.Show("Error beim Speichern der Pfad-Optionen: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
            }   
        }


        private string RemoveDoubleSlash(string path)
        {
            return path.Replace(@"\\",@"\");
        }

        private string AddDoubleSlash(string path)
        {
            return path.Replace(@"\", @"\\");
        }

    }
}
