using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Server;

namespace Dashboard
{
    /// <summary>
    /// Class for extra windows which should appear if there is some problem
    /// </summary>
    public abstract class MessageWindow : Form, IServerObserver
    {
        /// <summary>
        /// table with the information to show in this window
        /// </summary>
        protected DataGridView table; // = new DataGridView();
        /// <summary>
        /// reference to the main window ot this program
        /// </summary>
        protected MainWindow mainWindow;
        /// <summary>
        /// reference to the service controller
        /// </summary>
        protected ServiceControllerObject serviceController;
        /// <summary>
        /// reference to the prio controller
        /// </summary>
        protected IPrio0Controller prioController;

        /// <summary>
        /// updates the info shown in this window
        /// </summary>
        public abstract void ServerUpdate();

        private delegate void ClearTableCallBack();
        /// <summary>
        /// clears the table shown in this window
        /// </summary>
        protected void ClearTable()
        {
            if (this.table.InvokeRequired)
            {
                ClearTableCallBack delegater = new ClearTableCallBack(ClearTable);
                this.table.Invoke(delegater, new object[] { });
            }
            else
            {
                if (table.Rows.Count > 0)
                {
                    table.Rows.Clear();
                }

            }
        }

        /// <summary>
        /// returns the table with the information shown in this window
        /// </summary>
        public DataGridView Table
        {
            get
            {
                return table;
            }
        }        

        private delegate void ShowWindowCallBack();
        /// <summary>
        /// shows the window (if visible == true)
        /// </summary>
        public void ShowWindow()
        {
            if (this.InvokeRequired)
            {
                ShowWindowCallBack delegater = new ShowWindowCallBack(ShowWindow);
                this.Invoke(delegater, new object[] { });
            }
            else
            {
                if (mainWindow.InvokeRequired)
                {
                    ShowWindowCallBack delegater = new ShowWindowCallBack(ShowWindow);
                    mainWindow.Invoke(delegater, new object[] { });
                }
                else
                {
                    this.Show(mainWindow);
                }

            }
        }

        private delegate void MakeVisibleCallBack();
        /// <summary>
        /// sets the attribute visible to true
        /// </summary>
        public void MakeVisible()
        {
            if (this.InvokeRequired)
            {
                MakeVisibleCallBack delegater = new MakeVisibleCallBack(MakeVisible);
                this.Invoke(delegater, new object[] { });
            }
            else
            {
                this.Visible = true;
            }
        }

        private delegate void MakeUnVisibleCallBack();
        /// <summary>
        /// sets the attribute visible to false
        /// </summary>
        public void MakeUnVisible()
        {
            if (this.InvokeRequired)
            {
                MakeUnVisibleCallBack delegater = new MakeUnVisibleCallBack(MakeUnVisible);
                this.Invoke(delegater, new object[] { });
            }
            else
            {
                this.Visible = false;
            }
        }


        private delegate void BringTopCallBack();
        /// <summary>
        /// brings this window to the top
        /// </summary>
        public void BringTop()
        {
            if (this.InvokeRequired)
            {
                BringTopCallBack delegater = new BringTopCallBack(BringTop);
                this.Invoke(delegater, new object[] { });
            }
            else
            {
                this.TopMost = true;
            }
        }

        private delegate void ResizeWindowCallBack(int x, int y);
        /// <summary>
        /// Resizes the window
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ResizeWindow(int x, int y)
        {
            if (this.InvokeRequired)
            {
                ResizeWindowCallBack delegater = new ResizeWindowCallBack(ResizeWindow);
                this.Invoke(delegater, new object[] { x, y });
            }
            else
            {
                this.Size = new System.Drawing.Size(x, y);
            }
        }


        private delegate void InitializeCallBack();
        /// <summary>
        /// initializes the message window
        /// </summary>
        protected void Initialize()
        {
            if (this.InvokeRequired)
            {
                InitializeCallBack delegater = new InitializeCallBack(Initialize);
                this.Invoke(delegater, new object[] { });
            }
            else
            {
                this.Controls.Clear();
                GC.Collect();
                table = new DataGridView();
                this.table.MaximumSize = new System.Drawing.Size(mainWindow.Width * 5 / 10, mainWindow.Height * 7 / 10);
                table.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                table.Location = new System.Drawing.Point(0, 0);
                table.AutoSize = true;
                table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                this.MaximumSize = new System.Drawing.Size(mainWindow.Width * 8 / 10 + 50, mainWindow.Height);
                this.AutoSize = true;
                this.Controls.Add(table);
                this.table.AutoSize = true;
                this.table.ScrollBars = ScrollBars.Vertical;
                this.table.ReadOnly = true;
                this.table.AllowUserToAddRows = false;
                this.table.AllowUserToDeleteRows = false;
                this.table.AllowUserToResizeColumns = false;
                this.table.AllowUserToResizeRows = false;
                this.table.AutoGenerateColumns = false;
                this.table.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                this.table.RowHeadersVisible = false;
                this.table.BorderStyle = BorderStyle.None;
                this.table.Dock = DockStyle.Fill;
                this.table.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
                this.table.EnableHeadersVisualStyles = false;
                this.table.ReadOnly = true;
                this.table.RowHeadersVisible = false;
                this.table.MultiSelect = false;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.table.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            }
        }

        /// <summary>
        /// defines an offset to the position of this window related to the left upper corner of the main window
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offSetY"></param>
        protected void SetScreenOffset(int offsetX, int offSetY)
        {
            Point location = Screen.AllScreens[Options.ScreenNumber].Bounds.Location;
            location.X = location.X + offsetX;
            location.Y = location.Y + offSetY;
            this.Location = location;
        }


    }
}
