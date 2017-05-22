using Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// class to create a table for the refresh rates of cpu/ram and hdd for the different kinds of server
    /// </summary>
    public class TableTimingPanel : TableLayoutPanel
    {
        private DataGridViewTextBoxColumn capture;
        private DataGridViewTextBoxColumn captureWorker;
        private DataGridViewTextBoxColumn aidaWorker;
        private Label label;
        private DataGridView table;

        /// <summary>
        /// creates an instance of TableTimingPanel
        /// </summary>
        public TableTimingPanel()
        {
            this.RowCount = 2;
            this.ColumnCount = 1;

            label = FactoryPanels.CreateLabel("Performance Abfragen (Zeiten in s)");
            label.ForeColor = Color.Black;
            this.Controls.Add(label);

            table = new DataGridView();

            capture = new DataGridViewTextBoxColumn();
            captureWorker = new DataGridViewTextBoxColumn();
            aidaWorker = new DataGridViewTextBoxColumn();

            capture.HeaderText = "Capture";
            captureWorker.HeaderText = "Capture Worker";
            aidaWorker.HeaderText = "Aida Worker";

            table.Columns.AddRange(new DataGridViewColumn[] {
                capture,
                captureWorker,
                aidaWorker
            });

            
            table.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            table.Location = new System.Drawing.Point(0, 0);
            table.AutoSize = true;
            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.Controls.Add(table);
            this.table.AutoSize = true;
            this.table.ScrollBars = ScrollBars.Vertical;
            this.table.AllowUserToAddRows = false;
            this.table.AllowUserToDeleteRows = false;
            this.table.AllowUserToResizeRows = false;
            this.table.AutoGenerateColumns = false;
            this.table.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.table.BorderStyle = BorderStyle.None;
            this.table.Dock = DockStyle.Fill;
            this.table.EnableHeadersVisualStyles = false;
            this.table.MultiSelect = false;
            this.table.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            table.Rows.Add((int)(Options.CheckCapturePerformanceTime/1000),
                (int)(Options.CheckCaptureWorkerPerformanceTime/1000),
                (int)(Options.CheckAidaWorkerPerformanceTime/1000));
            table.Rows.Add((int)(Options.CheckCaptureHDDTime/1000),
                (int)(Options.CheckCaptureWorkerHDDTime/1000),
                (int)(Options.CheckAidaWorkerHDDTime/1000));

            table.Rows[0].HeaderCell.Value = "CPU/RAM";
            table.Rows[1].HeaderCell.Value = "HDD";

            this.table.RowHeadersWidth = Math.Max(TextWidth(table.Rows[0].HeaderCell), TextWidth(table.Rows[1].HeaderCell));

            this.Width = table.Width; 

            this.table.BackgroundColor = this.BackColor;
            this.table.AutoSize = true;
            this.AutoSize = true;
            this.Controls.Add(table);
        }

        private int TextWidth(DataGridViewRowHeaderCell cell)
        {
            System.Drawing.SizeF mySize = new System.Drawing.SizeF();

            // Use the textbox font
            System.Drawing.Font myFont = table.Font; // textBox.Font;

            using (Graphics g = this.CreateGraphics())
            {
                // Get the size given the string and the font
                mySize = g.MeasureString(cell.Value.ToString(), myFont);
            }

            // Resize the textbox 
            return (int)(1.5 * Math.Round(mySize.Width, 0));
        }

        /// <summary>
        /// returns the refresh rate of cpu/ram for captures
        /// </summary>
        public string capturePerformance
        {
            get
            {
                return ((int)(this.table[0, 0].Value)*1000).ToString();
            }
        }

        /// <summary>
        /// returns the refresh rate of cpu/ram for captures workers
        /// </summary>
        public string captureWorkerPerformance
        {
            get
            {
                return ((int)(this.table[1, 0].Value) * 1000).ToString();
            }
        }

        /// <summary>
        /// returns the refresh rate of cpu/ram for aida workers
        /// </summary>
        public string aidaWorkerPerformance
        {
            get
            {
                return ((int)(this.table[2, 0].Value) * 1000).ToString();
            }
        }

        /// <summary>
        /// returns the refresh rate of hdd for captures
        /// </summary>
        public string captureHDD
        {
            get
            {
                return ((int)(this.table[0, 1].Value) * 1000).ToString();
            }
        }

        /// <summary>
        /// returns the refresh rate of hdd for capture workers
        /// </summary>
        public string captureWorkerHDD
        {
            get
            {
                return ((int)(this.table[1, 1].Value) * 1000).ToString();
            }
        }

        /// <summary>
        /// returns the refresh rate of hdd for aida worker
        /// </summary>
        public string aidaWorkerHDD
        {
            get
            {
                return ((int)(this.table[2, 1].Value) * 1000).ToString();
            }
        }


    }
}
