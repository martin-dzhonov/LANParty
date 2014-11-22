using LANParty.Common;
using LANParty.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Windows.UI.Popups;

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
        private ICommand _applyCommand;
        public ICommand Apply
        {
            get
            {
                if (this._applyCommand == null)
                {
                    this._applyCommand = new DelegateCommand(this.ApplyForParty);
                }
                return this._applyCommand;
            }
        }
        private async void ApplyForParty()
        {
            ParseObject party = await this._dbRequester.GetPartyById(this._party.ObjectId);
            if (((ParseUser)party["host"]).ObjectId == ParseUser.CurrentUser.ObjectId)
            {
                MessageDialog msgDialog = new MessageDialog("This is your party, dummy !");
                msgDialog.ShowAsync();
            }
            else
            {
                ParseObject application = new ParseObject("Application");
                
                application["partyId"] = party.ObjectId;
                application["host"] = party["host"];
                application["guest"] = ParseUser.CurrentUser;
                application["approved"] = false;
                application["declined"] = false;
                try
                {
                    await application.SaveAsync();
                    MessageDialog msgDialog = new MessageDialog("Successfully applied.");
                    msgDialog.ShowAsync();
                }
                catch (Exception ex)
                {

                }
            }
        }

        private string _partyId;
        public PartyViewModel(ParseObject party)
        {
            this._party = new Party(party);
            this._partyId = party.ObjectId;
            this._users = new ObservableCollection<UserProfile>();
            this._dbRequester = new ParseDatabaseRequester();
            this.PopulateData(party);
        }

        private async void PopulateData(ParseObject parseParty)
        {
            this.IsLoading = true;
            ParseUser parseHost = await ((ParseUser)parseParty["host"]).FetchIfNeededAsync();
            this._users.Add(new UserProfile(parseHost));

            var approvedUsers = await this._dbRequester.GetApprovedUsersForParty(parseParty.ObjectId);
            foreach (ParseUser user in approvedUsers)
            {
                this._users.Add(new UserProfile(user));
            }
            this.IsLoading = false;
        }
    }
}
