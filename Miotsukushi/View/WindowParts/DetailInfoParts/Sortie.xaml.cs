﻿using System;
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

namespace Miotsukushi.View.WindowParts.DetailInfoParts
{
    /// <summary>
    /// Sortie.xaml の相互作用ロジック
    /// </summary>
    public partial class Sortie : UserControl
    {
        public Sortie()
        {
            InitializeComponent();

            try
            {
                DataContext = new ViewModel.DetailInfoPanel.Sortie.SortieViewModel();
            }
            catch { }
        }
    }
}