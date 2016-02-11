using System.Windows.Controls;
using Miotsukushi.ViewModel.CheatWindow;

namespace Miotsukushi.View.CheatWindow.Components
{
    /// <summary>
    /// ShipList.xaml の相互作用ロジック
    /// </summary>
    public partial class ShipList
    {
        public ShipList()
        {
            InitializeComponent();
            
            try
            {
                DataContext = new ShipListViewModel();
            }
            catch
            {
                // ignored
            }
        }
    }
}
