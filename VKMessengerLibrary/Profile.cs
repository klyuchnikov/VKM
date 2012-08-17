using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace VKMessengerLibrary
{
    public class Profile
    {
        public int uid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string photo { get; set; }
        public string photo_medium { get; set; }
        public string photo_medium_rec { get; set; }
        public string photo_big { get; set; }
        public string screen_name { get; set; }
        public bool online { get; set; }
        public int sex { get; set; }

        public Profile() { }
    }
}
