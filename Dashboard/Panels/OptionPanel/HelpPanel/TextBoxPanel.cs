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
    /// class to handle a textbox in a panel
    /// </summary>
    public class TextBoxPanel : TablePanel
    {
        /// <summary>
        /// label for the name of this textbox
        /// </summary>
        protected Label nameLabel;

        /// <summary>
        /// TextBox for this TextBoxPanel
        /// </summary>
        protected TextBox textBox;

        /// <summary>
        /// NumericUpDown-Control for 
        /// </summary>
        protected NumericUpDown numBox;

        /// <summary>
        /// label for some optional hint for this textbox
        /// </summary>
        protected Label noteLabel;

        /// <summary>
        /// creates an instance of a TextBoxPanel
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="note"></param>
        public TextBoxPanel(string name, string value, string note)
        {
            Initialize(name, note);
            this.textBox = new TextBox();
            this.textBox.Text = value;
            this.textBox.TextChanged += new EventHandler(ResizeTextBox);
            ResizeTextBox(this, new EventArgs());
            this.Height = Math.Max(this.textBox.Height + this.noteLabel.Height, this.nameLabel.Height);
            this.Controls.Add(this.textBox, 1, 0);
            
        }

        /// <summary>
        /// creates an instance of a TextBoxPanel with a NumericUpdown-Control
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="note"></param>
        public TextBoxPanel(string name, int value, string note)
        {
            Initialize(name, note);

            this.numBox = new NumericUpDown();
            this.numBox.Value = value;

            this.Height = Math.Max(this.numBox.Height + this.noteLabel.Height, this.nameLabel.Height);
            this.Controls.Add(this.numBox, 1, 0);
        }

        private void Initialize(string name, string note)
        {
            this.ColumnCount = 2;
            this.RowCount = 2;


            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80));

            this.nameLabel = new Label(); //FactoryPanels.CreateLabel(name);
            nameLabel.Text = name;
            nameLabel.ForeColor = Color.Black;
            nameLabel.Font = Options.FontMainWindow;
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.nameLabel.ForeColor = System.Drawing.Color.Black;
            this.nameLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            this.noteLabel = FactoryPanels.CreateLabel(note);
            this.noteLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.noteLabel.ForeColor = System.Drawing.Color.Black;
            this.noteLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            
            //this.numBox.ValueChanged += new EventHandler(ResizeTextBox);
            //ResizeTextBox(this, new EventArgs());

            this.Controls.Add(this.nameLabel, 0, 0);
            
            this.Controls.Add(this.noteLabel, 1, 1);

            this.SetRowSpan(nameLabel, 2);

            
            this.Width = OptionWindow.WidthOptionElements; // (int)(0.85*OptionWindow.SplitContainer1.Panel2.Width);

            this.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        }

        private void ResizeTextBox(object sender, EventArgs e)
        {
            System.Drawing.SizeF mySize = new System.Drawing.SizeF();

            // Use the textbox font
            System.Drawing.Font myFont = textBox.Font;

            using (Graphics g = this.CreateGraphics())
            {
                // Get the size given the string and the font
                mySize = g.MeasureString(textBox.Text, myFont);
            }

            // Resize the textbox 
            this.textBox.Width = Math.Min((int)(1.05*Math.Round(mySize.Width, 0)) + 20, (int)(0.7 * OptionWindow.SplitContainer1.Panel2.Width));
        }


        /// <summary>
        /// returns the actual text of the textbox
        /// </summary>
        public string ActualText
        {
            get
            {
                if (textBox == null)
                {
                    return numBox.ToString();
                }
                return textBox.Text;
            }
            set
            {
                if (textBox == null)
                {
                    numBox.Value = int.Parse(value);
                }
                textBox.Text = value;
            }
        }

        /// <summary>
        /// returns the actual value
        /// </summary>
        public int ActualValue
        {
            get
            {
                if (numBox == null)
                {
                    return int.Parse(textBox.Text);
                }
                return (int)numBox.Value;
            }
            set
            {
                if (numBox == null)
                {
                    textBox.Text = value.ToString();
                }
                numBox.Value = value;
            }
        }

        /// <summary>
        /// replaces double slashs in a string with single slashs
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected string RemoveDoubleSlash(string path)
        {
            return path.Replace(@"\\", @"\");
        }

        /// <summary>
        /// replaces single slashs in a string with double slashs
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected string AddDoubleSlash(string path)
        {
            return path.Replace(@"\", @"\\");
        }




    }
}
