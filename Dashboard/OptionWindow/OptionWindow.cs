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
    /// returns an instance of OptionWindow
    /// </summary>
    public class OptionWindow : Form
    {
        private static int windowWidth = (int)(0.5 * FactoryPanels.ScreenWidth);
        private static int windowHeight = (int)(0.7 * FactoryPanels.ScreenHeight);

        //private static OptionPanel actualOptionPanel = null;

        private static List<OptionPanel> optionPanels = new List<OptionPanel>();

        private static OptionButtonsPanel optionButtonPanel;

        private static System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private static System.Windows.Forms.TableLayoutPanel panelOptions;

        private List<TreeNodeObject> treeNodes = new List<TreeNodeObject>();


        //private OptionPaths optionPaths; // = new OptionPaths();
        //private OptionCommon optionCommon; // = new OptionCommon();

        /// <summary>
        /// returns an instance of an option window
        /// </summary>
        public OptionWindow()
        {
            InitializeComponent();
        }


        private void InitializeComponent()
        {
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            panelOptions = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            this.SuspendLayout();

            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panelOptions);
            splitContainer1.Size = new System.Drawing.Size(windowWidth, windowHeight);
            splitContainer1.SplitterDistance = (int)(0.15 * windowWidth);
            splitContainer1.TabIndex = 0;

            splitContainer1.Panel2.AutoScroll = true;

            

            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";

            this.treeView1.Size = new System.Drawing.Size((int)(0.1*windowWidth), windowHeight);
            this.treeView1.TabIndex = 0;

            treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);

            // 
            // panel1
            // 
            panelOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            panelOptions.Location = new System.Drawing.Point(0, 0);
            panelOptions.Name = "panel1";
            panelOptions.Size = new System.Drawing.Size((int)(0.85*windowWidth), windowHeight);
            panelOptions.TabIndex = 0;

            panelOptions.AutoScroll = true;

            panelOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 90));
            panelOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 10));

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(windowWidth, windowHeight);
            this.Controls.Add(splitContainer1);
            this.Name = "Optionen";
            this.Text = "Optionen";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).EndInit();
            splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

            InitTreeStructure();

            optionButtonPanel = new OptionButtonsPanel(this);
            panelOptions.Controls.Add(optionButtonPanel);

            StartPosition = FormStartPosition.Manual;
            SetScreenOffset(100, 100);
            BringTop();
        }


        private void InitTreeStructure()
        {
            OptionPanel optionPaths = new OptionPaths();
            OptionPanel optionCommon = new OptionCommon();
            OptionPanel optionPrio = new OptionPrio0();
            OptionPanel optionRefreshrates = new OptionRefreshrates();
            OptionPanel optionColors = new OptionColor();
            OptionPanel optionFonts = new OptionFont();

            optionPanels.Add(optionPaths);
            optionPanels.Add(optionCommon);
            optionPanels.Add(optionPrio);
            optionPanels.Add(optionRefreshrates);
            optionPanels.Add(optionColors);
            optionPanels.Add(optionFonts);


            TreeNodeObject pathsNode = new TreeNodeObject("Pfade", optionPaths);
            panelOptions.Controls.Add(optionPaths);
            pathsNode.Name = "Pfade";
            pathsNode.Text = "Pfade";
            treeNodes.Add(pathsNode);

            TreeNodeObject commonNode = new TreeNodeObject("Allgemein", optionCommon);
            panelOptions.Controls.Add(optionCommon);
            commonNode.Name = "Allgemein";
            commonNode.Text = "Allgemein";
            treeNodes.Add(commonNode);

            TreeNodeObject prio0Node = new TreeNodeObject("Prio0", optionPrio);
            panelOptions.Controls.Add(optionPrio);
            prio0Node.Name = "Prio0";
            prio0Node.Text = "Prio0";
            treeNodes.Add(prio0Node);


            TreeNodeObject refreshNode = new TreeNodeObject("Aktualisierungsraten", optionRefreshrates);
            panelOptions.Controls.Add(optionRefreshrates);
            refreshNode.Name = "Aktualisierungsraten";
            refreshNode.Text = "Aktualisierungsraten";
            treeNodes.Add(refreshNode);

            TreeNodeObject colorNode = new TreeNodeObject("Farben", optionColors);
            panelOptions.Controls.Add(optionColors);
            colorNode.Name = "Farben";
            colorNode.Text = "Farben";
            treeNodes.Add(colorNode);

            TreeNodeObject fontNode = new TreeNodeObject("Schriften", optionFonts);
            panelOptions.Controls.Add(optionFonts);
            fontNode.Name = "Schriften";
            fontNode.Text = "Schriften";
            treeNodes.Add(fontNode);

            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
                commonNode,
                pathsNode,
                prio0Node,
                refreshNode,
                colorNode,
                fontNode
            });

            

        }
        private void SetScreenOffset(int offsetX, int offSetY)
        {
            Point location = Screen.AllScreens[Options.ScreenNumber].Bounds.Location;
            location.X = location.X + offsetX;
            location.Y = location.Y + offSetY;
            this.Location = location;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool active = false;
            foreach (TreeNodeObject treeNode in treeNodes)
            {
                if (treeNode.OptionControl == null)
                {
                    continue;
                }

                if (treeNode.IsSelected)
                {
                    active = true;
                    treeNode.OptionControl.Visible = true;
                }
                else
                {
                    treeNode.OptionControl.Visible = false;
                }

            }

            if (active && optionButtonPanel.Visible == false)
            {
                optionButtonPanel.Visible = true;
            }
            else if (!active)
            {
                optionButtonPanel.Visible = false;
            }
        }

        /// <summary>
        /// returns a list of all the optionPanels
        /// </summary>
        public static List<OptionPanel> OptionPanels
        {
            get
            {
                return optionPanels;
            }
        }




        /// <summary>
        /// returns the width of the window
        /// </summary>
        public static int WindowWidth
        {
            get
            {
                return windowWidth;
            }
        }

        /// <summary>
        /// returns the height of the window
        /// </summary>
        public static int WindowHeight
        {
            get
            {
                return windowHeight;
            }
        }

        /// <summary>
        /// returns the splitContainer for this window
        /// </summary>
        public static System.Windows.Forms.SplitContainer SplitContainer1
        {
            get
            {
                return splitContainer1;
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

        private delegate void CloseWindowCallBack();

        /// <summary>
        /// close the option window
        /// </summary>
        public void CloseWindow()
        {
            if (this.InvokeRequired)
            {
                CloseWindowCallBack delegater = new CloseWindowCallBack(CloseWindow);
                this.Invoke(delegater, new object[] { });
            }
            else
            {
                this.Close();
            }
        }


        /// <summary>
        /// returns the panelOptions
        /// </summary>
        public static int WidthOptionElements
        {
            get
            {
                return (int)(Math.Round(0.95 * panelOptions.Width));
            }
        }

    }
}
