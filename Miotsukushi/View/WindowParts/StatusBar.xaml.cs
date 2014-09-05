using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Miotsukushi.View.WindowParts
{
	/// <summary>
	/// StatusBar.xaml の相互作用ロジック
	/// </summary>
	public partial class StatusBar : UserControl
    {
        #region Message プロパティ定義

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(StatusBar), null);

        [Description("ステータスバーテキスト"), Category("共通"), DefaultValue("Text")]
        [BindableAttribute(true)]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion

        public StatusBar()
		{
			this.InitializeComponent();
		}
	}
}