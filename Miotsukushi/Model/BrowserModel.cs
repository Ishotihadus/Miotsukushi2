using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Miotsukushi.Model
{
    class BrowserModel
    {
        #region スクリーンショット

        public class SaveSSCommandRaisedEventArgs : System.EventArgs
        {
            public string filename;

            public SaveSSCommandRaisedEventArgs(string filename)
            {
                this.filename = filename;
            }
        }

        public void RaiseSaveSS()
        {
            var filename = "ss\\ss_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            OnSaveSSCommandRaised(new SaveSSCommandRaisedEventArgs(filename));
        }

        public event SaveSSCommandRaisedEventHandler SaveSSCommandRaised;
        public delegate void SaveSSCommandRaisedEventHandler(object sender, SaveSSCommandRaisedEventArgs e);
        protected virtual void OnSaveSSCommandRaised(SaveSSCommandRaisedEventArgs e) { if (SaveSSCommandRaised != null) { SaveSSCommandRaised(this, e); } }

        #endregion

        #region 更新

        public void BrowserRefresh()
        {
            System.Media.SystemSounds.Beep.Play();
            var result = MessageBox.Show("ブラウザを更新しますか?", "確認",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                OnBrowserRefreshCommandRaised(new EventArgs());
            }
        }

        public event EventHandler BrowserRefreshCommandRaised;
        protected virtual void OnBrowserRefreshCommandRaised(EventArgs e) { if (BrowserRefreshCommandRaised != null) { BrowserRefreshCommandRaised(this, e); } }

        #endregion
    }
}
