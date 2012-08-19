using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace VKMessenger
{
    public partial class MessageItemControl : UserControl
    {
        public MessageItemControl()
        {
            // Required to initialize variables
            InitializeComponent();
        }

        public static DependencyProperty BrushMessageProperty = DependencyProperty.Register("BrushMessage", typeof(SolidColorBrush), typeof(MessageItemControl), null);


        public SolidColorBrush BrushMessage
        {
            get
            {
                return (SolidColorBrush)GetValue(BrushMessageProperty);
            }
            set
            {
                SetValue(BrushMessageProperty, value);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ItemViewModel item = DataContext as ItemViewModel;
            item.Selected += new EventHandler(item_Selected);
            if (item.Out)
            {
                NoMyM.Visibility = System.Windows.Visibility.Collapsed;
                MyM.Visibility = System.Windows.Visibility.Visible;
                GridMessage.Background = (SolidColorBrush)Resources["BrushMyMessage"];
                LayoutRoot.Margin = new Thickness(95, 0, 0, 20);
            }
            else
            {
                NoMyM.Visibility = System.Windows.Visibility.Visible;
                MyM.Visibility = System.Windows.Visibility.Collapsed;
                GridMessage.Background = (SolidColorBrush)Resources["BrushOtherMessage"];
                LayoutRoot.Margin = new Thickness(0, 0, 0, 20);
            }
            if (item.IsNoSend)
            {
                NoSendMessage.Visibility = System.Windows.Visibility.Visible;
                DateMessage.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                NoSendMessage.Visibility = System.Windows.Visibility.Collapsed;
                DateMessage.Visibility = System.Windows.Visibility.Visible;
            }
            if (item.IsNoRead)
                NoReadMessage.Visibility = System.Windows.Visibility.Visible;
            else
                NoReadMessage.Visibility = System.Windows.Visibility.Collapsed;
        }

        void item_Selected(object sender, EventArgs e)
        {
            ItemViewModel item = DataContext as ItemViewModel;
            if (item.IsSelected)
                GridMessage.Background = (SolidColorBrush)Resources["BrushSelectMessage"];
            else
                GridMessage.Background = item.Out ? (SolidColorBrush)Resources["BrushMyMessage"] : (SolidColorBrush)Resources["BrushOtherMessage"];
        }

    }
}