﻿namespace OpenChords
{
    using System;
    using System.IO;

    internal class HtmlResources
    {

        internal static string BaseSongHtml
        {
            get
            {
                return File.ReadAllText("./Resources/BaseSongHtml.cshtml");
            }
        }
        
    }
}
