using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    class ScreenChoicePanel : TablePanel
    {
        private Label label;
        private ComboBox list;
        private int screenNumber;

        public ScreenChoicePanel(int screenNumber)
        {
            this.RowCount = 1;
            this.ColumnCount = 2;

            this.CreateOccupied();

            this.screenNumber = screenNumber;

            label = FactoryPanels.CreateLabel("Bildschirm:");
            label.ForeColor = System.Drawing.Color.Black;
            label.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            label.TextAlign = System.Drawing.ContentAlignment.TopLeft;

            list = new ComboBox();
            list.SelectedValueChanged += new EventHandler(ItemChanged);

            GenerateItemList();

            this.AddControl(label);
            this.AddControl(list);

            this.Width = OptionWindow.WidthOptionElements;

        }


        private void ItemChanged(Object sender, EventArgs e)
        {
            for (int i = 0; i < Screen.AllScreens.Count<Screen>(); i++)
            {
                if (list.SelectedItem.ToString() == CutString(Screen.AllScreens[i].DeviceName))
                {
                    screenNumber = i;
                    break;
                }
            }
        }

        public int ScreenNumber
        {
            get
            {
                return screenNumber;
            }
        }

        private void GenerateItemList()
        {
            for (int i = 0; i < Screen.AllScreens.Count<Screen>(); i++)
            {
                list.Items.Add(CutString(Screen.AllScreens[i].DeviceName));
                if (i == screenNumber)
                {
                    list.SelectedItem = list.Items[i];
                }
            }
        }

        private string CutString(string value)
        {
            return value.Substring(value.LastIndexOf("\\") + 1);
        }

    }
}
