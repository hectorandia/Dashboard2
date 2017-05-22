using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Query;
using System.Windows.Forms;

namespace Server
{
    /// <summary>
    /// abstract class for server
    /// </summary>
    public abstract class Server : IServer
    {
        /// <summary>
        /// list of server observer
        /// </summary>
        protected List<IServerObserver> observers = new List<IServerObserver>();
        /// <summary>
        /// list of server panel observer
        /// </summary>
        protected List<IServerPanelOberserver> panelObserver = new List<IServerPanelOberserver>();
        /// <summary>
        /// instance of QueryHDD
        /// </summary>
        protected QueryHDD queryHDD;
        /// <summary>
        /// instance of QueryPing
        /// </summary>
        protected QueryPing queryPing;
        /// <summary>
        /// instance of QueryUsers
        /// </summary>
        protected QueryUsers queryUsers;
        /// <summary>
        /// instance of QueryCPU
        /// </summary>
        protected QueryPerformance queryCPU;
        /// <summary>
        /// instance of QueryService
        /// </summary>
        protected QueryService queryServices;
        /// <summary>
        /// instance of QueryRAM
        /// </summary>
        protected QueryPerformance queryRam;
        /// <summary>
        /// instance of QueryEvent
        /// </summary>
        protected QueryEvent queryEvent;
        /// <summary>
        /// status whether the server is disconnected or not
        /// </summary>
        protected bool serverDisconect = false;
        /// <summary>
        /// time when the server was recognized as disconnected
        /// </summary>
        protected DateTime timeOfDisconection;
        /// <summary>
        /// name of the server
        /// </summary>
        protected string serverName;

        /// <summary>
        /// lock variable for access to serverDisconnect (thread safe)
        /// </summary>
        protected Object thislock = new Object();

        /// <summary>
        /// creates an instance of Server
        /// </summary>
        /// <param name="serverName"></param>
        public Server(string serverName)
        {           
            queryPing = new QueryPing();
            this.serverName = serverName;
        }

        /// <summary>
        /// registers a server observer
        /// </summary>
        /// <param name="serverObserver"></param>
        public void RegisterObserver(IServerObserver serverObserver)
        {
            observers.Add(serverObserver);
        }

        /// <summary>
        /// unregisters a server observer
        /// </summary>
        /// <param name="serverObserver"></param>
        public void UnregisterObserver(IServerObserver serverObserver)
        {
            observers.Remove(serverObserver);
        }

        /// <summary>
        /// registers a server panel observer
        /// </summary>
        /// <param name="serverObserver"></param>
        public void RegisterObserver(IServerPanelOberserver serverObserver)
        {
            panelObserver.Add(serverObserver);
        }

        /// <summary>
        /// unregisters a server panel oberserver
        /// </summary>
        /// <param name="serverObserver"></param>
        public void UnregisterObserver(IServerPanelOberserver serverObserver)
        {
            panelObserver.Remove(serverObserver);
        }

        /// <summary>
        /// notifies all observers
        /// </summary>
        public void Notify()
        { 
            foreach(IServerObserver ob in observers)
            {
                ob.ServerUpdate();
            }
        }

        /// <summary>
        /// nofifies all panel observers
        /// </summary>
        public void NotifyServerPanelObserver()
        {
            foreach (IServerPanelOberserver panel in panelObserver)
            {
                panel.CheckConnection();
            }
        }

        /// <summary>
        /// checks the connection to the server
        /// </summary>
        public abstract void CheckServerConnection();

        /// <summary>
        /// returns whether the server is disconnected or not
        /// </summary>
        public bool ServerDisconect
        {
            get
            {
                lock (thislock)
                {
                    return this.serverDisconect;
                }
            }
        }

        /// <summary>
        /// returns the name of the server
        /// </summary>
        public string ServerName
        {
            get
            {
                return serverName;
            }

        }

        /// <summary>
        /// returns or sets the time when the disconnection of the server was recognized
        /// </summary>
        public DateTime TimeOfDisconection
        {
            get
            {
                return timeOfDisconection;
            }
            set
            {
                this.timeOfDisconection = value;
            }
        }

    }
}
