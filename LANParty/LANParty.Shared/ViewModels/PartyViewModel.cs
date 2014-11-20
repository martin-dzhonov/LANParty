using LANParty.Common;
using LANParty.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LANParty.ViewModels
{
    public class PartyViewModel : Bindable
    {
        private Party _party;
        private ParseDatabaseRequester dbrequester;

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

        public string ObjectId
        {
            get
            {
                return this._party.ObjectId;
            }
            set
            {
                if (value == this._party.ObjectId)
                {
                    return;
                }
                this._party.ObjectId = value;
                OnPropertyChanged();
            }
        }
        public string Title
        {
            get
            {
                return this._party.Title;
            }
            set
            {
                if (value == this._party.Title)
                {
                    return;
                }
                this._party.Title = value;
                OnPropertyChanged();
            }
        }
        public PartyViewModel(ParseObject objectId)
        {
            this._users = new ObservableCollection<UserProfile>();
            this.dbrequester = new ParseDatabaseRequester();
            this.LoadParty(objectId);
        }

        private async void LoadParty(ParseObject parseParty)
        {
            this._party = new Party(parseParty);
            ParseUser parseHost = await ((ParseUser)parseParty["host"]).FetchAsync();
            this._users.Add(new UserProfile(parseHost));

            var approvedUsers = await this.dbrequester.GetApprovedUsersForParty(parseParty.ObjectId);
            foreach (ParseUser user in approvedUsers)
            {
                await user.FetchAsync();
                this._users.Add(new UserProfile(user));
            }
            
        }
    }
}
