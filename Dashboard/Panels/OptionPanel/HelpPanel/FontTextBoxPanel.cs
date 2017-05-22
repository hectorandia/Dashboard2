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
    /// class to handle the font dialog in a TextBoxPanel
    /// </summary>
    public class FontTextBoxPanel : TextBoxPanel
    {
        private FontDialog dialog;
        private Font font;

        /// <summary>
        /// creates an instance of a FontTextBoxPanel
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="note"></param>
        /// <param name="font"></param>
        public FontTextBoxPanel(string name, string value, string note, Font font) : base(name, value, note)
        {
            dialog = new FontDialog();
            this.font = font;
            dialog.Font = font;
            textBox.Click += new EventHandler(ChooseFont);
        }

        private void ChooseFont(Object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                font = dialog.Font;
                textBox.Text = OptionMessages.CreateFontMessage(font);
            }
        }

        /// <summary>
        /// get the current font selected with the font dialog
        /// </summary>
        public Font FontForThis
        {
            get
            {
                return font;
            }
        }

    }
}
