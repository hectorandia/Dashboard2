using Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Query;

namespace Dashboard
{
    /// <summary>
    /// class for the service window
    /// </summary>
    public class WindowService : MessageWindow
    {

        private IWindowServiceState myState;
        private static WindowService instance;

        private DataGridViewTextBoxColumn serverName;
        private DataGridViewTextBoxColumn serviceName;
        private DataGridViewTextBoxColumn timeOfDisconection;

        


        private WindowService()
        {
            this.mainWindow = Program.MainWindow;
            this.serviceController = Program.ServiceController;
            this.ShowWindow();
            this.MakeUnVisible();
            StartPosition = FormStartPosition.Manual;
            SetScreenOffset(300, 50);
            myState = new WindowServiceInactive();
        }

        /// <summary>
        /// returns the instance
        /// </summary>
        public static WindowService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WindowService();
                }
                return instance;
            }
            
        }

        /// <summary>
        /// occurs before closing the window
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Es existieren noch ausgefallene Service.", "Beenden nicht möglich", MessageBoxButtons.OK);
            }
            else
            {
                this.MakeUnVisible();
                this.ResizeWindow(10, 10);
                myState = new WindowServiceInactive();
                //LogFile.Log("Keine ausgefallenen Services mehr: Fenster wird geschlossen.");
            }
            e.Cancel = true;
        }

        /// <summary>
        /// closes the window if empty and if this option is set true in the config file
        /// </summary>
        public void CloseIfEmpty()
        {
            if (Options.CloseServiceWindowAutomatic && table.Rows.Count == 0)
            {
                OnFormClosing(new FormClosingEventArgs(new CloseReason(), false));
            }
        }

        /// <summary>
        /// reinitializes the window
        /// </summary>
        public void ReInitialize()
        {
            Initialize();
        }


        private delegate void InitializeCallBack();
        private new void Initialize(  )
        {
            if (this.InvokeRequired)
            {
                InitializeCallBack delegater = new InitializeCallBack(Initialize);
                this.Invoke(delegater, new object[] { });
            }
            else
            {
                base.Initialize();
                serverName = new DataGridViewTextBoxColumn();
                serviceName = new DataGridViewTextBoxColumn();
                timeOfDisconection = new DataGridViewTextBoxColumn();
                serverName.HeaderText = "Server";
                serviceName.HeaderText = "Service";
                timeOfDisconection.HeaderText = "Ausfallzeitpunkt";

                table.Columns.AddRange(new DataGridViewColumn[] {
                    serverName,
                    serviceName,
                    timeOfDisconection
                });
                Font font = Options.FontServiceWindow; //new Font("Arial", 20F, GraphicsUnit.Pixel);
                serverName.DefaultCellStyle.Font = font;
                serviceName.DefaultCellStyle.Font = font;
                timeOfDisconection.DefaultCellStyle.Font = font;
                serverName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                serviceName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                timeOfDisconection.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                myState = new WindowServiceInactive();
                this.table.ColumnHeadersDefaultCellStyle.Font = font;
                Color color = Options.BackColorServiceWindow;
                this.BackColor = color;
                this.table.ColumnHeadersDefaultCellStyle.BackColor = color;
                this.table.BackColor = color;
                this.table.BackgroundColor = color;
                serverName.DefaultCellStyle.BackColor = color;
                serviceName.DefaultCellStyle.BackColor = color;
                timeOfDisconection.DefaultCellStyle.BackColor = color;
                serviceName.HeaderCell.Style.BackColor = color;
                this.table.AlternatingRowsDefaultCellStyle.BackColor =
                    Options.BackColorServiceWindow2;
                this.table.AlternatingRowsDefaultCellStyle.SelectionBackColor =
                    Options.BackColorServiceWindow2;
                this.table.GridColor = Options.GridColorServiceWindow;
                this.Opacity = Options.OpacityServiceWindow;
                this.table.RowTemplate.DefaultCellStyle.SelectionBackColor = color;
                this.table.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Black;
                this.Text = "Service ausgefallen";
            }


        }

        /// <summary>
        /// returns or sets the state of this window
        /// </summary>
        public IWindowServiceState MyState
        {
            get
            {
                return myState;
            }
            set
            {
                myState = value;
            }
        }

        /// <summary>
        /// Updates the information shown in this window triggered by the ServiceController
        /// </summary>
        public override void ServerUpdate()
        {
            myState.WindowUpdate(this);
        }

        /// <summary>
        /// refreshs the table with the information shown in this window
        /// </summary>
        public void RefreshWindow()
        {
            ClearTable();
            foreach (ServiceStopped service in ServiceControllerObject.StoppedServices)
            {
                AddRow(service);
            }
        }

        /// <summary>
        /// Creates the table with the information shown in this window
        /// </summary>
        public void CreateTable()
        {
            foreach (ServiceStopped service in ServiceControllerObject.StoppedServices)
            {
                AddRow(service);
            }
        }

        private delegate void AddRowCallBack(ServiceStopped service);
        /// <summary>
        /// adds a row to the table shown on this window
        /// </summary>
        /// <param name="service"></param>
        protected void AddRow(ServiceStopped service)
        {
            if (this.table.InvokeRequired)
            {
                AddRowCallBack delegater = new AddRowCallBack(AddRow);
                this.table.Invoke(delegater, new object[] { service });
            }
            else
            {
                table.Rows.Add(service.ServerName, service.ServiceName, service.TimeOfStop);
            }
        }



    }
}
