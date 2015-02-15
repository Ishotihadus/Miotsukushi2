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
        #region プロパティ定義

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(StatusBar), null);

        [Description("ステータスバーテキスト"), Category("共通"), DefaultValue("Text")]
        [BindableAttribute(true)]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }



        public static readonly DependencyProperty AlertTitleProperty = DependencyProperty.Register("AlertTitle", typeof(string), typeof(StatusBar), null);

        [Description("アラートテキストタイトル"), Category("共通"), DefaultValue("AlertTitle")]
        [BindableAttribute(true)]
        public string AlertTitle
        {
            get { return (string)GetValue(AlertTitleProperty); }
            set { SetValue(AlertTitleProperty, value); }
        }

        public static readonly DependencyProperty AlertTextProperty = DependencyProperty.Register("AlertText", typeof(string), typeof(StatusBar), null);

        [Description("アラートテキスト"), Category("共通"), DefaultValue("AlertText")]
        [BindableAttribute(true)]
        public string AlertText
        {
            get { return (string)GetValue(AlertTextProperty); }
            set { SetValue(AlertTextProperty, value); }
        }

        public static readonly DependencyProperty AlertBGBrushProperty = DependencyProperty.Register("AlertBGBrush", typeof(Brush), typeof(StatusBar), null);

        [Description("アラートテキスト背景"), Category("共通"), DefaultValue("#FFFFFF")]
        [BindableAttribute(true)]
        public Brush AlertBGBrush
        {
            get { return (Brush)GetValue(AlertBGBrushProperty); }
            set { SetValue(AlertBGBrushProperty, value); }
        }

        #endregion

        public StatusBar()
		{
			this.InitializeComponent();

            // DependencyPropertyの変更通知を受け取る
            // なんかもうちょっとこうスマートな方法はないんかいな
            var dpd = DependencyPropertyDescriptor.FromProperty(AlertTextProperty, typeof(StatusBar));
            dpd.AddValueChanged(this, (s, e) => OnAlertTextChangedEvent());
		}


        public static readonly RoutedEvent AlertTextChangedEvent = EventManager.RegisterRoutedEvent("AlertTextChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(StatusBar));

        public event RoutedEventHandler AlertTextChanged
        {
            add { this.AddHandler(AlertTextChangedEvent, value); }
            remove { this.RemoveHandler(AlertTextChangedEvent, value); }
        }

        protected virtual void OnAlertTextChangedEvent() { if (AlertTextChangedEvent != null) { this.RaiseEvent(new RoutedEventArgs(AlertTextChangedEvent, this)); } }
	}
}