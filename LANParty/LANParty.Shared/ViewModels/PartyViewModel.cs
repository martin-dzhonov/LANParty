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

        public string Description
        {
            get
            {
                return this._party.Description;
            }
            set
            {
                if (value == this._party.Description)
                {
                    return;
                }
                this._party.Description = value;
                OnPropertyChanged();
            }
        }
        public PartyViewModel(ParseObject objectId)
        {
            this._users = new ObservableCollection<UserProfile>();
            this._dbRequester = new ParseDatabaseRequester();
            this.PopulateData(objectId);
        }

        private async void PopulateData(ParseObject parseParty)
        {
            this._party = new Party(parseParty);
            ParseUser parseHost = await ((ParseUser)parseParty["host"]).FetchIfNeededAsync();
            this._users.Add(new UserProfile(parseHost));

            var approvedUsers = await this._dbRequester.GetApprovedUsersForParty(parseParty.ObjectId);
            foreach (ParseUser user in approvedUsers)
            {
                this._users.Add(new UserProfile(user));
            }
        }
    }
}
