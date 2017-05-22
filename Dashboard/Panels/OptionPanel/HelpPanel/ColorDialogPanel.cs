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
    /// class to handle all collor buttons for the option window
    /// </summary>
    public class ColorDialogPanel : TablePanel
    {

        private ColorButton fontColorButton;
        private ColorButton backColorCapturePanel;
        private ColorButton backColorWorkerPanel;
        private ColorButton backColorServerDisconnect;
        private ColorButton backColorServerPanelByProblems;
        private ColorButton ramColor;
        private ColorButton ramWarningColor;
        private ColorButton cpuColor;
        private ColorButton cpuWarningColor;
        private ColorButton hddWarningColor;
        private ColorButton backColorServiceWindow;
        private ColorButton backColorServiceWindow2;
        private ColorButton gridColorServiceWindow;
        private ColorButton backColorPrioWindow;
        private ColorButton backColorPrioWindow2;
        private ColorButton gridColorPrioWindow;
        private ColorButton backColorFaxWindow;
        private ColorButton backColorFaxWindow2;
        private ColorButton gridColorFaxWindow;

        /// <summary>
        /// creates an instance of ColorDialogPanel
        /// </summary>
        public ColorDialogPanel() : base()
        { 
            this.ColumnCount = 3;
            this.RowCount = 10;

            this.CreateOccupied();

            fontColorButton = new ColorButton("Schrift", Options.FontColor);
            backColorCapturePanel = new ColorButton("Hintergrundfarbe Capture", Options.BackColorCapturePanel);
            backColorWorkerPanel = new ColorButton("Hintergrundfarbe Worker", Options.BackColorWorkerPanel);
            backColorServerDisconnect = new ColorButton("Hintergrundfarbe bei Disconnect", Options.BackColorServerDisconnect);
            backColorServerPanelByProblems = new ColorButton("Hintergrundfarbe bei Problemen", Options.BackColorServerPanelByProblems);
            ramColor = new ColorButton("Balkenfarbe RAM", Options.RamColor);
            ramWarningColor = new ColorButton("Warnfarbe RAM", Options.RamWarningColor);
            cpuColor = new ColorButton("Balkenfarbe CPU", Options.CpuColor);
            cpuWarningColor = new ColorButton("Warnfarbe CPU", Options.CpuWarningColor);
            hddWarningColor = new ColorButton("Warnfarbe HDD", Options.HddWarningColor);
            backColorServiceWindow = new ColorButton("Hintergrundfarbe 1 Service-Fenster", Options.BackColorServiceWindow);
            backColorServiceWindow2 = new ColorButton("Hintergrundfarbe 2 Service-Fenster", Options.BackColorServiceWindow2);
            gridColorServiceWindow = new ColorButton("Gitterfarbe Service-Fenster", Options.GridColorServiceWindow);
            backColorPrioWindow = new ColorButton("Hintergrundfarbe 1 Prio0-Fenster", Options.BackColorPrioWindow);
            backColorPrioWindow2 = new ColorButton("Hintergrundfarbe 2 Prio0-Fenster", Options.BackColorPrioWindow2);
            gridColorPrioWindow = new ColorButton("Gitterfarbe Prio0-Fenster", Options.GridColorPrioWindow);
            backColorFaxWindow = new ColorButton("Hintergrundfarbe 1 Ordnerüberwachungsfenster", Options.BackColorFaxWindow);
            backColorFaxWindow2 = new ColorButton("Hintergrundfarbe 2 Ordnerüberwachungsfenster", Options.BackColorFaxWindow2);
            gridColorFaxWindow = new ColorButton("Gitterfarbe Ordnerüberwachungsfenster", Options.GridColorFaxWindow);


            this.AddControl(fontColorButton);
            this.AddControl(backColorCapturePanel);
            this.AddControl(backColorWorkerPanel);
            this.AddControl(backColorServerDisconnect);
            this.AddControl(backColorServerPanelByProblems);
            this.AddControl(ramColor);
            this.AddControl(cpuColor);
            this.AddControl(ramWarningColor);            
            this.AddControl(cpuWarningColor);
            this.AddControl(hddWarningColor);
            this.AddControl(backColorServiceWindow);
            this.AddControl(backColorServiceWindow2);
            this.AddControl(gridColorServiceWindow);
            this.AddControl(backColorPrioWindow);
            this.AddControl(backColorPrioWindow2);
            this.AddControl(gridColorPrioWindow);
            this.AddControl(backColorFaxWindow);
            this.AddControl(backColorFaxWindow2);
            this.AddControl(gridColorFaxWindow);


            this.Width = (int)(0.9*OptionWindow.WidthOptionElements);
            this.Height = (int)(0.85*OptionWindow.WindowHeight);
            ResizeButtons();
        }

        /// <summary>
        /// saves all the data which from the color buttons
        /// </summary>
        public void SaveData()
        {
            try
            {
                Options.FontColor = fontColorButton.Color;
                Options.BackColorCapturePanel = backColorCapturePanel.Color;
                Options.BackColorWorkerPanel = backColorWorkerPanel.Color;
                Options.BackColorServerDisconnect = backColorServerDisconnect.Color;
                Options.BackColorServerPanelByProblems = backColorServerPanelByProblems.Color;
                Options.RamColor = ramColor.Color;
                Options.RamWarningColor = ramWarningColor.Color;
                Options.CpuColor = cpuColor.Color;
                Options.CpuWarningColor = cpuWarningColor.Color;
                Options.HddWarningColor = hddWarningColor.Color;
                Options.BackColorServiceWindow = backColorServiceWindow.Color;
                Options.BackColorServiceWindow2 = backColorServiceWindow2.Color;
                Options.GridColorServiceWindow = gridColorServiceWindow.Color;
                Options.BackColorPrioWindow = backColorPrioWindow.Color;
                Options.BackColorPrioWindow2 = backColorPrioWindow2.Color;
                Options.GridColorPrioWindow = gridColorPrioWindow.Color;
                Options.BackColorFaxWindow = backColorFaxWindow.Color;
                Options.BackColorFaxWindow2 = backColorFaxWindow2.Color;
                Options.GridColorFaxWindow = gridColorFaxWindow.Color;
            }
            catch (Exception ex)
            {
                LogFile.Log("Error beim Speichern der Color-Optionen: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                MessageBox.Show("Error beim Speichern der Color-Optionen: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
            }
            
        }




        private void ResizeButtons()
        {
            System.Drawing.SizeF mySize = new System.Drawing.SizeF();

            // Use the textbox font
            System.Drawing.Font myFont = fontColorButton.Font;

            int max = 0;
            foreach (ColorButton button in this.Controls)
            {
                using (Graphics g = this.CreateGraphics())
                {
                    // Get the size given the string and the font
                    mySize = g.MeasureString(button.Text, myFont);
                    if (mySize.Width > max)
                    {
                        max = (int)Math.Round(mySize.Width, 0);
                    }
                }
   
            }

            foreach (ColorButton button in this.Controls)
            {
                button.Width = (int)(1.1 * max);
            }

            
        }

    }
}
