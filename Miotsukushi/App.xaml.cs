﻿using System;
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
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // GPUによるハードウェアアクセラレーションを無効化する
            // あとから設定で変えられるようにしよう
            System.Windows.Media.RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
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
