using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Server;

namespace Dashboard
{
    /// <summary>
    /// abstract class for the info panel
    /// </summary>
    public abstract class InfoPanel : TablePanel, IServerObserver
    {
        /// <summary>
        /// state of this InfoPanel
        /// </summary>
        protected IInfoPanelState myState;
        /// <summary>
        /// serverPanel to which this infoPanel belongs to
        /// </summary>
        protected ServerPanel serverPanel;

        /// <summary>
        /// Updating the information shown by this panel from the server
        /// </summary>
        public abstract void ServerUpdate();
        /// <summary>
        /// update the info
        /// </summary>
        public abstract void UpdateInfo();
        /// <summary>
        /// server which is observed by this infoPanel
        /// </summary>
        protected ServerRZ server;
        /// <summary>
        /// name of this info panel
        /// </summary>
        protected Label panelName = new Label();

        /// <summary>
        /// Get or set the label with the panelname
        /// </summary>
        public Label PanelName
        {
            get
            {
                return panelName;
            }
            set
            {
                this.panelName = value;
            }
        }      

        /// <summary>
        /// Creates an instance of an InfoPanel
        /// </summary>
        /// <param name="server"></param>
        /// <param name="serverPanel"></param>
        public InfoPanel(ServerRZ server, ServerPanel serverPanel)
        {
            this.serverPanel = serverPanel;
            this.server = server;
            this.server.RegisterObserver(this);
            CheckFirstConnection();
        }

        private void CheckFirstConnection()
        {
            if (server.ServerDisconect)
            {
                myState = new InfoPanelDisconnected();
            }
            else
            {
                myState = new InfoPanelConnected();
            }
        }

        /// <summary>
        /// returns the server which is observed by this InfoPanel
        /// </summary>
        public ServerRZ Server
        {
            get
            {
                return server;
            }
        }

        /// <summary>
        /// get or set the state of this InfoPanel
        /// </summary>
        public IInfoPanelState MyState
        {
            get
            {
                return myState;
            }
            set
            {
                this.myState = value;
            }
        }


        delegate void SetVisibleCallBack(bool visible);
        /// <summary>
        /// Sets the visibility of this InfoPanel
        /// </summary>
        /// <param name="visible"></param>
        public void SetVisible(bool visible)
        {
            if (this.InvokeRequired)
            {
                SetVisibleCallBack delegater = new SetVisibleCallBack(SetVisible);
                this.Invoke(delegater, new object[] { visible });
            }
            else
            {
                this.Visible = visible;
            }

        }
        /// <summary>
        /// returns the ServerPanel to which this InfoPanel belongs to
        /// </summary>
        public ServerPanel ServerPanel
        {
            get
            {
                return serverPanel;
            }
        }


    }
}
