using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Query;
using System.Threading;
using Server;

namespace Dashboard
{
    /// <summary>
    /// class for the main window of this program
    /// </summary>
    public partial class MainWindow : Form
    {

        private MenuStrip menuStrip1;
        private ToolStripMenuItem dateiToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem optionToolStripMenuItem;
        private ToolStripMenuItem restartToolStripMenuItem;
        private ToolStripMenuItem hilfeToolStripMenuItem;
        private ToolStripMenuItem minimizeToolStripMenuItem;
        private ToolStripMenuItem documentationToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;


        private static MainWindow instance;

        private MainWindow()
        {
            InitializeComponent();
            this.Controls.Add(Program.MotherPanel);
            this.Size = new System.Drawing.Size(FactoryPanels.ScreenWidth, FactoryPanels.ScreenHeight);
        }

        /// <summary>
        /// returns the instance of the main window
        /// </summary>
        public static MainWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainWindow();
                }
                return instance;
            }
        }

        /// <summary>
        /// stops all threads and the main program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void exit_Click(object sender, EventArgs e)
        {
            LogFile.Log(" --------------------------------------- ");
            LogFile.Log(" -------- Dashboard geschlossen -------- ");
            LogFile.Log(" --------------------------------------- ");

            LogFile.setInactive();
            Options.RunThreads = false;
            foreach (Thread thread in Program.Threads)
            {
                thread.Abort();
            }


            Environment.Exit(0);
        }

        /// <summary>
        /// restarts the program
        /// </summary>
        public static void Restart()
        {
            LogFile.Log(" --------------------------------------- ");
            LogFile.Log(" ---- Dashboard wird neu gestartet ----- ");
            LogFile.Log(" --------------------------------------- ");

            LogFile.setInactive();
            Options.RunThreads = false;
            foreach (Thread thread in Program.Threads)
            {
                thread.Abort();
            }
            Thread.Sleep(100);

            Environment.Exit(0);
        }

        /// <summary>
        /// show the option window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void option_Click(object sender, EventArgs e)
        {
            OptionWindow optionWindow = new OptionWindow();
            optionWindow.Show(Program.MainWindow);
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private void documentation_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("Dokumentation\\Dashboard_Anleitung.htm");
            }
            catch (Exception)
            {
                MessageBox.Show("Dokumentation nicht vorhanden.");
            }
            
        }

        //public int ScreenNumber;
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// </summary>
        public void SetScreener()
        {
            try
            {
                this.Location = Screen.AllScreens[Options.ScreenNumber].Bounds.Location;
            }
            catch
            {
                if (Options.ScreenNumber >= Screen.AllScreens.Count<Screen>())
                {
                    MessageBox.Show("ScreenNumber aus der Konfigurationsdatei ist zu groß gewählt. Es wird der Standardbildschirm verwendet.");
                }
                Options.ScreenNumber = 0;
                this.Location = Screen.AllScreens[Options.ScreenNumber].Bounds.Location;
            }
            
        }

    }
}
