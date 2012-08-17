using System;
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
using System.Windows.Navigation;
using Newtonsoft.Json.Linq;
using System.Windows.Data;
using System.Globalization;
using ImageTools.IO;
using ImageTools.IO.Gif;
using Microsoft.Phone.Shell;
using VKMessengerLibrary;

namespace VKMessenger
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Decoders.AddDecoder<GifDecoder>();
            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            var i = SystemTray.ProgressIndicator;
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            App.vk.FriendsGetComplited += new VKService.FriendsEventHandler(vk_FriendsGetComplited);
            App.vk.FriendsGet(ProfileFields.uid | ProfileFields.first_name | ProfileFields.city | ProfileFields.photo | ProfileFields.photo_medium | ProfileFields.photo_big);
        }

        void vk_FriendsGetComplited(Profile[] list)
        {
            var l = list.Select(q => new FriendItem(q)).ToList();
            var best = l.Take(5).ToArray();
            l.AddRange(list.Select(q => q.first_name[0].ToString().ToLower()).Distinct().Select(q => new FriendItem { photo = q.ToString(), first_name = q.ToString() }));
            friends.ItemsSource = best.Union(l.OrderBy(q => q.first_name)).ToArray();
        }

        private void AdvancedApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SearchFriends.xaml", UriKind.Relative));
        }

        private void friends_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}