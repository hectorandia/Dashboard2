using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    partial class MainWindow
    {
        private void InitializeComponent()
        {
            StartPosition = FormStartPosition.Manual;
            SetScreener();

            this.ClientSize = new System.Drawing.Size(FactoryPanels.ScreenWidth, FactoryPanels.ScreenHeight);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.WindowState = FormWindowState.Maximized;
            this.ControlBox = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = System.Drawing.Color.DarkGray;


            // menue bar
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GrayText;
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new ToolStripMenuItem();
            this.restartToolStripMenuItem = new ToolStripMenuItem();
            this.minimizeToolStripMenuItem = new ToolStripMenuItem();
            this.aboutToolStripMenuItem = new ToolStripMenuItem();
            this.documentationToolStripMenuItem = new ToolStripMenuItem();

            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.hilfeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(FactoryPanels.ScreenWidth, FactoryPanels.MenuHeight);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";

            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.minimizeToolStripMenuItem,
                this.optionToolStripMenuItem,
                this.restartToolStripMenuItem,
                this.exitToolStripMenuItem
            });

            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // hilfeToolStripMenuItem
            // 
            this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
            this.hilfeToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.hilfeToolStripMenuItem.Text = "Hilfe";
            this.hilfeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.documentationToolStripMenuItem
                //this.aboutToolStripMenuItem,
            });
            // 
            // beendenToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Beenden";
            this.exitToolStripMenuItem.Click += new System.EventHandler(MainWindow.exit_Click);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.optionToolStripMenuItem.Text = "Optionen";
            this.optionToolStripMenuItem.Click += new System.EventHandler(MainWindow.option_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new Size(152, 22);
            this.restartToolStripMenuItem.Text = "Neustart";
            this.restartToolStripMenuItem.Click += new EventHandler(Program.Restart);
            //
            // minimizing
            //
            this.minimizeToolStripMenuItem.Name = "minimizeToolStripMenuItem";
            this.minimizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.minimizeToolStripMenuItem.Text = "Minimieren";
            this.minimizeToolStripMenuItem.Click += new System.EventHandler(this.minimize_Click);

            // 
            // documentation 
            //
            this.documentationToolStripMenuItem.Name = "Dokumentation";
            this.documentationToolStripMenuItem.Text = "Dokumentation";
            this.documentationToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.documentationToolStripMenuItem.Click += new EventHandler(this.documentation_Click);

            // adding the menu bar to the window
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;

        }
    }
}
