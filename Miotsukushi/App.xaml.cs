using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Miotsukushi
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
#if DEBUG
#else
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
#endif
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // ハンドルされていない例外

        }
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // ハンドルされていない例外2
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
#if DEBUG
#else
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
#endif
            
#if DEBUG
            SetCurrentCulture("en-US");
#endif
            GetCurrentCulture();
            System.Diagnostics.Debug.WriteLine(Tools.ResourceStringGetter.GetShipNameResourceString("天津風改二"));
            Model.MainModel.GetInstance();
            var Window = new View.MainWindow();
            Window.Closed += Window_Closed;
            Window.Show();
        }


        private void SetCurrentCulture(string culname)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culname);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culname);
            Tools.ResourceStringGetter.culture = new CultureInfo(culname);
        }

        private void GetCurrentCulture()
        {
            System.Diagnostics.Debug.WriteLine("CurrentCulture: " + Thread.CurrentThread.CurrentCulture.Name);
            System.Diagnostics.Debug.WriteLine("CurrentUICulture: " + Thread.CurrentThread.CurrentUICulture.Name);
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Window_Closed(object sender, EventArgs e)
        {
            Model.MainModel.CurrentClose();
        }
    }
}
