using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Miotsukushi.View.WindowParts.DetailInfoParts.GeneralParts
{
	/// <summary>
	/// Basic.xaml の相互作用ロジック
	/// </summary>
	public partial class Basic : UserControl
	{
		public Basic()
		{
			this.InitializeComponent();
            try
            {
                this.DataContext = new ViewModel.DetailInfoPanel.GeneralParts.BasicPanelViewModel();
            }
            catch { }
		}
	}
}