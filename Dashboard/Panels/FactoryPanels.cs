using Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// Creating the panels for the main window
    /// </summary>
    public class FactoryPanels
    {

        private static int screenWidth;
        private static int screenHeight;

        private static int numberOfPanelsX;
        private static int numberOfPanelsY;

        private static int panelWidth;
        private static int panelHeight;

        private static int menuHeight = 24;

        private static FactoryPanels instance = null;

        private FactoryPanels(int numberOfServer)
        {
            screenWidth = Screen.AllScreens[Options.ScreenNumber].Bounds.Width;
            screenHeight = Screen.AllScreens[Options.ScreenNumber].Bounds.Height;
            SetNumberOfPanels(numberOfServer);
            SetPanelSize();
        }

        /// <summary>
        /// returns the instance of the Factory
        /// </summary>
        /// <param name="numberOfServer"></param>
        /// <returns></returns>
        public static FactoryPanels Instance(int numberOfServer)
        {
            if (instance == null)
            {
                instance = new FactoryPanels(numberOfServer);
            }
            return instance;
        }

        /// <summary>
        /// returns the width of the screen in px
        /// </summary>
        public static int ScreenWidth
        {
            get
            {
                return screenWidth;
            }
        }

        /// <summary>
        /// returns the height of the screen in px
        /// </summary>
        public static int ScreenHeight
        {
            get
            {
                return screenHeight;
            }
        }

        /// <summary>
        /// returns the number of serverPanels in horizontal
        /// </summary>
        public static int NumberOfPanelsX
        {
            get
            {
                return numberOfPanelsX;
            }
        }

        /// <summary>
        /// returns the number of serverPanels vertical
        /// </summary>
        public static int NumberOfPanelsY
        {
            get
            {
                return numberOfPanelsY;
            }
        }

        /// <summary>
        /// returns the width of a serverPanel
        /// </summary>
        public static int PanelWidth
        {
            get
            {
                return panelWidth;
            }
        }

        /// <summary>
        /// returns the height of a serverPanel
        /// </summary>
        public static int PanelHeight
        {
            get
            {
                return panelHeight;
            }
        }

        /// <summary>
        /// returns the height of the menue bar
        /// </summary>
        public static int MenuHeight
        {
            get
            {
                return menuHeight;
            }
        }




        /// <summary>
        /// Creates an CapturePanel
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        public CapturePanel CreateCapturePanel(ServerRZ server)
        {
            CapturePanel panel = new CapturePanel(server);
            return panel;
        }

        /// <summary>
        /// Creates a WorkerPanel
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        public WorkerPanel CreateWorkerPanel(ServerRZ server)
        {
            WorkerPanel panel = new WorkerPanel(server);
            return panel;
        }

        /// <summary>
        /// Creates a MotherPanel
        /// </summary>
        /// <returns></returns>
        public MotherPanel CreateMotherPanel()
        {
            MotherPanel panel = MotherPanel.Instance;
            return panel;
        }

        /// <summary>
        /// Creates a panel for ProgressBar for cpu
        /// </summary>
        /// <param name="server"></param>
        /// <param name="serverPanel"></param>
        /// <returns></returns>
        public static ProgressBarPanel CreateProgressBarPanelCPU(ServerRZ server, ServerPanel serverPanel)
        {
            ProgressBarPanel panel = new ProgressBarPanelCPU(server, serverPanel, CreateLabel("CPU: "));                     
            return panel;
        }

        /// <summary>
        /// Creates a panel for ProgressBar for ram
        /// </summary>
        /// <param name="server"></param>
        /// <param name="serverPanel"></param>
        /// <returns></returns>
        public static ProgressBarPanel CreateProgressBarPanelRAM(ServerRZ server, ServerPanel serverPanel)
        {
            ProgressBarPanel panel = new ProgressBarPanelRAM(server, serverPanel, CreateLabel("RAM: "));
            return panel;
        }

        /// <summary>
        /// Creates a status panel for the serverName
        /// </summary>
        /// <param name="server"></param>
        /// <param name="serverPanel"></param>
        /// <returns></returns>
        public static StatusPanelServerName CreateStatusPanelServerName(ServerRZ server, ServerPanel serverPanel)
        {
            StatusPanelServerName panel = new StatusPanelServerName(server, serverPanel);
            return panel;
        }

        /// <summary>
        /// Creates a status panel for the hdds
        /// </summary>
        /// <param name="server"></param>
        /// <param name="serverPanel"></param>
        /// <returns></returns>
        public static StatusPanelHDD CreateStatusPanelHDD(ServerRZ server, ServerPanel serverPanel)
        {
            StatusPanelHDD panel = new StatusPanelHDD(server, serverPanel, CreateLabel("HDD: "));           
            return panel;
        }

        /// <summary>
        /// Creates s status panel for the ip
        /// </summary>
        /// <param name="server"></param>
        /// <param name="serverPanel"></param>
        /// <returns></returns>
        public static StatusPanelIP CreateStatusPanelIP(ServerRZ server, ServerPanel serverPanel)
        {
            return new StatusPanelIP(server, serverPanel);
        }

        /// <summary>
        /// Creates a status panel for the mac adress
        /// </summary>
        /// <param name="server"></param>
        /// <param name="serverPanel"></param>
        /// <returns></returns>
        public static StatusPanelMac CreateStatusPanelMac(ServerRZ server, ServerPanel serverPanel)
        {
            return new StatusPanelMac(server, serverPanel);
        }

        /// <summary>
        /// creates a status panel for the user logged in 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="serverPanel"></param>
        /// <returns></returns>
        public static StatusPanelUser CreateStatusPanelUser(ServerRZ server, ServerPanel serverPanel)
        {
            return new StatusPanelUser(server, serverPanel);
        }

        /// <summary>
        /// Creates a label with the text labelText
        /// </summary>
        /// <param name="labelText"></param>
        /// <returns></returns>
        public static Label CreateLabel(string labelText)
        {
            Label label = new Label();
            label.Anchor = ((System.Windows.Forms.AnchorStyles)
                ((((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(0, 0);
            label.Margin = new System.Windows.Forms.Padding(1);
            label.TabIndex = 0;
            label.Text = labelText;
            label.ForeColor = Options.FontColor;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label.Anchor = AnchorStyles.Bottom | AnchorStyles.Left 
                | AnchorStyles.Right | AnchorStyles.Top;
            label.Font = Options.FontMainWindow;
            return label;
        }

        /// <summary>
        /// creates a progress bar
        /// </summary>
        /// <returns></returns>
        public static ProgressBar CreateProgressbar()
        {
            ProgressBar progressBar = new ProgressBar();
            //progressBar.ForeColor = Options.ForeColorProgressbar; // Color.Green;
            progressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            return progressBar;
        }

        
        private void SetPanelSize()
        {
            panelWidth = screenWidth / numberOfPanelsX;
            panelHeight = (screenHeight - menuHeight) / numberOfPanelsY;
        }



        private void SetNumberOfPanels(int numberOfServer)
        {
            switch (numberOfServer)
            {
                case 1:
                    numberOfPanelsX = 1;
                    numberOfPanelsY = 1;
                    break;
                case 2:
                    numberOfPanelsX = 2;
                    numberOfPanelsY = 1;
                    break;
                case 3:
                case 4:
                    numberOfPanelsX = 2;
                    numberOfPanelsY = 2;
                    break;
                case 5:
                case 6:
                    numberOfPanelsX = 3;
                    numberOfPanelsY = 2;
                    break;
                case 7:
                case 8:
                case 9:
                    numberOfPanelsX = 3;
                    numberOfPanelsY = 3;
                    break;
                case 10:
                case 11:
                case 12:
                    numberOfPanelsX = 4;
                    numberOfPanelsY = 3;
                    break;
                case 13:
                case 14:
                case 15:
                case 16:
                    numberOfPanelsX = 4;
                    numberOfPanelsY = 4;
                    break;
                case 17:
                case 18:
                case 19:
                case 20:
                    numberOfPanelsX = 5;
                    numberOfPanelsY = 4;
                    break;
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                    numberOfPanelsX = 5;
                    numberOfPanelsY = 5;
                    break;
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                    numberOfPanelsX = 6;
                    numberOfPanelsY = 5;
                    break;
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                    numberOfPanelsX = 6;
                    numberOfPanelsY = 6;
                    break;
                case 37:
                case 38:
                case 39:
                case 40:
                case 41:
                case 42:
                    numberOfPanelsX = 7;
                    numberOfPanelsY = 6;
                    break;
                default:
                    MessageBox.Show("Zu viele Server zur Beobachtung eingetragen.\n Bitte maximal 42 Server angeben."
                        , "Zu viele Server", MessageBoxButtons.OK);
                    foreach (Thread thread in Program.Threads)
                    {
                        thread.Abort();
                    }
                    Environment.Exit(0);
                    break;
            }
            SetPanelSize();
        }



    }
}
