using Server;
using Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// class for a status panel for hdd
    /// </summary>
    public class StatusPanelHDD : StatusPanel
    {
        private int numberOfHDD = 1;
        private bool firstRun = true;


        /// <summary>
        /// Creates an instance of a StatusPanelHDD
        /// </summary>
        /// <param name="server"></param>
        /// <param name="serverPanel"></param>
        /// <param name="name"></param>
        public StatusPanelHDD(ServerRZ server, ServerPanel serverPanel, Label name)
            : base(server, serverPanel, name)
        {
            this.ColumnCount = 2;
            this.RowCount = 1;
            this.ColumnStyles.Clear();

            this.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15));
            this.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85));


            RemoveControl(infoLabel);
            RemoveControl(name);

        }

        private List<HDD> hdds = new List<HDD>();

        private List<HDDLabel> hddLabels = new List<HDDLabel>();

        /// <summary>
        /// updates the info shown on this panel
        /// </summary>
        public override void ServerUpdate()
        {
            //serverPanel.MyState.CheckConnection(serverPanel);
            myState.PanelUpdate(this);
        }

        private delegate void SetInfoLabelCallBack( );
        private void SetInfoLabel( )
        {
            if (this.InvokeRequired)
            {
                SetInfoLabelCallBack delegater = new SetInfoLabelCallBack(SetInfoLabel);
                this.Invoke(delegater, new object[] {  });
            }
            else
            {
                hdds = server.LogicalDisk;

                if (hdds == null)
                {
                    return;
                }

                if (numberOfHDD != hdds.Count || firstRun)
                {
                    ResizePanel();
                    firstRun = false;
                }
                else
                {
                    UpdateLabels();
                }

            }
        }

        private void UpdateLabels()
        {
            if (hdds.Count != hddLabels.Count)
            {
                
            }
            foreach (HDD hdd in hdds)
            {
                foreach (HDDLabel hddLabel in hddLabels)
                {
                    if (hdd.Device.Equals(hddLabel.DeviceText))
                    {
                        hddLabel.MessageText = hdd.Message;
                        CheckColor(hdd, hddLabel);
                        break;
                    }
                }
            }
        }

        private void UpdateNumberOfLabels()
        {
            foreach (HDDLabel hddLabel in hddLabels)
            {
                RemoveControl(hddLabel.Device);
                RemoveControl(hddLabel.Message);
            }

            hddLabels.Clear();

            foreach (HDD hdd in hdds)
            {
                hddLabels.Add(new HDDLabel(hdd.Device, hdd.Message));
            }

            for (int i = 0; i < hddLabels.Count; i++)
            {
                hddLabels[i].Device.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                hddLabels[i].Message.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                AddControl(hddLabels[i].Device, 0, i);
                AddControl(hddLabels[i].Message, 1, i);
            }
        }

        private void CheckColor(HDD hdd, HDDLabel hddLabel) 
        {
            if (hdd.Ussage > Options.HddWarningPercentage && hddLabel.Device.ForeColor != Options.HddWarningColor)
            {
                hddLabel.Device.ForeColor = Options.HddWarningColor;
                hddLabel.Message.ForeColor = Options.HddWarningColor;
                LogFile.Log("HDD " + hdd.Device + " auf " + Server.ServerName +
                    " ist im kritischen Bereich");
            }
            else if (hdd.Ussage <= Options.HddWarningPercentage && hddLabel.Device.ForeColor != Options.FontColor)
            {
                hddLabel.Device.ForeColor = Options.FontColor;
                hddLabel.Message.ForeColor = Options.FontColor;
                LogFile.Log("HDD " + hdd.Device + " auf " + Server.ServerName +
                    " ist nicht mehr im kritischen Bereich");
            }
        }

        private delegate void AddControlCallBack(Control control, int x, int y);
        private void AddControl(Control control, int x, int y)
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

        private delegate void RemoveControlCallBack(Control control);
        private void RemoveControl(Control control)
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

        private void ResizePanel()
        {
            while (numberOfHDD != hdds.Count)
            {
                if (numberOfHDD < hdds.Count)
                {
                    serverPanel.AddRow();
                    numberOfHDD++;
                }
                else
                {
                    serverPanel.RemoveRow();
                    numberOfHDD--;
                }
            }

            SetNumberRows(numberOfHDD);
            if (numberOfHDD > 0) serverPanel.SetRowSpan(this, numberOfHDD);
            UpdateNumberOfLabels();
        }

        private delegate void SetHeightCallBack(int x);
        private void SetHeight(int x)
        {
            if (this.InvokeRequired)
            {
                SetHeightCallBack delegater = new SetHeightCallBack(SetHeight);
                this.Invoke(delegater, new object[] { x });
            }
            else
            {
                this.Size = new System.Drawing.Size(this.Size.Width, x);
            }
        }

        private delegate void SetNumberRowsCallBack(int x);
        private void SetNumberRows(int x)
        {
            if (this.InvokeRequired)
            {
                SetNumberRowsCallBack delegater = new SetNumberRowsCallBack(SetNumberRows);
                this.Invoke(delegater, new object[] { x });
            }
            else
            {
                this.RowCount = x;
                this.RowStyles.Clear();
                for (int i = 0; i < x; i++)
                {
                    this.RowStyles.Add(new RowStyle(SizeType.Percent, (float)(100 / x)));
                }  
            }
        }

        /// <summary>
        /// updates the info
        /// </summary>
        public override void UpdateInfo()
        {
            SetInfoLabel();
        }
    }
}
