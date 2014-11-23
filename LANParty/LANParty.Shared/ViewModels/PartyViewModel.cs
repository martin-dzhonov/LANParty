using LANParty.Common;
using LANParty.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;

namespace LANParty.ViewModels
{
    public class PartyViewModel : Bindable
    {
        private ParseDatabaseRequester _dbRequester;

        private Party _party;

        public Party Party
        {
            get
            {
                return this._party;
            }
            set
            {
                if (value == this._party)
                {
                    return;
                }
                this._party = value;
                OnPropertyChanged();
            }
        }

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
            if (!this.IsConnected())
            {
                MessageDialog msgDialog = new MessageDialog("No internet connection, try again later !");
                await msgDialog.ShowAsync();
            }
            else
            {
                ParseObject party = await this._dbRequester.GetPartyById(this._party.ObjectId);
                var spots = (Int64)party["spots"];

                if (((ParseUser)party["host"]).ObjectId == ParseUser.CurrentUser.ObjectId)
                {
                    MessageDialog msgDialog = new MessageDialog("This is your party, dummy !");
                    await msgDialog.ShowAsync();
                }

                else if (spots <= 0)
                {
                    MessageDialog msgDialog = new MessageDialog("No spots left !");
                    await msgDialog.ShowAsync();
                }
                else
                {
                    this.IsLoading = true;
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
                        this.IsLoading = false;
                        await msgDialog.ShowAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageDialog msgDialog = new MessageDialog(ex.Message);
                        this.IsLoading = false;
                        msgDialog.ShowAsync();
                    }
                }
            }
        }

        public PartyViewModel(string partyId)
        {
            this._party = new Party();
            this._users = new ObservableCollection<UserProfile>();
            this._dbRequester = new ParseDatabaseRequester();
            this.PopulateData(partyId);
        }

        private async void PopulateData(string partyId)
        {
            this.IsLoading = true;

            ParseObject parseParty = await this._dbRequester.GetPartyById(partyId);
            DateTime date = (DateTime)parseParty["date"];
            string title = parseParty["title"].ToString();

            this._party.ObjectId = parseParty.ObjectId;
            this._party.Title = title;
            this._party.Description = parseParty["description"].ToString();
            this._party.Spots = parseParty["spots"].ToString();
            this._party.Date = date;

            ParseUser parseHost = await ((ParseUser)parseParty["host"]).FetchIfNeededAsync();
            this._users.Add(new UserProfile(parseHost));

            var approvedUsers = await this._dbRequester.GetApprovedUsersForParty(parseParty.ObjectId);
            foreach (ParseUser user in approvedUsers)
            {
                this._users.Add(new UserProfile(user));
            }
            this.IsLoading = false;
        }

        private bool IsConnected()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }
    }
}
