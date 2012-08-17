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
using Newtonsoft.Json.Linq;
using Microsoft.Phone.Shell;
using VKMessengerLibrary;

namespace VKMessenger
{
    public partial class Auth : PhoneApplicationPage
    {
        public VKService vk;
        public Auth()
        {
            InitializeComponent();
<<<<<<< HEAD
            App.vk = new VKService(web);
=======
            vk = new VKService(web);
>>>>>>> Revert "вынес экземпляр класса VKService сюда"
            web.IsScriptEnabled = true;
            vk.Authorized += new VKService.EventHandler(vk_Authorized);
        }

        void vk_Authorized()
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

            vk.FriendsGetComplited += new VKService.FriendsEventHandler(vk_FriendsGetComplited);
            vk.FriendsGet(ProfileFields.uid | ProfileFields.first_name | ProfileFields.city);
        }

        void vk_FriendsGetComplited(Profile[] list)
        {
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vk.auth(loginTB.Text, passTB.Password);
            SystemTray.ProgressIndicator.IsVisible = true;
        }
    }
}