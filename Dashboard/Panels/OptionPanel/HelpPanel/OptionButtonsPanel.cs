using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// class for the button panel in the option window
    /// </summary>
    public class OptionButtonsPanel : FlowLayoutPanel
    {
        private Button applyButton;
        private Button saveButton;
        private Button cancelButton;

        private OptionWindow optionWindow;

        /// <summary>
        /// returns an instance of OptionButtonsPanel
        /// </summary>
        public OptionButtonsPanel(OptionWindow optionWindow)
        {
            this.optionWindow = optionWindow;

            applyButton = new Button();
            applyButton.Text = "Anwenden";
            applyButton.Click += new EventHandler(this.apply_Click);

            saveButton = new Button();
            saveButton.Text = "Speichern";
            saveButton.Click += new EventHandler(this.save_click);

            cancelButton = new Button();
            cancelButton.Text = "Abbrechen";
            cancelButton.Click += new EventHandler(this.cancel_Click);

            this.Controls.Add(applyButton);
            this.Controls.Add(saveButton);
            this.Controls.Add(cancelButton);

            this.AutoSize = true;

            this.Dock = DockStyle.Left;

            this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            this.Visible = false;
        }

        private void apply_Click(Object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sollen die Optionen gespeichert und neu gestartet werden?", "Neustart", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                foreach (OptionPanel panel in OptionWindow.OptionPanels)
                {
                    panel.SaveData();
                }
                Program.Restart(new Object(), new EventArgs());
            }
        }

        private void save_click(Object sender, EventArgs e)
        {
            foreach (OptionPanel panel in OptionWindow.OptionPanels)
            {
                panel.SaveData();
            }
        }

        private void cancel_Click(Object sender, EventArgs e)
        {
            optionWindow.CloseWindow();
        }

    }
}
