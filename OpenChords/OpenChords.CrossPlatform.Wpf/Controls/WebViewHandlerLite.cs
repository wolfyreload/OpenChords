using System;
using Eto.Forms;
using Eto.Wpf.Forms;
using System.Windows.Controls;

namespace OpenChords.CrossPlatform.Wpf.Controls
{
    /// <summary>
    /// original control from Eto.Forms shrunk down to exactly whats needed
    /// </summary>
    public class WebViewHandlerLite : WpfFrameworkElement<WebBrowser, WebView, WebView.ICallback>, WebView.IHandler
    {
        public WebBrowser Browser { get; private set; }

        public WebViewHandlerLite()
        {
            Browser = new WebBrowser();
            Control = Browser;
        }

        public override void Focus()
        {
            Browser.Focus();
        }

        public string ExecuteScript(string script)
        {
            var fullScript = string.Format("var fn = function() {{ {0} }}; fn();", script);
            return Convert.ToString(Browser.InvokeScript("eval", new object[] { fullScript }));
        }

        public void LoadHtml(string html, Uri baseUri)
        {
            this.Browser.NavigateToString(html);
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
                Browser.Navigate(value);
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

