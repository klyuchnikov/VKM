using Microsoft.Phone.Controls;
using System;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace VKMessengerLibrary
{
    public class VKService
    {
        public delegate void EventHandler();
        public event EventHandler Authorized;
        public event EventHandler AuthorizedError;
        public delegate void FriendsEventHandler(Profile[] list);
        public event FriendsEventHandler FriendsGetComplited;

        public bool IsAuthorized = false;
        private readonly WebBrowser webBrowser;
        string autorize = "http://oauth.vk.com/oauth/authorize?client_id=2660600&scope=messages,friends&redirect_uri=http://oauth.vk.com/blank.html&display=popup&response_type=token&_hash=0";
        private string loginScript;
        private string access_token;
        private string expires_in;
        private string user_id;
        public VKService(WebBrowser _webBrowser)
        {
            webBrowser = _webBrowser;
            webBrowser.LoadCompleted += webBrowser1_LoadCompleted;
        }

        public void webBrowser1_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (webBrowser.Source.ToString() == autorize)
            {
                try
                {
                    webBrowser.InvokeScript("eval", loginScript);
                }
                catch { };
            }
            else if (webBrowser.SaveToString().Contains("Login success"))
            {
                var arr = webBrowser.Source.Fragment.Substring(1).Split('&');
                access_token = arr[0].Split('=')[1];
                expires_in = arr[1].Split('=')[1];
                user_id = arr[2].Split('=')[1];
                IsAuthorized = true;
                Authorized();
            }
            else
                AuthorizedError();
        }

        public void auth(string login, string password)
        {
            loginScript = "document.getElementsByName('email')[0].value='" + login + "';document.getElementsByName('pass')[0].value = '" + password + "';document.getElementById('install_allow').click();void(0);";
            webBrowser.Navigate(new System.Uri(autorize));
        }

        public void FriendsGet(ProfileFields fields)
        {
            var wc = new WebClient();
            wc.DownloadStringCompleted += delegate(object sender, DownloadStringCompletedEventArgs e)
            {
                var ob = JObject.Parse(e.Result);
                var list = ob["response"].Select(q => new Profile
                {
                    uid = (int)q["uid"],
                    first_name = (string)q["first_name"],
                    last_name = (string)q["last_name"],
                    online = (int)q["online"] == 1,
                    photo = (string)q["photo"],
                    photo_big = (string)q["photo_big"],
                    photo_medium = (string)q["photo_medium"],
                    sex = q["sex"] != null ? (int)q["sex"] : 0 // и .т.д.
                }).ToArray();
                FriendsGetComplited(list);

            };
            wc.DownloadStringAsync(new Uri(string.Format("https://api.vk.com/method/friends.get?access_token={0}&fields={1}&order=hints", access_token, fields.ToString().Replace(" ", ""))));
        }
    }


}
