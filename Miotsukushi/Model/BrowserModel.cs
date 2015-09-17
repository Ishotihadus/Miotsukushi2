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

        public class SaveSsCommandRaisedEventArgs : System.EventArgs
        {
            public string Filename;

            public SaveSsCommandRaisedEventArgs(string filename)
            {
                this.Filename = filename;
            }
        }

        public void RaiseSaveSs()
        {
            var filename = "ss\\ss_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            OnSaveSsCommandRaised(new SaveSsCommandRaisedEventArgs(filename));
        }

        public event SaveSsCommandRaisedEventHandler SaveSsCommandRaised;
        public delegate void SaveSsCommandRaisedEventHandler(object sender, SaveSsCommandRaisedEventArgs e);
        protected virtual void OnSaveSsCommandRaised(SaveSsCommandRaisedEventArgs e) { if (SaveSsCommandRaised != null) { SaveSsCommandRaised(this, e); } }

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
