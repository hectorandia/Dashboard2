using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// class to handle checkbox inside a panel
    /// </summary>
    public class CheckBoxPanel : TablePanel
    {
        private CheckBox checkBox;

        /// <summary>
        /// creates an instance of a CheckBoxPanel
        /// </summary>
        /// <param name="text"></param>
        /// <param name="check"></param>
        public CheckBoxPanel(string text, bool check) : base()
        {
            this.ColumnCount = 1;
            this.RowCount = 1;

            this.Width = OptionWindow.WidthOptionElements;

            this.checkBox = new CheckBox();

            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(10, 10);
            //this.checkBox.Name = "checkBox1";
            //this.checkBox.Size = new System.Drawing.Size(80, 17);
            //this.checkBox.TabIndex = 0;
            this.checkBox.Text = text;
            this.checkBox.UseVisualStyleBackColor = true;

            this.Controls.Add(checkBox);

            checkBox.Checked = check;

            this.Height = (int)(1.1 * checkBox.Height);

        }

        /// <summary>
        /// returns true if the checkbox is selected and false if not
        /// </summary>
        public bool Checked
        {
            get
            {
                if (checkBox.CheckState == CheckState.Checked)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// returns the checkbox
        /// </summary>
        public CheckBox CheckBox
        {
            get
            {
                return checkBox;
            }
        }

        
    }
}
