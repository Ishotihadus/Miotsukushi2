using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

            try
            {
                Model.MainModel.Current.kancolleModel.GameStart += kancolleModel_GameStart; // ゆるせ、後で何とかする
                Model.MainModel.Current.SaveSSCommandRaised += (o, e) => SaveScreenShot(e.filename);
            }
            catch { }

            // 起動時の処理（ズームおよびスクリプトエラーの抑制）
            bool rendered = false;
            webBrowser.Loaded += (o, e) =>
                System.Windows.PresentationSource.FromVisual((Visual)o).ContentRendered += (sender, args) =>
                {
                    if (!rendered)
                    {
                        BrowserSupressScriptError();
                        BrowserZoom();
                    }
                    rendered = true;
                };
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

        [ComImport, Guid("6d5140c1-7436-11ce-8034-00aa006009fa"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IServiceProvider
        {
            [return: MarshalAs(UnmanagedType.IUnknown)]
            object QueryService(ref Guid guidService, ref Guid riid);
        }

        private static readonly Guid SID_SWebBrowserApp = new Guid("0002DF05-0000-0000-C000-000000000046");

        /// <summary>
        /// DPIを算出して自動でズームする
        /// </summary>
        public void BrowserZoom()
        {
            var desktop = System.Drawing.Graphics.FromHwnd(((System.Windows.Interop.HwndSource)System.Windows.PresentationSource.FromVisual(this)).Handle);
            var dpiX = desktop.DpiX;
            var dpiY = desktop.DpiY;

            var scalefactor_x = dpiX / 96;
            var scalefactor_y = dpiX / 96;

            int scale = (int)(scalefactor_x * scalefactor_x * 100);

            Guid serviceGuid = SID_SWebBrowserApp;
            Guid iid = typeof(SHDocVw.IWebBrowser2).GUID;
            SHDocVw.IWebBrowser2 webBrowser2 = (SHDocVw.IWebBrowser2)((IServiceProvider)webBrowser.Document).QueryService(ref serviceGuid, ref iid);
            object pvaIn = scale;

            webBrowser2.ExecWB(SHDocVw.OLECMDID.OLECMDID_OPTICAL_ZOOM, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT, ref pvaIn);
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

        /// <summary>
        /// スクリーンショットを保存します
        /// </summary>
        /// <param name="filename"></param>
        public void SaveScreenShot(string filename)
        {
            var dg = VisualTreeHelper.GetDrawing(webBrowser);
            var width = webBrowser.ActualWidth;
            var height = webBrowser.ActualHeight;
            
            var desktop = System.Drawing.Graphics.FromHwnd(((System.Windows.Interop.HwndSource)System.Windows.PresentationSource.FromVisual(this)).Handle);
            var dpiX = desktop.DpiX;
            var dpiY = desktop.DpiY;

            var scalefactor_x = dpiX / 96;
            var scalefactor_y = dpiX / 96;

            var dv = new DrawingVisual();
            using (var dc = dv.RenderOpen())
            {
                dc.DrawDrawing(dg);
            }
            
            var bitmap = new RenderTargetBitmap((int)(width * scalefactor_x), (int)(height * scalefactor_y), dpiX, dpiY, PixelFormats.Pbgra32);
            bitmap.Render(dv);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            using (var stream = new FileStream(filename, FileMode.Create))
            {
                encoder.Save(stream);
            }
            
        }
    }
}
