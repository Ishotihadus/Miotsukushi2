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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Miotsukushi.Tools;

namespace Miotsukushi.View
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            /*
            e.Cancel = true;
            
            var result = await this.ShowMessageAsync(
                ResourceStringGetter.GetResourceString("WindowCloseConfirmationTitle"),
                ResourceStringGetter.GetResourceString("WindowCloseConfirmationMessage"),
                MessageDialogStyle.AffirmativeAndNegative);
            

            if (result == MessageDialogResult.Affirmative)
            {
                this.Closing -= MainWindow_Closing;
                this.Close();
            }
             * 
             * */

            var result = MessageBox.Show(ResourceStringGetter.GetResourceString("WindowCloseConfirmationMessage"), ResourceStringGetter.GetResourceString("WindowCloseConfirmationTitle"), 
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if(result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
