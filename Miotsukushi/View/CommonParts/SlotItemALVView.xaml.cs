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
using System.ComponentModel;

namespace Miotsukushi.View.CommonParts
{
    /// <summary>
    /// SlotItemALVView.xaml の相互作用ロジック
    /// </summary>
    public partial class SlotItemALVView : UserControl
    {
        public static readonly DependencyProperty ItemALLevelProperty = DependencyProperty.Register("ItemALLevel", typeof(int), typeof(SlotItemALVView));

        [Description("艦載機熟練度"), Category("みおつくし")]
        [BindableAttribute(true)]
        public int ItemALLevel
        {
            get
            {
                return (int)GetValue(ItemALLevelProperty);
            }
            set
            {
                SetValue(ItemALLevelProperty, value);
            }
        }

        public SlotItemALVView()
        {
            InitializeComponent();
        }
    }
}
