using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Server
{
    /// <summary>
    /// class to handle all Messages in one place
    /// </summary>
    public static class OptionMessages
    {
        // Hints
        private static string optionServerPathHint = "Pfad zur csv-Datei, in der die zu überwachenden Server aufgelistet sind";
        private static string optionServicePathHint = "Pfad zur csv-Datei, in der die zu überwachenden Service aufgelistet sind";
        private static string optionPathToOberservPathHint = "Pfad zur csv-Datei, in der die zu überwachenden Pfade angegeben sind";
        private static string optionLogFilePathHint = "Pfad zur Log-datei";
        private static string optionScreenNumberHint = "Nummer des zu verwendenden Bildschirms: 0 für den Primärbildschirm, 1 für den Sekundärbildschirm, ...";
        private static string optionDataConnectionHint = "Connection string für die Datenbankverbindung";

        // labels
        //private static string optionServerPathlabel = "";



        /// <summary>
        /// returns the hint message for the screen number
        /// </summary>
        public static string OptionScreenNumberHint
        {
            get
            {
                return optionScreenNumberHint;
            }
        }

        /// <summary>
        /// returns the hint message for the path to the log file
        /// </summary>
        public static string OptionLogFilePathHint
        {
            get
            {
                return optionLogFilePathHint;
            }
        }


        /// <summary>
        /// returns the hint message for the server path
        /// </summary>
        public static string OptionServerPathHint
        {
            get
            {
                return optionServerPathHint;
            }
        }

        /// <summary>
        /// returns the hint message for the service path
        /// </summary>
        public static string OptionServicePathHint
        {
            get
            {
                return optionServicePathHint;
            }
        }

        /// <summary>
        /// returns the hint message for path to the observed paths
        /// </summary>
        public static string OptionPathToOberservPathHint
        {
            get
            {
                return optionPathToOberservPathHint;
            }
        }

        /// <summary>
        /// returns the hint message for the dataconnection string
        /// </summary>
        public static string OptionDataConnectionHint
        {
            get
            {
                return optionDataConnectionHint;
            }

        }


        /// <summary>
        /// get the font messages for this font to be shown in the option window
        /// </summary>
        /// <param name="font"></param>
        /// <returns></returns>
        public static string CreateFontMessage(Font font)
        {
            return "Font: " + font.FontFamily.ToString()
                    + "; Schriftgröße: " + font.Size.ToString() +
                    "; Style: " + font.Style.ToString();
        }
    }
}
