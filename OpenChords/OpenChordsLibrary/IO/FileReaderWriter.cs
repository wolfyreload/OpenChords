using System;
using System.IO;
using System.Text;

namespace OpenChords.IO
{
    public static class FileReaderWriter
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// writes a string to file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="Contents"></param>
        public static void writeToFile(String filename, String Contents)
        {
            try
            {
                logger.DebugFormat("Writing file {0}", filename);
                Contents = splitAndRebuildString(Contents);
            	
            	FileStream fStream = new FileStream(filename, FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fStream);
                writer.Write(Contents);
                writer.Flush();
                writer.Close();
                fStream.Close();
            }
            catch (Exception ex)
            {
                logger.Error("error writing to file", ex);
            }
               
        }

        private static string splitAndRebuildString(String Contents)
        {
            var splitContents = Contents.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            foreach (var piece in splitContents)
            {
                sb.AppendLine(piece);
            }
            Contents = sb.ToString();
            return Contents;
        }

        /// <summary>
        /// reads a file and generates a string of its contents
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static String readFromFile(String filename)
        {
            logger.DebugFormat("Reading file {0}", filename);
            FileStream fStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                        
            StreamReader reader = new StreamReader(fStream);
            String Contents = reader.ReadToEnd();
            reader.Close();
            fStream.Close();

            Contents = splitAndRebuildString(Contents);
            
            
            
            return Contents;
        }
        
        
 
    
    
    
    
    
    
    
    }
}
