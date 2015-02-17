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
