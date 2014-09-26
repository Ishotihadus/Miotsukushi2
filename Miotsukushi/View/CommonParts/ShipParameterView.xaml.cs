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

namespace Miotsukushi.View.CommonParts
{
	/// <summary>
	/// ShipParameterView.xaml の相互作用ロジック
	/// </summary>
	public partial class ShipParameterView : UserControl
	{
        public static readonly DependencyProperty ParameterNameProperty = DependencyProperty.Register("ParameterName", typeof(string), typeof(ShipParameterView));

        [Description("パラメータの名前"), Category("みおつくし")]
        [BindableAttribute(true)]
        public string ParameterName
        {
            get
            {
                return GetValue(ParameterNameProperty) as string;
            }
            set
            {
                SetValue(ParameterNameProperty, value);
            }
        }

        public static readonly DependencyProperty ParameterValueProperty = DependencyProperty.Register("ParameterValue", typeof(int), typeof(ShipParameterView));

        [Description("パラメータの値"), Category("みおつくし")]
        [BindableAttribute(true)]
        public int ParameterValue
        {
            get
            {
                return (int)GetValue(ParameterValueProperty);
            }
            set
            {
                SetValue(ParameterValueProperty, value);
            }
        }

		public ShipParameterView()
		{
			this.InitializeComponent();
		}
	}
}