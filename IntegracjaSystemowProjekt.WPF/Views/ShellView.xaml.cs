﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IntegracjaSystemowProjekt.WPF.ViewModels;

namespace IntegracjaSystemowProjekt.WPF.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public event EventHandler BeforeSearch;
        public ShellView()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }

        public static bool IsValid(string str)
        {
            return int.TryParse(str, out var i) && i >= 0;
        }


        public void ExecuteSearch()
        {
            BeforeSearch?.Invoke(null, EventArgs.Empty);
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //if(!ShellViewModel.IsInLoadingState)
            //    CollectionViewSource.GetDefaultView(Records)?.Refresh();
        }
    }
}
