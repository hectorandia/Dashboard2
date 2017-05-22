using Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// subclass of OptionPanel to handle the options for prio0
    /// </summary>
    public class OptionPrio0 : OptionPanel
    {

        private CheckBoxPanel checkPrioWithDatabase;
        private TextBoxPanel serverNameColumnPanel;
        private TextBoxPanel numberOfPrio0ColumnPanel;
        private TextBoxPanel timestampColumnPanel;
        private TextBoxPanel databasePanel;
        private TextBoxPanel eventIdPrio0Panel;
        private TextBoxPanel driver;
        private TextBoxPanel server;
        private TextBoxPanel dsn;
        private TextBoxPanel database;


        /// <summary>
        /// creates an instance of OptionPrio0
        /// </summary>
        public OptionPrio0() : base()
        {
            checkPrioWithDatabase = new CheckBoxPanel("Check Prio0 über Datenbank mit MfIMonitor", Options.UseMfIMonitor);
            checkPrioWithDatabase.CheckBox.CheckedChanged += new EventHandler(RefreshActivness);

            serverNameColumnPanel = new TextBoxPanel("Spaltenname Servername: ", Options.ServerNameColumn, "");
            numberOfPrio0ColumnPanel = new TextBoxPanel("Spaltenname Anzahl Prio0: ", Options.NumberOfPrio0Column, "");
            timestampColumnPanel = new TextBoxPanel("Spaltenname Zeitstempel", Options.TimestampColumn, "");
            databasePanel = new TextBoxPanel("Datenbanktabelle", Options.DatabaseTable, "");
            eventIdPrio0Panel = new TextBoxPanel("Event-ID Prio0: ", Options.EventIDPrio0, "");
            driver = new TextBoxPanel("Treiber: ", RemoveBrackets(Options.Driver), "Zu verwendender Treiber für den Datenbankzugriff");
            server = new TextBoxPanel("Server: ", Options.Server, "Server, auf dem die Datenbank existiert");
            dsn = new TextBoxPanel("DSN: ", Options.DSN, "Name der der ODBC-Verbindung");
            database = new TextBoxPanel("Datenbank: ", Options.Database, "Name der Datenbank");

            this.AddPanel(checkPrioWithDatabase);
            this.AddPanel(driver);
            this.AddPanel(server);
            this.AddPanel(dsn);
            this.AddPanel(database);
            this.AddPanel(databasePanel);
            this.AddPanel(serverNameColumnPanel);
            this.AddPanel(numberOfPrio0ColumnPanel);
            this.AddPanel(timestampColumnPanel);            
            this.AddPanel(eventIdPrio0Panel);

            RefreshActivness(new Object() , new EventArgs() );

            this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        }


        private void RefreshActivness(Object sender, EventArgs e)
        {
            if (checkPrioWithDatabase.Checked)
            {
                database.Visible = true;
                dsn.Visible = true;
                server.Visible = true;
                driver.Visible = true;
                serverNameColumnPanel.Visible = true;
                numberOfPrio0ColumnPanel.Visible = true;
                timestampColumnPanel.Visible = true;
                databasePanel.Visible = true;

                eventIdPrio0Panel.Visible = false;
            }
            else
            {
                database.Visible = false;
                dsn.Visible = false;
                server.Visible = false;
                driver.Visible = false;
                serverNameColumnPanel.Visible = false;
                numberOfPrio0ColumnPanel.Visible = false;
                timestampColumnPanel.Visible = false;
                databasePanel.Visible = false;

                eventIdPrio0Panel.Visible = true;
            }
        }

        /// <summary>
        /// saves all the data which belongs to this OptionPanel
        /// </summary>
        public override void SaveData()
        {
            try
            {
                Options.UseMfIMonitor = checkPrioWithDatabase.Checked;
                Options.ServerNameColumn = serverNameColumnPanel.ActualText;
                Options.NumberOfPrio0Column = numberOfPrio0ColumnPanel.ActualText;
                Options.TimestampColumn = timestampColumnPanel.ActualText;
                Options.DatabaseTable = databasePanel.ActualText;
                Options.Driver = AddBrackets(driver.ActualText);
                Options.Server = server.ActualText;
                Options.DSN = dsn.ActualText;
                Options.Database = database.ActualText;
                Options.EventIDPrio0 = eventIdPrio0Panel.ActualValue; //int.Parse(eventIdPrio0Panel.ActualText);
            }
            catch (Exception ex)
            {
                LogFile.Log("Error beim Speichern der Prio0-Optionen: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
                MessageBox.Show("Error beim Speichern der Prio0-Optionen: " + ex.Source + ": " + ex.Message + "\n" + ex.HelpLink);
            }
            
        }


        private string RemoveBrackets(string value)
        {
            string re = value.Replace("{", "");
            return re.Replace("}", "");
        }

        private string AddBrackets(string value)
        {
            return "{" + value + "}";
        }

    }
}
