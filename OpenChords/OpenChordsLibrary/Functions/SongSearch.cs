/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 2010/11/12
 * Time: 08:22 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OpenChords.Entities;
using OpenChords.IO;

using System.Text.RegularExpressions;
using System.Text;

namespace OpenChords.Functions
{
	/// <summary>
	/// Description of SongSearch.
	/// </summary>
	public static class SongSearch
	{
        //remove the any character that is not A-Za-z0-9
        private static Regex regexRemoveFunnyCharacters = new Regex(@"([^A-Z^a-z^0-9\s]+)");
        private static Regex regexRemoveExcessiveSpaces = new Regex(@"(\s{2,200})");

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// search all songs in the song list based on the text in searchCriteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
		public static string[] search(string searchCriteria)
		{
			//load directory list
			string[] songlist = 
				FileFolderFunctions.getDirectoryListing(Settings.ExtAppsAndDir.SongsFolder);
			
			//if there is no search
			if (searchCriteria == "")
				return songlist;
			
			//list of the songs found
			List<string> foundSongs = new List<string>();
			
			//list of the search criteria
            var searchCriteriaList = getSearchCriteriaList(searchCriteria);
			
			//search each song for the required criteria
			//Song tempSong;
			foreach (string songName in songlist)
			{
                var songFound = performSearch(searchCriteriaList, songName);
                if (songFound)
                    foundSongs.Add(songName);
            }
			
			return foundSongs.ToArray();
		}

        /// <summary>
        /// search for a song with provided terms and return whether all terms are met
        /// </summary>
        /// <param name="searchCriteriaList"></param>
        /// <param name="songName"></param>
        /// <returns></returns>
        private static bool performSearch(List<string> searchCriteriaList, string songName)
        {
            Song tempSong;
            int termsFound = 0;
            tempSong = Song.loadSong(songName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(tempSong.title.ToUpper());
            sb.AppendLine(tempSong.lyrics.ToUpper());
            sb.AppendLine(tempSong.author.ToUpper());
            var tempSongText = sb.ToString();
            Regex regexRemoveChordLines = new Regex(@"(\..+)");
            
            tempSongText = regexRemoveChordLines.Replace(tempSongText, "");
            tempSongText = regexRemoveFunnyCharacters.Replace(tempSongText, "");
            tempSongText = regexRemoveExcessiveSpaces.Replace(tempSongText, " ");


            foreach (string term in searchCriteriaList)
            {
                //if a single term is not found we can stop the search
                if (!tempSongText.Contains(term))
                    break;

                //we keep track of the number of terms found
                termsFound++;
            }

            //if all search terms are met then we have a positive result
            if (termsFound == searchCriteriaList.Count)
                return true;
            else
                return false;
        }

        /// <summary>
        /// here we split up the search criteria into a list of terms to search for
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        private static List<string> getSearchCriteriaList(string searchCriteria)
        {
            
            string[] searchCriteriaArray;
            List<string> searchCriteriaList = new List<string>();

            //first search quotes for phrases
            Regex expression = new Regex(@"['""][\w\d\s]+['""]");
            var matches = expression.Matches(searchCriteria);
            foreach (var match in matches)
            {
                //get the term that was matched
                var matchedTerm = match.ToString();

                //remove the term from the list
                searchCriteria = searchCriteria.Replace(matchedTerm, "");

                //remove quotes from the found item
                matchedTerm = regexRemoveFunnyCharacters.Replace(matchedTerm, "");

                //add the found term into the list of found items
                searchCriteriaList.Add(matchedTerm.ToUpper());
            }

            //lets remove weird characters and excessive spacing from the search
            searchCriteria = regexRemoveFunnyCharacters.Replace(searchCriteria, "");
            searchCriteria = regexRemoveExcessiveSpaces.Replace(searchCriteria, " ");
            
            //search all the individual terms
            char[] seperator = { ' ' };
            searchCriteriaArray = searchCriteria.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in searchCriteriaArray)
            {
                searchCriteriaList.Add(item.ToUpper());
            }
            return searchCriteriaList;
        }
	}
}
