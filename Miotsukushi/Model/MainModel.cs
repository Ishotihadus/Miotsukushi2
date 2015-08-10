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
        public TimerModel timerModel { get; private set; }
        public KanColleModel kancolleModel { get; private set; }
        public Audio.VolumeModel volumeModel { get; private set; }

        private View.CheatWindow.CheatWindow _cheat_window;

        public void OpenCheatWindow()
        {
            if (_cheat_window != null && _cheat_window.IsVisible)
                _cheat_window.Activate();
            else
            {
                if (_cheat_window == null)
                {
                    _cheat_window = new View.CheatWindow.CheatWindow();
                    _cheat_window.Closing += _cheat_window_Closing;
                }
                _cheat_window.Show();
            }
        }

        private void _cheat_window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            _cheat_window.Hide();
        }

        private MainModel()
        {
            timerModel = new TimerModel();
            kancolleModel = new KanColleModel();
            volumeModel = new Audio.VolumeModel();
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
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
                _cheat_window.Closing -= _cheat_window_Closing;
                _cheat_window.Close();
                _cheat_window = null;
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        ~MainModel()
        {
            Dispose();
        }

        #endregion
    }
}
