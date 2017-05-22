using Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Query;

namespace Dashboard
{
    /// <summary>
    /// class for the prio window (displays the observed events)
    /// </summary>
    public class WindowPrio : MessageWindow
    {

        private static WindowPrio instance;
        private IWindowPrioState myState;

        private DataGridViewTextBoxColumn serverName;
        private DataGridViewTextBoxColumn number;
        private DataGridViewTextBoxColumn time;


        private WindowPrio()
        {
            this.mainWindow = Program.MainWindow;
            prioController = Program.PrioController;
            this.ShowWindow();
            this.MakeUnVisible();
            StartPosition = FormStartPosition.Manual;
            SetScreenOffset(200, 50);
            myState = new WindowPrioInactive();
        }

        /// <summary>
        /// returns the instance
        /// </summary>
        public static WindowPrio Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WindowPrio();
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
                MessageBox.Show("Es existieren Prio0.", "Beenden nicht möglich", MessageBoxButtons.OK);
            }
            else
            {
                this.MakeUnVisible();
                //this.ResizeWindow(10, 10);
                myState = new WindowPrioInactive();
                //LogFile.Log("Prio0 Fenster wird geschlossen");
            }
            e.Cancel = true;
        }

        /// <summary>
        /// closes the window if empty and if this option is set true in the config file
        /// </summary>
        public void CloseIfEmpty()
        {
            if (Options.ClosePrioWindowAutomatic && table.Rows.Count == 0)
            {
                OnFormClosing(new FormClosingEventArgs(new CloseReason(), false));
            }            
        }

        /// <summary>
        /// reinitialzes the window
        /// </summary>
        public void ReInitialize()
        {
            Initialize();
        }

        private delegate void InitializeCallBack();
        private new void Initialize()
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
                number = new DataGridViewTextBoxColumn();
                time = new DataGridViewTextBoxColumn();


                serverName.HeaderText = "Server";
                if (Options.UseMfIMonitor)
                {
                    number.HeaderText = "Anzahl Prio0";
                }
                else
                {
                    number.HeaderText = "Message";
                }
                
                time.HeaderText = "Zeitpunkt";
                table.Columns.AddRange(new DataGridViewColumn[] {
                    serverName,
                    number,
                    time
                });
                Font font = Options.FontPrioWindow; //new Font("Arial", 20F, GraphicsUnit.Pixel);
                serverName.DefaultCellStyle.Font = font;
                number.DefaultCellStyle.Font = font;
                time.DefaultCellStyle.Font = font;
                serverName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                number.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                time.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                number.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                myState = new WindowPrioInactive();
                this.table.ColumnHeadersDefaultCellStyle.Font = font;

                Color color = Options.BackColorPrioWindow;

                this.BackColor = color;

                this.table.ColumnHeadersDefaultCellStyle.BackColor = color;
                this.table.BackColor = color;
                this.table.BackgroundColor = color;
                serverName.DefaultCellStyle.BackColor = color;
                number.DefaultCellStyle.BackColor = color;
                time.DefaultCellStyle.BackColor = color;
                number.HeaderCell.Style.BackColor = color;
                this.table.AlternatingRowsDefaultCellStyle.BackColor = Options.BackColorPrioWindow2;
                this.table.AlternatingRowsDefaultCellStyle.SelectionBackColor = Options.BackColorPrioWindow2;

                this.table.RowTemplate.DefaultCellStyle.SelectionBackColor = color;
                this.table.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Black;
                this.table.MultiSelect = false;

                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.table.GridColor = Options.GridColorPrioWindow;

                this.Opacity = Options.OpacityPrioWindow;

                this.Text = "Prio0";

            }
        }

        /// <summary>
        /// returns or sets an instance of the state of this window
        /// </summary>
        public IWindowPrioState MyState
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
        /// Updates the information shown in this window triggered by the prioController
        /// </summary>
        public override void ServerUpdate()
        {
            myState.WindowUpdate(this);
        }

        /// <summary>
        /// refreshs the information shown in this window
        /// </summary>
        public void RefreshWindow()
        {
            ClearTable();
            foreach (IEventObject eventObject in prioController.EventObjects)
            {
                AddRow(eventObject);
            }
        }

        /// <summary>
        /// Creates the table with the information shown in this window
        /// </summary>
        public void CreateTable()
        {
            foreach (IEventObject eventObject in prioController.EventObjects)
            {
                AddRow(eventObject);
            }
        }

        private delegate void AddRowCallBack(IEventObject eventObject);
        private void AddRow(IEventObject eventObject)
        {
            if (eventObject == null)
            {
                return;
            }
            if (this.table.InvokeRequired)
            {
                AddRowCallBack delegater = new AddRowCallBack(AddRow);
                this.table.Invoke(delegater, new object[] { eventObject });
            }
            else
            {
                if (Options.UseMfIMonitor)
                {
                    table.Rows.Add(eventObject.ServerName, eventObject.Number, eventObject.Time);
                }
                else
                {
                    table.Rows.Add(eventObject.ServerName, eventObject.Message, eventObject.Time);
                }
                
            }
        }



    }
}
