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
    [Flags]
    public enum ProfileFields
    {
        uid = 1,
        first_name = 2,
        last_name = 4,
        nickname = 8,
        sex = 16,
        birthdate = 32,
        city = 64,
        country = 128,
        timezone = 512,
        photo = 1024,
        photo_medium = 2048,
        photo_big = 4096,
        domain = 40819296,
        has_mobile = 16384,
        rate = 32768,
        contacts = 65536,
        education = 131072
    }
}
