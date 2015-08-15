using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using swc = System.Windows.Controls;
using swn = System.Windows.Navigation;
using sw = System.Windows;
using swf = System.Windows.Forms;
using Eto.Forms;
using System.Runtime.InteropServices;
using Eto.CustomControls;
using Eto.Drawing;
using Eto.Wpf.Forms;

namespace OpenChords.CrossPlatform.Wpf.Controls
{
    /// <summary>
    /// original control from Eto.Forms shrunk down to exactly whats needed
    /// </summary>
    public class WebViewHandlerLite : WpfFrameworkElement<swf.Integration.WindowsFormsHost, WebView, WebView.ICallback>, WebView.IHandler
    {
        public swf.WebBrowser Browser { get; private set; }

        public WebViewHandlerLite()
        {
            Browser = new swf.WebBrowser
            {
                IsWebBrowserContextMenuEnabled = false,
                WebBrowserShortcutsEnabled = true,
                AllowWebBrowserDrop = false,
                ScriptErrorsSuppressed = false
            };

            Control = new swf.Integration.WindowsFormsHost
            {
                Child = Browser
            };
        }

        public string ExecuteScript(string script)
        {
            var fullScript = string.Format("var fn = function() {{ {0} }}; fn();", script);
            return Convert.ToString(Browser.Document.InvokeScript("eval", new object[] { fullScript }));
        }

        public void LoadHtml(string html, Uri baseUri)
        {
            this.Browser.DocumentText = html;
        }
        
        void WebView.IHandler.GoBack()
        {
            throw new NotImplementedException();
        }

        void WebView.IHandler.GoForward()
        {
            throw new NotImplementedException();
        }

        void WebView.IHandler.Stop()
        {
            throw new NotImplementedException();
        }

        void WebView.IHandler.Reload()
        {
            throw new NotImplementedException();
        }

        string WebView.IHandler.ExecuteScript(string script)
        {
            throw new NotImplementedException();
        }

        void WebView.IHandler.ShowPrintDialog()
        {
            throw new NotImplementedException();
        }

        public override Eto.Drawing.Color BackgroundColor
        {
            get { return Eto.Drawing.Colors.Transparent; }
            set { }
        }

        Uri WebView.IHandler.Url
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        bool WebView.IHandler.CanGoBack
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool WebView.IHandler.CanGoForward
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        string WebView.IHandler.DocumentTitle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool WebView.IHandler.BrowserContextMenuEnabled
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}

