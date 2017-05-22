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
    /// class for the progress bar panel
    /// </summary>
    public abstract class ProgressBarPanel : InfoPanel
    {
        /// <summary>
        /// Creates an instance of an ProgressBarPanel
        /// </summary>
        /// <param name="server"></param>
        /// <param name="serverPanel"></param>
        /// <param name="name"></param>
        protected ProgressBarPanel(ServerRZ server, ServerPanel serverPanel, Label name) : base(server, serverPanel)
        {
            this.panelName = name;
            this.PanelName.TextAlign = ContentAlignment.MiddleLeft;
            this.ColumnCount = 3;
            this.RowCount = 1;


            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15));

            this.Size = new System.Drawing.Size(FactoryPanels.PanelWidth, FactoryPanels.PanelHeight / serverPanel.RowCount);

            

            this.Value = FactoryPanels.CreateLabel(" ");
            this.Progress = FactoryPanels.CreateProgressbar();
            this.Controls.Add(this.PanelName, 0, 0);
            this.Controls.Add(this.Progress, 1, 0);
            this.Controls.Add(this.Value, 2, 0);

            this.progress.MaximumSize = new Size(FactoryPanels.PanelWidth, FactoryPanels.PanelHeight / 13);
            this.progress.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

        }

        /// <summary>
        /// progress bar shown on this panel
        /// </summary>
        protected ProgressBar progress;
        /// <summary>
        /// Lbale with the actual value of the progress bar
        /// </summary>
        protected Label value;


        /// <summary>
        /// Returns or sets the progressbar of this panel
        /// </summary>
        public ProgressBar Progress
        {
            get {
                return progress;
            }
            set {
                this.progress = value;
            }
        }

        /// <summary>
        /// Returns or sets the value-Label of this panel
        /// </summary>
        public Label Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }


        // Functionality for setting the progressbar and Labels of the form in a thread safe way with delegaters
        private delegate void SetProgressBarCallBack(int value);
        private delegate void SetLabelCallBack(string text);

        /// <summary>
        /// set the value of the progress bar 
        /// </summary>
        /// <param name="value"></param>
        protected void SetProgressBar(int value)
        {
            if (this.progress.InvokeRequired)
            {
                SetProgressBarCallBack delegater = new SetProgressBarCallBack(SetProgressBar);
                this.Invoke(delegater, new object[] { value });
            }
            else
            {
                progress.Value = value;
            }
        }

        /// <summary>
        /// sets the label of the ussage
        /// </summary>
        /// <param name="text"></param>
        protected void SetvalueLabel(string text)
        {
            if (this.value.InvokeRequired)
            {
                SetLabelCallBack delegater = new SetLabelCallBack(SetvalueLabel);
                this.Invoke(delegater, new object[] { text });
            }
            else
            {
                value.Text = text + "%";
            }
        }


        private delegate void SetColorCallBack(Color color);

        /// <summary>
        /// sets the color of the progress bar
        /// </summary>
        /// <param name="color"></param>
        protected void SetColor(Color color)
        {
            if (this.progress.InvokeRequired)
            {
                SetColorCallBack delegater = new SetColorCallBack(SetColor);
                this.Invoke(delegater, new object[] { color });
            }
            else
            {
                if (!color.Equals(progress.ForeColor))
                {
                    progress.ForeColor = color;
                }
            }
        }




    }
}
