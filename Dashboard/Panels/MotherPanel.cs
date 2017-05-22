using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// class for the mother panel
    /// </summary>
    public class MotherPanel :  TablePanel //FlowLayoutPanel, IPanel
    {
        private static MotherPanel instance = null;

        private MotherPanel()
        {
            this.ColumnCount = FactoryPanels.NumberOfPanelsX;
            this.RowCount = FactoryPanels.NumberOfPanelsY;

            this.CreateOccupied();

            for (int i = 0; i < FactoryPanels.NumberOfPanelsX; i++)
            {
                this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 / FactoryPanels.NumberOfPanelsY));
            }

            for (int i = 0; i < FactoryPanels.NumberOfPanelsY; i++)
            {
                this.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 100 / FactoryPanels.NumberOfPanelsY));
            }


            this.Location = new System.Drawing.Point(0, FactoryPanels.MenuHeight);



            this.Name = "motherpanel";

            this.Size = new System.Drawing.Size(FactoryPanels.ScreenWidth, FactoryPanels.ScreenHeight - FactoryPanels.MenuHeight);

            this.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

        }

        /// <summary>
        /// returns the instance of the MotherPanel
        /// </summary>
        public static MotherPanel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MotherPanel();
                }
                return instance;
            }
        }

    }
}
