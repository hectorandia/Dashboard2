using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    class OptionCommon : OptionPanel
    {
        private ScreenChoicePanel screennumberPanel;


        public OptionCommon() : base()
        {
            screennumberPanel = new ScreenChoicePanel( Options.ScreenNumber);

            this.AddPanel(screennumberPanel);

            this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        }

        public override void SaveData()
        {
            try
            {
                Options.ScreenNumber = int.Parse(this.screennumberPanel.ScreenNumber.ToString());
            }
            catch (Exception ex)
            {
                LogFile.Log("Error beim Speichern der Allgemeinen-Optionen: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                MessageBox.Show("Error beim Speichern der Allgemeinen-Optionen: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
            }
        }
    }
}
