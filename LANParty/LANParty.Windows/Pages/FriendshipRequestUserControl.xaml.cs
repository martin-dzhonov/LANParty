using LANParty.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace LANParty.Pages
{
    public sealed partial class FriendshipRequestUserControl : UserControl
    {
        public event EventHandler Ignored;

        public FriendshipRequestUserControl()
        {
            this.InitializeComponent();
            this.Loaded += FriendshipRequestUserControl_Loaded;
        }

        async void FriendshipRequestUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Ignored += FriendRequestsPage.FriendRequestsPage_UserIgnored;
            FriendshipRequest friendshipRequest = this.DataContext as FriendshipRequest;
            if (friendshipRequest != null)
            {
                if (friendshipRequest.UserAge != 1)
                {
                    TextBlockAge.Text = friendshipRequest.UserAge + " years old";
                }
                else
                {
                    TextBlockAge.Text = friendshipRequest + " year old";
                }
                TextBlockUserSays.Text = friendshipRequest.UserName + " says:";
                TextBlockUserMessage.Text = "\"" + friendshipRequest.UserMessage + "\"";
            }
            else
            {
                //ButtonAccept.Visibility = Visibility.Collapsed;
               // ButtonIgnore.Visibility = Visibility.Collapsed;
                this.Ignored(this, null);
            }
        }

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Tick += timer_Tick;
            ButtonAccept.Visibility = Visibility.Collapsed;
            ButtonIgnore.Visibility = Visibility.Collapsed;
            ProgressRing.IsActive = true;
            timer.Start();
        }

        void timer_Tick(object sender, object e)
        {
            ProgressRing.IsActive = false;
            TextBlockRequestAccepted.Text = "You are now friends with " + (this.DataContext as FriendshipRequest).UserName + "!";
            TextBlockRequestAccepted.Visibility = Visibility.Visible;
        }

        private void ButtonIgnore_Click(object sender, RoutedEventArgs e)
        {
            this.Ignored(this, new EventArgs());
        }
    }
}
