﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace VKMessenger
{
    public partial class Conversation : PhoneApplicationPage
    {
        public Conversation()
        {
            InitializeComponent();
            messages.ItemsSource = App.ViewModel.Items;
            if (!App.ViewModel.IsDataLoaded)
                App.ViewModel.LoadData();
        }

        private void messages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (ItemViewModel a in messages.ItemsSource)
                a.IsSelected = false;
            foreach (ItemViewModel a in messages.SelectedItems)
                a.IsSelected = true;
        }
    }
}