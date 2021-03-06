﻿using LANParty.Models;
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
        public event EventHandler Approved;
        public FriendshipRequestUserControl()
        {
            this.InitializeComponent();
            this.Loaded += FriendshipRequestUserControl_Loaded;
        }

        async void FriendshipRequestUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Ignored += FriendRequestsPage.FriendRequestsPage_UserDeclined;
            this.Approved += FriendRequestsPage.FriendRequestsPage_UserApproved;
            FriendshipRequest friendshipRequest = this.DataContext as FriendshipRequest;
            if (friendshipRequest != null)
            {
                TextBlockUserSays.Text = friendshipRequest.UserName + " says:";
                TextBlockUserMessage.Text = "\"" + "Wow much reqest, very friends, wow" + "\"";
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
            this.Approved(this, new EventArgs());
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
