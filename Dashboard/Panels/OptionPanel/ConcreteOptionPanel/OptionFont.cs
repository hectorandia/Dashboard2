using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// OptionPanel for the fonts
    /// </summary>
    public class OptionFont : OptionPanel
    {

        private FontTextBoxPanel fontMainWindow;
        private FontTextBoxPanel fontServiceWindow;
        private FontTextBoxPanel fontPrioWindow;
        private FontTextBoxPanel fontFaxWindow;

        /// <summary>
        /// creates an instance of an OptionFont
        /// </summary>
        public OptionFont()
        {
            fontMainWindow = new FontTextBoxPanel("Schrift Hauptfenster: ", OptionMessages.CreateFontMessage( Options.FontMainWindow), "", Options.FontMainWindow);
            fontServiceWindow = new FontTextBoxPanel("Schrift Service-Fenster: ", OptionMessages.CreateFontMessage(Options.FontServiceWindow), "", Options.FontServiceWindow);
            fontPrioWindow = new FontTextBoxPanel("Schrift Prio0-Fenster: ", OptionMessages.CreateFontMessage(Options.FontPrioWindow), "", Options.FontPrioWindow);
            fontFaxWindow = new FontTextBoxPanel("Schrift Ordnerüberwachungsfenster: ", OptionMessages.CreateFontMessage(Options.FontFaxWindow), "", Options.FontFaxWindow);


            this.Width = OptionWindow.WidthOptionElements;

            this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            this.AddPanel(fontMainWindow);
            this.AddPanel(fontServiceWindow);
            this.AddPanel(fontPrioWindow);
            this.AddPanel(fontFaxWindow);
        }

        /// <summary>
        /// saves the data on this OptionPanel
        /// </summary>
        public override void SaveData()
        {
            try
            {
                Options.FontMainWindow = fontMainWindow.FontForThis;
                Options.FontServiceWindow = fontServiceWindow.FontForThis;
                Options.FontPrioWindow = fontPrioWindow.FontForThis;
                Options.FontFaxWindow = fontFaxWindow.FontForThis;
            }
            catch (Exception ex)
            {
                LogFile.Log("Error beim Speichern der Font-Optionen: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                MessageBox.Show("Error beim Speichern der Font-Optionen: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
            }
        }
    }
}
