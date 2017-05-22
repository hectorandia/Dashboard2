using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Server;
using System.Drawing;

namespace Dashboard
{
    /// <summary>
    /// Window for monitoring the fax paths
    /// </summary>
    public class WindowFax : MessageWindow
    {
        private FaxController faxController;
        private static WindowFax instance;
        private IWindowFaxState myState;

        private DataGridViewTextBoxColumn pathName;
        private DataGridViewTextBoxColumn numberOfFiles;
        private DataGridViewTextBoxColumn time;

        private WindowFax()
        {
            this.mainWindow = Program.MainWindow;
            this.faxController = Program.FaxController;
            myState = new WindowFaxInactive();
            this.ShowWindow();
            this.MakeUnVisible();
            StartPosition = FormStartPosition.Manual;
            SetScreenOffset(100, 50);
            Initialize();
        }

        /// <summary>
        /// Reinitializes the window
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
                pathName = new DataGridViewTextBoxColumn();
                numberOfFiles = new DataGridViewTextBoxColumn();
                time = new DataGridViewTextBoxColumn();
                pathName.HeaderText = "Pfad";
                numberOfFiles.HeaderText = "Anzahl Dateien/Ordner";
                time.HeaderText = "Zeitpunkt";
                table.Columns.AddRange(new DataGridViewColumn[] {
                    pathName,
                    numberOfFiles,
                    time
                });
                Font font = Options.FontFaxWindow; //new Font("Arial", 20F, GraphicsUnit.Pixel);
                pathName.DefaultCellStyle.Font = font;
                numberOfFiles.DefaultCellStyle.Font = font;
                time.DefaultCellStyle.Font = font;
                pathName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                numberOfFiles.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                time.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                this.table.ColumnHeadersDefaultCellStyle.Font = font;
                Color color = Options.BackColorFaxWindow;
                this.BackColor = color;
                this.table.ColumnHeadersDefaultCellStyle.BackColor = color;
                this.table.BackColor = color;
                this.table.BackgroundColor = color;
                pathName.DefaultCellStyle.BackColor = color;
                numberOfFiles.DefaultCellStyle.BackColor = color;
                time.DefaultCellStyle.BackColor = color;
                numberOfFiles.HeaderCell.Style.BackColor = color;
                this.table.AlternatingRowsDefaultCellStyle.BackColor =
                    Options.BackColorFaxWindow2;
                this.table.AlternatingRowsDefaultCellStyle.SelectionBackColor =
                    Options.BackColorFaxWindow2;
                this.table.GridColor = Options.GridColorFaxWindow;
                this.Opacity = Options.OpacityFaxWindow;
                this.table.RowTemplate.DefaultCellStyle.SelectionBackColor = color;
                this.table.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Black;
                this.Text = "Probleme in überwachten Ordnern";
            }
        }

        /// <summary>
        /// closes the window if empty and if this option is set true in the config file
        /// </summary>
        public void CloseIfEmpty()
        {
            if (Options.CloseFaxWindowAutomatic && table.Rows.Count == 0)
            {
                OnFormClosing(new FormClosingEventArgs(new CloseReason(), false));
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
                MessageBox.Show("Es gibt noch Probleme in überwachten Ordnern."
                    , "Beenden nicht möglich", MessageBoxButtons.OK);
            }
            else
            {
                this.MakeUnVisible();
                this.ResizeWindow(10, 10);
                myState = new WindowFaxInactive();
                //LogFile.Log("Keine Probleme mehr in überwachten Ordnern: Fenster wird geschlossen");
            }
            e.Cancel = true;
        }

        /// <summary>
        /// returns the instance
        /// </summary>
        public static WindowFax Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WindowFax();
                }
                return instance;
            }
        }

        /// <summary>
        /// Updates the information shown in this window triggered by the faxController
        /// </summary>
        public override void ServerUpdate()
        {
            myState.WindowUpdate(this);
        }

        /// <summary>
        /// returns or sets an instance of the state of this window
        /// </summary>
        public IWindowFaxState MyState
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
        /// refresh the information in this window
        /// </summary>
        public void RefreshWindow()
        {
            ClearTable();
            foreach (PathToCheck pathToCheck in faxController.PathsToCheck)
            {
                if (!pathToCheck.StatusOk)
                {
                    AddRow(pathToCheck);
                }   
            }
        }


        private delegate void AddRowCallBack(PathToCheck pathToCheck);
        /// <summary>
        /// adds a row to the table shown on this window
        /// </summary>
        /// <param name="pathToCheck"></param>
        protected void AddRow(PathToCheck pathToCheck)
        {
            if (this.table.InvokeRequired)
            {
                AddRowCallBack delegater = new AddRowCallBack(AddRow);
                this.table.Invoke(delegater, new object[] { pathToCheck });
            }
            else
            {
                table.Rows.Add(pathToCheck.Path,
                    pathToCheck.NumberOfFiles.ToString(), pathToCheck.OldestDateTime());
            }
        }



    }
}
