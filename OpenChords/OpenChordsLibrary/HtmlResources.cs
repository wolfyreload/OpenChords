namespace OpenChords
{
    using System;
    using System.IO;

    internal class HtmlResources
    {

        internal static string BaseSongHtml
        {
            get
            {
                return File.ReadAllText("./Resources/BaseSongHtml.html");
            }
        }

        internal static string stylesheet
        {
            get
            {
                return File.ReadAllText("./Resources/stylesheet.css");
            }
        }

        internal static string scripts
        {
            get
            {
                return File.ReadAllText("./Resources/scripts.txt");
            }
        }
    }
}
