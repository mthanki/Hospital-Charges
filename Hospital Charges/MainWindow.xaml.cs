﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital_Charges
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VM vm = new VM();
        MaterialDesignThemes.Wpf.PaletteHelper ph = new MaterialDesignThemes.Wpf.PaletteHelper();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewTaxToggle_Changed(object sender, RoutedEventArgs e)
        {
            var toggle = sender as ToggleButton;
            if (!toggle.IsChecked ?? false)
                vm.AddChargesWithoutTax();
            else
                vm.AddChargesWithTax();
        }
    }
}
