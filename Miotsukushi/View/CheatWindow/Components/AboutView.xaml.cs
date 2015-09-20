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
using Miotsukushi.ViewModel.CheatWindow;

namespace Miotsukushi.View.CheatWindow.Components
{
    /// <summary>
    /// About.xaml の相互作用ロジック
    /// </summary>
    public partial class AboutView : UserControl
    {
        public AboutView()
        {
            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(Mascotimage, BitmapScalingMode.HighQuality);

            try
            {
                DataContext = new AboutViewModel();
            }
            catch
            {
            }
        }
    }
}
