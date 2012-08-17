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
using System.ComponentModel;
using System.Windows.Media.Imaging;
using ImageTools.IO;
using ImageTools;
using VKMessengerLibrary;

namespace VKMessenger
{
    public class FriendItem : INotifyPropertyChanged
    {
        public FriendItem(Profile p)
        {
            this.uid = p.uid;
            this.first_name = p.first_name;
            this.last_name = p.last_name;
            this.photo = p.photo;
            this.online = p.online;
            this.photo_medium = p.photo_medium;
            this.photo_medium_rec = p.photo_medium_rec;
            this.photo_big = p.photo_big;

        }
        public FriendItem() { }
        public event PropertyChangedEventHandler PropertyChanged;

        public int uid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string photo { get; set; }
        public string photo_medium { get; set; }
        public string photo_medium_rec { get; set; }
        public string photo_big { get; set; }
        public bool online { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", first_name, last_name);
            }
        }

        public Visibility OnlineVisibility
        {
            get
            {
                return online ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private ExtendedImage _im;
        public ExtendedImage ExtendedImage
        {
            get
            {
                if (!string.IsNullOrEmpty(photo))
                    if (_im == null && photo.Contains(".gif"))
                    {
                        var im = new ExtendedImage();
                        im.LoadingCompleted += new EventHandler(im_LoadingCompleted);
                        im.UriSource = new Uri(photo);
                        this._im = im;
                    }
                return _im;
            }
        }

        void im_LoadingCompleted(object sender, EventArgs e)
        {
        }

        private BitmapSource _photo;
        public BitmapSource Photo
        {
            get
            {
                if (!string.IsNullOrEmpty(photo))
                {
                    if (photo.Length == 1)
                    {

                    }

                    if (_photo == null && photo.Contains(".jpg"))
                    {
                        var bmp = new BitmapImage();
                        bmp.ImageFailed += new EventHandler<ExceptionRoutedEventArgs>(bmp_ImageFailed);
                        bmp.ImageOpened += new EventHandler<RoutedEventArgs>(bmp_ImageOpened);
                        bmp.UriSource = new Uri(photo);
                        this._photo = bmp;
                    }
                }
                return _photo;
            }
        }

        public Visibility StartLetterVisibility
        {
            get
            {
                if (!string.IsNullOrEmpty(photo))
                {
                    if (photo.Length == 1)
                    {
                        first_name = "";
                    }
                    return photo.Length == 1 ? Visibility.Visible : Visibility.Collapsed;
                }
                return Visibility.Collapsed;
            }
        }

        void bmp_ImageOpened(object sender, RoutedEventArgs e)
        {
        }

        void bmp_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            var wc = new WebClient();
            /*
            wc.OpenReadCompleted += delegate(object senderr, OpenReadCompletedEventArgs ee)
            {
                var bytes = new byte[ee.Result.Length];
                ee.Result.Read(bytes, 0, (int)ee.Result.Length);
                WriteableBitmap wb = new WriteableBitmap(50, 50);
                wb.SaveJpeg(ee.Result, 50, 50, 0, 100);
                this._photo = wb;
            };
            wc.OpenReadAsync(new Uri((sender as BitmapImage).UriSource.AbsoluteUri));*/
        }
    }
}
