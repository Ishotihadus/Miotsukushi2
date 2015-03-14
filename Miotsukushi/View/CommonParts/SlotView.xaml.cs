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
	/// SlotView.xaml の相互作用ロジック
	/// </summary>
	public partial class SlotView : UserControl
	{
        public static readonly DependencyProperty ItemTypeBrushProperty = DependencyProperty.Register("ItemTypeBrush", typeof(Brush), typeof(SlotView));

        [Description("装備の種類の色"), Category("みおつくし")]
        [BindableAttribute(true)]
        public Brush ItemTypeBrush
        {
            get
            {
                return GetValue(ItemTypeBrushProperty) as Brush;
            }
            set
            {
                SetValue(ItemTypeBrushProperty, value);
            }
        }

        public static readonly DependencyProperty ItemTypeNameProperty = DependencyProperty.Register("ItemTypeName", typeof(string), typeof(SlotView));

        [Description("装備の種類の名前"), Category("みおつくし")]
        [BindableAttribute(true)]
        public string ItemTypeName
        {
            get
            {
                return GetValue(ItemTypeNameProperty) as string;
            }
            set
            {
                SetValue(ItemTypeNameProperty, value);
            }
        }

        public static readonly DependencyProperty IsSlotEmptyProperty = DependencyProperty.Register("IsSlotEmpty", typeof(bool), typeof(SlotView));

        [Description("スロットが空かどうか"), Category("みおつくし")]
        [BindableAttribute(true)]
        public bool IsSlotEmpty
        {
            get
            {
                return (bool)GetValue(IsSlotEmptyProperty);
            }
            set
            {
                SetValue(IsSlotEmptyProperty, value);
            }
        }

        public static readonly DependencyProperty ItemNameProperty = DependencyProperty.Register("ItemName", typeof(string), typeof(SlotView));

        [Description("アイテムの名前"), Category("みおつくし")]
        [BindableAttribute(true)]
        public string ItemName
        {
            get
            {
                return GetValue(ItemNameProperty) as string;
            }
            set
            {
                SetValue(ItemNameProperty, value);
            }
        }

        public static readonly DependencyProperty OnSlotCountProperty = DependencyProperty.Register("OnSlotCount", typeof(int), typeof(SlotView));

        [Description("搭載数"), Category("みおつくし")]
        [BindableAttribute(true)]
        public int OnSlotCount
        {
            get
            {
                return (int)GetValue(OnSlotCountProperty);
            }
            set
            {
                SetValue(OnSlotCountProperty, value);
            }
        }

        public SlotView()
		{
			this.InitializeComponent();
            
		}
	}
}