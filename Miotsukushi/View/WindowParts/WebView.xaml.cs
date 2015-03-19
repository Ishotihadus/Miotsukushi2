using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;

namespace Miotsukushi.View.WindowParts
{
    /// <summary>
    /// WebView.xaml の相互作用ロジック
    /// </summary>
    public partial class WebView : UserControl
    {
        public WebView()
        {
            InitializeComponent();
            BrowserSupressScriptError();

            Model.MainModel.Current.kancolleModel.GameStart += kancolleModel_GameStart; // ゆるせ、後で何とかする
        }

        /// <summary>
        /// ゲームスタート時に呼び出されるアレ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void kancolleModel_GameStart(object sender, EventArgs e)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => kancolleModel_GameStart(sender, e));
                return;
            }

            try
            {
                mshtml.HTMLDocument htmlDoc = webBrowser.Document as mshtml.HTMLDocument;
                if (htmlDoc != null)
                {
                    htmlDoc.parentWindow.execScript("document.getElementById('game_frame').style.left='-50px'");
                    htmlDoc.parentWindow.execScript("document.getElementById('game_frame').style.top='-16px'");
                    htmlDoc.parentWindow.execScript("document.getElementById('game_frame').style.zIndex='1024'");
                    htmlDoc.parentWindow.execScript("document.getElementById('game_frame').style.position='fixed'");
                    htmlDoc.parentWindow.execScript("document.body.style.overflow ='hidden'");
                    htmlDoc.parentWindow.scrollTo(0, 0);
                }
            }
            catch (System.Runtime.InteropServices.COMException exception)
            {
                System.Diagnostics.Debug.WriteLine(exception);
            }
        }

        /// <summary>
        /// ブラウザを指定位置にスクロールしてスクロールバーを消去します
        /// </summary>
        /// <param name="x">スクロール先のx座標</param>
        /// <param name="y">スクロール先のy座標</param>
        public void BrowserScroll(int x, int y)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => BrowserScroll(x, y));
                return;
            }

            mshtml.HTMLDocument htmlDoc = webBrowser.Document as mshtml.HTMLDocument;
            if (htmlDoc != null) htmlDoc.parentWindow.scrollTo(x, y);
            htmlDoc.parentWindow.execScript("document.body.style.overflow ='hidden'");
        }

        /// <summary>
        /// ブラウザを更新します
        /// </summary>
        public void BrowserRefresh()
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => BrowserRefresh());
                return;
            }
            webBrowser.Refresh();
        }

        /// <summary>
        /// ブラウザを指定されたURLに遷移します
        /// </summary>
        /// <param name="source">移動先のURL</param>
        public void BrowserNavigate(string source)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => BrowserNavigate(source));
                return;
            }
            webBrowser.Navigate(source);
        }

        /// <summary>
        /// ブラウザのスクリプトエラーを抑制します
        /// </summary>
        public void BrowserSupressScriptError()
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => BrowserSupressScriptError());
                return;
            }

            var axIWebBrowser2 = typeof(WebBrowser).GetProperty("AxIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            var comObj = axIWebBrowser2.GetValue(webBrowser, null);
            comObj.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, comObj, new object[] { true });
        }
    }
}
