using LANParty.Common;
using LANParty.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LANParty.ViewModels
{
    public class FriendsViewModel : Bindable
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
        private bool _isLoading;

        public bool IsLoading
        {
            get
            {
                return this._isLoading;
            }
            set
            {
                if (value == this._isLoading)
                {
                    return;
                }
                this._isLoading = value;
                OnPropertyChanged();
            }
        }
        public FriendsViewModel()
        {
            this._users = new ObservableCollection<UserProfile>();
            this._dbRequester = new ParseDatabaseRequester();
            this.PopulateData();
        }

        private async void PopulateData()
        {
            this.IsLoading = true;
            IEnumerable<ParseUser> asd = await this._dbRequester.GetFriendsFroCurrentUser();
            foreach (ParseUser item in asd)
            {
                this._users.Add(new UserProfile(item));
            }
            this.IsLoading = false;
        }
    }
}
