using LANParty.Common;
using LANParty.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LANParty.ViewModels
{
    public class FriendRequestsViewModel : Bindable
    {
        private ParseDatabaseRequester _dbRequester;
        private ObservableCollection<UserProfile> _users;

        public ObservableCollection<UserProfile> Users
        {
            get
            {
                return this._users;
            }
            set
            {
                if (value == this._users)
                {
                    return;
                }
                this._users = value;
                OnPropertyChanged();
            }
        }

        public FriendRequestsViewModel()
        {
            this._users = new ObservableCollection<UserProfile>();
            this._dbRequester = new ParseDatabaseRequester();
            this.PopulateData();
        }

        private async void PopulateData()
        {
            IEnumerable<ParseObject> requests = await this._dbRequester.GetFriendRequestsForCurrentUser();
            foreach (ParseObject obj in requests)
            {
                ParseUser user = await ((ParseUser)obj["friend"]).FetchAsync();
                this._users.Add(new UserProfile(user));
            }
        }
    }
}
