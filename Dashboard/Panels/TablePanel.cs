using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// abstract class for the TableLayoutPanels
    /// </summary>
    public abstract class TablePanel : TableLayoutPanel, IPanel
    {
        /// <summary>
        /// to have some information about which field is occupied
        /// so by adding a new child it can be set to a free place
        /// </summary>
        protected bool[,] occupied;

        /// <summary>
        /// list of panels which are children of this one
        /// </summary>
        protected ArrayList panels = new ArrayList();

        /// <summary>
        /// list of controls which belongs to this TablePanel
        /// </summary>
        protected ArrayList controls = new ArrayList();


        /// <summary>
        /// Creates the array occupied to have some information about which field is occupied
        /// so by adding a new child it can be set to a free place
        /// </summary>
        public void CreateOccupied()
        {
            occupied = new bool[this.ColumnCount, this.RowCount];
            for (int i = 0; i < this.ColumnCount; i++)
            {
                for (int j = 0; j < this.RowCount; j++)
                {
                    occupied[i, j] = false;
                }
            }
        }

        /// <summary>
        /// get or set the array occupied which holds the information of the occupation of the fields in this TableLayoutPanel
        /// </summary>
        public bool[,] Occupied
        {
            get
            {
                return occupied;
            }
        }

        /// <summary>
        /// returns a list of panels which belongs to this panel
        /// </summary>
        public ArrayList Panels
        {
            get
            {
                return panels;
            }
        }

        /// <summary>
        /// Adds a new panel to this panel at a free place
        /// </summary>
        /// <param name="panel"></param>
        public void AddPanel(TableLayoutPanel panel)
        {
            if (!panels.Contains(panel))
            {
                panels.Add(panel);
                // find a free place for the panel
                bool placed = false;
                for (int i = 0; i < this.ColumnCount; i++)
                {
                    for (int j = 0; j < this.RowCount; j++)
                    {
                        if (!Occupied[i, j])
                        {
                            this.Controls.Add(panel, i, j);
                            Occupied[i, j] = true;
                            placed = true;
                            break;
                        }
                    }
                    if (placed)
                    {
                        break;
                    }
                }
            }   
        }

        /// <summary>
        /// adds an control to this TablePanel
        /// </summary>
        /// <param name="control"></param>
        public void AddControl(Control control)
        {
            if (!controls.Contains(control))
            {
                controls.Add(control);
                // find a free place for the panel
                bool placed = false;
                for (int i = 0; i < this.ColumnCount; i++)
                {
                    for (int j = 0; j < this.RowCount; j++)
                    {
                        if (!Occupied[i, j])
                        {
                            this.Controls.Add(control, i, j);
                            Occupied[i, j] = true;
                            placed = true;
                            break;
                        }
                    }
                    if (placed)
                    {
                        break;
                    }
                }
            }
        }

    }
}
