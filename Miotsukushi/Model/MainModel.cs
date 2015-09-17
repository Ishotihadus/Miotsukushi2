using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miotsukushi.Model.KanColle;

namespace Miotsukushi.Model
{
    class MainModel : IDisposable
    {
        public TimerModel TimerModel { get; private set; }
        public KanColleModel KancolleModel { get; private set; }
        public Audio.VolumeModel VolumeModel { get; private set; }

        public ThemeModel ThemeModel { get; private set; }

        public BrowserModel BrowserModel { get; private set; }

        private View.CheatWindow.CheatWindow _cheatWindow;

        public void OpenCheatWindow()
        {
            if (_cheatWindow != null && _cheatWindow.IsVisible)
                _cheatWindow.Activate();
            else
            {
                if (_cheatWindow == null)
                {
                    _cheatWindow = new View.CheatWindow.CheatWindow();
                    _cheatWindow.Closing += _cheat_window_Closing;
                }
                _cheatWindow.Show();
            }
        }

        private void _cheat_window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            _cheatWindow.Hide();
        }

        private MainModel()
        {
            TimerModel = new TimerModel();
            KancolleModel = new KanColleModel();
            VolumeModel = new Audio.VolumeModel();
            ThemeModel = new ThemeModel();
            BrowserModel = new BrowserModel();
        }

        #region インスタンス単一化

        public static MainModel Current { get; protected set; }

        public static MainModel GetInstance()
        {
            if (Current == null)
                Current = new MainModel();

            return Current;
        }

        public static void CurrentClose()
        {
            Current.Dispose();
            Current = null;
        }

        #endregion

        #region Dispose

        // Flag: Has Dispose already been called?
        bool _disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
                if (_cheatWindow != null)
                {
                    _cheatWindow.Closing -= _cheat_window_Closing;
                    _cheatWindow.Close();
                    _cheatWindow = null;
                }
            }

            // Free any unmanaged objects here.
            //
            _disposed = true;
        }

        ~MainModel()
        {
            Dispose();
        }

        #endregion
    }
}
