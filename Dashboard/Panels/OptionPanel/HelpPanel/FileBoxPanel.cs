using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Dashboard
{
    /// <summary>
    /// subclass of TextBoxPanel to handle file requests in the option window
    /// </summary>
    public class FileBoxPanel : TextBoxPanel
    {
        private OpenFileDialog dialog;

        /// <summary>
        /// creates an instance of FileBoxPanel
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="note"></param>
        public FileBoxPanel(string name, string value, string note) : base(name, value, note)
        {
            dialog = new OpenFileDialog();
            dialog.InitialDirectory = Directory(value);
            dialog.FileName = File(value);
            textBox.DoubleClick += new EventHandler(ChooseFolder);
        }


        private void ChooseFolder(Object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox.Text = dialog.FileName;
            }
        }

        private string Directory(string value)
        {
            return RemoveDoubleSlash(value.Substring(0, value.LastIndexOf("\\")));
        }

        private string File(string value)
        {
            return RemoveDoubleSlash(value.Substring(value.LastIndexOf("\\") + 1 ));
        }

    }
}
