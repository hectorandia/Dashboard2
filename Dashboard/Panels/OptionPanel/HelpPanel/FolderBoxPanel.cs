using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Dashboard
{
    /// <summary>
    /// subclass of TextBoxPanel to handle folder requests in the option window
    /// </summary>
    public class FolderBoxPanel : TextBoxPanel
    {

        private FolderBrowserDialog dialog;

        /// <summary>
        /// creates an instance of FolderBoxPanel
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="note"></param>
        public FolderBoxPanel(string name, string value, string note) : base(name, value, note)
        {
            dialog = new FolderBrowserDialog();
            dialog.SelectedPath = RemoveDoubleSlash(value);
            textBox.DoubleClick += new EventHandler(ChooseFolder);
        }


        private void ChooseFolder(Object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox.Text = dialog.SelectedPath + "\\";
            }
        }





    }



}
