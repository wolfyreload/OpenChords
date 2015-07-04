using NHttp;
using OpenChords.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenChords.Functions
{
    public class WebServer : IDisposable
    {
        public static Song CurrentSong { get; set; }
        public static Set CurrentSet { get; set; }
        public static DisplayAndPrintSettings CurrentDisplayAndPrintSettings { get; set; }

        private HttpServer server;
        public WebServer(int port = 8083)
        {
            var thread = new Thread(() =>
            {
                server = new HttpServer();
                server.EndPoint = new System.Net.IPEndPoint(IPAddress.Any, port);
                server.RequestReceived += server_RequestReceived;
                server.Start();
            });
            thread.Name = "OpenChords Web Server";
            thread.Start();
           
        }

        void server_RequestReceived(object sender, HttpRequestEventArgs e)
        {
            if (CurrentDisplayAndPrintSettings == null)
                CurrentDisplayAndPrintSettings = Entities.DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.DisplaySettings);
            
            var request = e.Request.Url.LocalPath;
            if (request.ToUpper().Contains("SET"))
            {
                showCurrentSet(e);
            }
            else
            {
                showCurrentSong(e);
            }
        
        }



        private void showCurrentSong(HttpRequestEventArgs e)
        {
            DisplayAndPrintSettings displayAndPrintSettings = getDisplaySettingsOption(e);
            using (var writer = new StreamWriter(e.Response.OutputStream))
            {
                if (CurrentSong == null)
                {
                    writer.Write("No song selected");
                    return;
                }
                var htmlSong = CurrentSong.getHtml(displayAndPrintSettings);
                writer.Write(htmlSong);
            }
        }

        private DisplayAndPrintSettings getDisplaySettingsOption(HttpRequestEventArgs e)
        {
            DisplayAndPrintSettings settings = null;
            var request = e.Request.Url.ToString();
            if (request.ToLower().Contains("/print"))
                settings = DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.PrintSettings);
            else if (request.ToLower().Contains("/tablet"))
                settings = DisplayAndPrintSettings.loadSettings(DisplayAndPrintSettingsType.TabletSettings);
            else
                settings = CurrentDisplayAndPrintSettings;
            return settings;
        }

        private void showCurrentSet(HttpRequestEventArgs e)
        {
            DisplayAndPrintSettings displayAndPrintSettings = getDisplaySettingsOption(e);
            using (var writer = new StreamWriter(e.Response.OutputStream))
            {
                if (CurrentSet == null)
                {
                    writer.Write("No set selected");
                    return;
                }
                var htmlSet = CurrentSet.getHtml(displayAndPrintSettings);
                writer.Write(htmlSet);
            }
        }


        public void Dispose()
        {
            server.Dispose();
        }
    }
}
