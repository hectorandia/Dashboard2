using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// class for the color buttons
    /// </summary>
    public class ColorButton : Button
    {

        //private Color color;
        private ColorDialog dialog;

        /// <summary>
        /// creates an instance of ColorButton
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public ColorButton(string text, Color color)
        {
            //this.color = color;
            this.BackColor = color;
            this.BackColorChanged += new EventHandler(SetBackColor);
            this.Click += new EventHandler(button_Click);
            this.Text = text;
            dialog = new ColorDialog();
            dialog.Color = color;
            SetBackColor(new Object(), new EventArgs());
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //this.color = dialog.Color;
                this.BackColor = dialog.Color;
            }
        }

        private void SetBackColor(Object sender, EventArgs e)
        {
            this.ForeColor = (PerceivedBrightness(this.BackColor) > 130 ? Color.Black : Color.White);
        }

        private int PerceivedBrightness(Color c)
        {
            return (int)Math.Sqrt(
            c.R * c.R * .241 +
            c.G * c.G * .691 +
            c.B * c.B * .068);
        }


        /// <summary>
        /// gives the actual color back
        /// </summary>
        public Color Color
        {
            get
            {
                return this.BackColor;
            }
        }


    }
}
