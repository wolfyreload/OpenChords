using System;
using Eto.Forms;
using Eto.Wpf.Forms;
using System.Windows.Controls;
using System.Windows.Input;

namespace OpenChords.CrossPlatform.Executable.Controls
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
            Browser.PreviewKeyDown += Browser_ShortcutKeyPressed2;
            Browser.LoadCompleted += Browser_LoadCompleted;
            Control = Browser;

        }

        /// <summary>
        /// disable context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Browser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            var document = Browser.Document as mshtml.HTMLDocumentEvents2_Event;
            document.oncontextmenu += obj => false;
        }



        //block shortcut keys I dont want
        private void Browser_ShortcutKeyPressed2(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.A //select all
                || e.Key == Key.F //find
                || e.Key == Key.L //change url
                || e.Key == Key.N //new window
                || e.Key == Key.O //open url
                || e.Key == Key.P //print
                || e.Key == Key.OemPlus //key not recognised in Eto.Forms
                || e.Key == Key.OemMinus //key not recognised in Eto.Forms
                )
                e.Handled = true;
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

