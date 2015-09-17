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
            ((App) Current)?.OnUnhandledExceptionRaised(new UnhandledExceptionRaisedEventArgs() { exceptionObject = e.ExceptionObject, exceptionType = "CurrentDomain_UnhandledException" });
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // ハンドルされていない例外2
            OnUnhandledExceptionRaised(new UnhandledExceptionRaisedEventArgs() { innerException = e.Exception, exceptionType = "App_DispatcherUnhandledException" });
            e.Handled = true; // 終了させない
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
#if DEBUG
#else
            DispatcherUnhandledException += App_DispatcherUnhandledException;
#endif

#if DEBUG
            SetCurrentCulture("en-US");
#endif
            Model.MainModel.GetInstance();
            var window = new View.MainWindow();
            window.Closed += Window_Closed;
            window.Show();
        }


        private void SetCurrentCulture(string culname)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culname);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culname);
            Tools.ResourceStringGetter.Culture = new CultureInfo(culname);
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

        public event UnhandledExceptionRaisedEventHandler UnhandledExceptionRaised;
        public delegate void UnhandledExceptionRaisedEventHandler(object sender, UnhandledExceptionRaisedEventArgs e);
        protected virtual void OnUnhandledExceptionRaised(UnhandledExceptionRaisedEventArgs e) { if (UnhandledExceptionRaised != null) { UnhandledExceptionRaised(App.Current, e); } }
    }
}
