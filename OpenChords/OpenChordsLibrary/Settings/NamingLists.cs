/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 2010/11/12
 * Time: 10:16 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace OpenChords.Settings
{
	/// <summary>
	/// Description of NamingLists.
	/// </summary>
	public static class NamingLists
	{
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public static string[] PrintElementNames = 
        {       "pageHeight",        
                "pageWidth",         
                "titleSize",         
                "authorSize",        
                "contentSize",       
                "footerSize",        
                "paragraphSpacing",  
                "contentLineSpacing",
                "leftPageMargin",    
                "topPageMargin",     
                "bottomPageMargin",  
                "rightPageMargin",
                "notesSize",
                "notesStartPosition",
				"lyricsStartPosition",
				"orderSize"};
		
		
	}
}
