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

namespace Miotsukushi.View.CheatWindow.Components
{
    /// <summary>
    /// Debugger.xaml の相互作用ロジック
    /// </summary>
    public partial class Debugger : UserControl
    {
        public Debugger()
        {
            InitializeComponent();

            try
            {
                DataContext = new ViewModel.CheatWindow.DebuggerViewModel();
            }
            catch { }
        }
    }
}
