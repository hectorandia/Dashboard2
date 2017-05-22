using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Server;
using System.Drawing;

namespace Dashboard
{
    /// <summary>
    /// abstract class for the server panel
    /// </summary>
    public abstract class ServerPanel : TablePanel, IServerPanelOberserver
    {
        /// <summary>
        /// state of the server panel
        /// </summary>
        protected IServerPanelState myState;
        /// <summary>
        /// server which is observed by this panel
        /// </summary>
        protected ServerRZ server;
        /// <summary>
        /// label to show if the server is disconnected
        /// </summary>
        protected Label infoServerDisconnected;
        /// <summary>
        /// label to show if the server is connected
        /// </summary>
        protected InfoPanel infoServerConnected;
        /// <summary>
        /// normal back color of the server panel
        /// </summary>
        protected Color backColorNormal;

        /// <summary>
        /// Creates an instance of an ServerPanel
        /// </summary>
        /// <param name="server"></param>
        public ServerPanel(ServerRZ server)
        {
            this.server = server;
            server.RegisterObserver(this);
            this.ColumnCount = 1;
            this.RowCount = 6;

            for (int i = 0; i < this.ColumnCount; i++)
            {
                this.ColumnStyles.Add(new System.Windows.Forms.
                    ColumnStyle(System.Windows.Forms.SizeType.Percent, (float)(100 / this.ColumnCount)));
            }


            SetRowStyle();

            this.CreateOccupied();
            //this.occupied[0, 0] = true;

            //this.Name = String.Format();
            this.Size = new System.Drawing.Size(FactoryPanels.PanelWidth, FactoryPanels.PanelHeight);
            this.TabIndex = 1;

            InfoPanel cpuPanel = FactoryPanels.CreateProgressBarPanelCPU(this.Server, this);
            InfoPanel ramPanel = FactoryPanels.CreateProgressBarPanelRAM(this.Server, this);
            InfoPanel hddPanel = FactoryPanels.CreateStatusPanelHDD(this.Server, this);
            InfoPanel serverNamePanel = FactoryPanels.CreateStatusPanelServerName(this.Server, this);
            InfoPanel ipPanel = FactoryPanels.CreateStatusPanelIP(this.Server, this);
            InfoPanel macPanel = FactoryPanels.CreateStatusPanelMac(this.Server, this);
            //InfoPanel userPanel = Factory.CreateStatusPanelUser(this.server, this);

            this.AddPanel(serverNamePanel);
            this.AddPanel(cpuPanel);
            this.AddPanel(ramPanel);
            this.AddPanel(ipPanel);
            this.AddPanel(macPanel);
            this.AddPanel(hddPanel);
            //this.AddPanel(userPanel);

            this.infoServerConnected = serverNamePanel;

            myState = new ServerPanelConnected();

            this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

        }


        /// <summary>
        /// get or set the Label if the server is disconnected
        /// </summary>
        public Label InfoServerDisconnected
        {
            get
            {
                return infoServerDisconnected;
            }
            set
            {
                infoServerDisconnected = value;
            }
        }

        /// <summary>
        /// returns the InfoLabel if the server is connected
        /// </summary>
        public InfoPanel InfoServerConnected
        {
            get
            {
                return infoServerConnected;
            }
        }

        /// <summary>
        /// returns the Server which is observed by this ServerPanel
        /// </summary>
        public ServerRZ Server
        {
            get
            {
                return server;
            }
        }

        /// <summary>
        /// returns the normal back color of this ServerPanel
        /// </summary>
        public Color BackColorNormal
        {
            get
            {
                return backColorNormal;
            }
        }

        /// <summary>
        /// get or set the state of this ServerPanel
        /// </summary>
        public IServerPanelState MyState
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

        delegate void SetBackColorCallBack(System.Drawing.Color color);
        /// <summary>
        /// sets the back color of this ServerPanel
        /// </summary>
        /// <param name="color"></param>
        public void SetBackColor(System.Drawing.Color color)
        {
            if (this.InvokeRequired)
            {
                SetBackColorCallBack delegater = new SetBackColorCallBack(SetBackColor);
                this.Invoke(delegater, new object[] { color });
            }
            else
            {
                this.BackColor = color;
                this.Refresh();
            }

        }

        delegate void AddRowCallBack();
        /// <summary>
        /// adds a row in the design of this Panel
        /// </summary>
        public void AddRow()
        {
            if (this.InvokeRequired)
            {
                AddRowCallBack delegater = new AddRowCallBack(AddRow);
                this.Invoke(delegater, new object[] { });
            }
            else
            {
                this.RowCount++;
                bool[,] occupiedOld = Occupied;
                occupied = new bool[this.ColumnCount, this.RowCount];
                for (int i = 0; i < this.ColumnCount; i++)
                {
                    for (int j = 0; j < this.RowCount - 1; j++)
                    {
                        Occupied[i, j] = occupiedOld[i, j];
                    }
                }
                SetRowStyle();
            }
        }

        private void SetRowStyle()
        {
            this.RowStyles.Clear();
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, Math.Min( (float)(15), 100/this.RowCount) ));
            for (int i = 1; i < this.RowCount; i++)
            {
                this.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent,
                    (float)(85 / (this.RowCount - 1))));
            }
        }

        delegate void RemoveRowCallBack();
        /// <summary>
        /// removes a row in the design of this panel
        /// </summary>
        public void RemoveRow()
        {
            if (this.InvokeRequired)
            {
                RemoveRowCallBack delegater = new RemoveRowCallBack(RemoveRow);
                this.Invoke(delegater, new object[] { });
            }
            else
            {
                this.RowCount--;
                bool[,] occupiedOld = Occupied;
                occupied = new bool[this.ColumnCount, this.RowCount];
                for (int i = 0; i < this.ColumnCount; i++)
                {
                    for (int j = 0; j < this.RowCount; j++)
                    {
                        Occupied[i, j] = occupiedOld[i, j];
                    }
                }
            }
        }


        delegate void RemoveControlCallBack(Control control);
        /// <summary>
        /// removes the control from this panel
        /// </summary>
        /// <param name="control"></param>
        public void RemoveControl(Control control)
        {
            if (this.InvokeRequired)
            {
                RemoveControlCallBack delegater = new RemoveControlCallBack(RemoveControl);
                this.Invoke(delegater, new object[] { control });
            }
            else
            {
                this.Controls.Remove(control);
            }

        }

        delegate void AddControlCallBack(Control control, int x, int y);
        /// <summary>
        /// adds the control from this panel to the position x and y
        /// </summary>
        /// <param name="control"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddControl(Control control, int x, int y)
        {
            if (this.InvokeRequired)
            {
                AddControlCallBack delegater = new AddControlCallBack(AddControl);
                this.Invoke(delegater, new object[] { control, x, y });
            }
            else
            {
                this.Controls.Add(control, x, y);
            }

        }

        /// <summary>
        /// checks the connection to the server
        /// </summary>
        public void CheckConnection()
        {
            myState.CheckConnection(this);
        }
    }
}
