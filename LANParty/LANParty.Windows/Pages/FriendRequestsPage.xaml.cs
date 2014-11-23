using LANParty.Common;
using LANParty.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace LANParty.Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class FriendRequestsPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public FriendRequestsPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            this.Loaded += FriendRequestsPage_Loaded;
        }
        ObservableCollection<FriendshipRequest> friendshipRequestList = new ObservableCollection<FriendshipRequest>();

        void FriendRequestsPage_Loaded(object sender, RoutedEventArgs e)
        {
            friendshipRequestList = new ObservableCollection<FriendshipRequest>();
            friendshipRequestList.Add(new FriendshipRequest() { UserName = "Doge", UserAge = 3, UserGender = "Male", UserJob = "Dog", UserMessage = "wow such request\nplz accept\nmuch friend", UserImage = "http://discoverygc.com/wiki/images/thumb/3/34/Doge_(1).jpg/264px-Doge_(1).jpg" });
            friendshipRequestList.Add(new FriendshipRequest() { UserName = "GlaDOS", UserAge = 532, UserGender = "Female", UserJob = "Not a homicidal AI", UserMessage = "Please accept my friend request so I can use you as test sub.. Ehm, so I could give you some cake. Really.", UserImage = "http://images.wikia.com/half-life/en/images/4/4d/Glados_new_body.jpg" });
            friendshipRequestList.Add(new FriendshipRequest() { UserName = "Nigerian Prince", UserAge = 52, UserGender = "Male", UserJob = "Prince", UserMessage = "Hello friend. I'm looking for someone I can trust with a bank transfer involving $500.000.000. Please accept my request and send me your information as soon as possible.", UserImage = "http://ethicsalarms.files.wordpress.com/2011/03/nigerian-prince.jpg" });
            friendshipRequestList.Add(new FriendshipRequest() { UserName = "John Shepard", UserAge = 32, UserGender = "Male", UserJob = "Commander", UserMessage = "Hi, I'm Commander Shepard, and this is my favorite app on the Citadel.", UserImage = "http://images4.wikia.nocookie.net/__cb20120213040953/knowyourmeme/images/thumb/2/20/Commander_Shepard.jpg/398px-Commander_Shepard.jpg" });
            friendshipRequestList.Add(new FriendshipRequest() { UserName = "Slenderman", UserAge = 99999999, UserGender = "Not available", UserJob = "bzzzrrtrt", UserMessage = "Á̳̙̰͖͝ĺ̞̭̜͖̭͖̩̝̻͠ẃ̷̥̠̜̹̻͙̳̩a͏̦͓͇̭y̨̞̺̖͇͍s̨̠̖͠ ̧͖̹̠͍̦͇͟ͅͅẃ̸̭̗̗̳͖̖̭̫a̷̶̮̹̮̩̯̟̲t̖̯͉c҉̖h̢̖̹̤͇̥̙̻̬͘è̢̤̭̣̠͕ş͉̲ͅ,̵̧͎̜ ̪̟̭̱̗̤̟̥ͅn̪̙̙͍̳̲̳̟o̴͉ ̠͈̦̩͚̞̝̺̯͠e̖̼̟̤̪̟y̸̟̝̖e̶̦͢͝ͅs̴̫̥̳͇͚͎̘͝ͅ.͏̴̳͉̺̣̥͘ ͕̳̫͙͚̼͍͜\nḐ͉̹͔̩̤͔͔̕o̧̖̦̺̲̲̖͢n̮͉̠͕̗͚̖̥͘ͅ'̹̫̬͙̲̲͙̞t͉̠̩̜ ̢̘̻̙ļ̶̡͎̺o͎͓̗͚̗ó̦̫̥̹k͚ͅ,҉̢̣͎͇͙̺̠̜ ̮̫̘͖͡o̶̞͔̳̕r̥̫̩̮̬͙͈̪̀̕ ̭͉ͅí̞̺̳͕͚t̡͔͎͇́͝ ̢҉̯̩̪͍͍̜͈t̸̢̢̯̥ą̼̘̬̳k̷̕͏͕̣̲̘̜̟e̸̷҉̖̲̫̪̭ș̬̝̖͟͠ ̸͏̴̩̱̻̙̗͔̥y̭̹̞o͖̺͠ư͙̯̘͟.͉̳̺̺̹̙̩̳͡ ̳̬̬͍̬̺̗\nL̨̥̮͚͍e͇̲̕̕á̧̫̤̠̤̤̥͚͝v̴͖̤͇̟̳̥͔̤͟e̢͓̝̯͇͘ ͕͍̳̕ͅm̴̥̳͍͉̙͕͍̫̩e̵̛̠̮͓̦̖̤̗ ̶̸̙͖͚̬̣ą̼̥̼́͡l̳͟͟ͅơ̴̼͕̗ͅn̶̴̝̣̥̗e̜.̰̖̣͓̹̭͕̟ ̠̳̙̯́͞\nḆ̸̩̹͟͜è̤͍h̡̛̯̮̝i̢̛͚̝̻̺͓͔̲̗̲n̨̛͕͙̺̠̫d̸̴̘̝ ̶̺͎̟̹͉͕͎͞y̹̞̟̙̮̭̭̫o̵̺̹͟͠ù̧̞͖̟͎̯̩̹̘.̷̗̠̟͙̥̞̞̝", UserImage = "http://d38zt8ehae1tnt.cloudfront.net/Slenderman__109647.jpg" });
            GridViewFriendshipRequests.ItemsSource = friendshipRequestList;
        }
        public static void FriendRequestsPage_UserIgnored(object sender, EventArgs e)
        {
            FriendshipRequestUserControl friendshipRequestUserControl = sender as FriendshipRequestUserControl;
            FriendshipRequest friendshipRequest = friendshipRequestUserControl.DataContext as FriendshipRequest;
            ((Window.Current.Content as Frame).Content as FriendRequestsPage).RemoveRequest(friendshipRequest);
        }

        private void RemoveRequest(FriendshipRequest friendshipRequest)
        {
            friendshipRequestList.Remove(friendshipRequest);
        }
        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
