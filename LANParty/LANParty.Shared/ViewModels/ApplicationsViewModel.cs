using LANParty.Common;
using LANParty.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LANParty.ViewModels
{
    class ApplicationsViewModel : Bindable
    {
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
        public ApplicationsViewModel(string partyId)
        {
            this._users = new ObservableCollection<UserProfile>();
            this.PopulateParties(partyId);
        }

        private async void PopulateParties(string partyId)
        {
            ParseDatabaseRequester dbRequester = new ParseDatabaseRequester();
            IEnumerable<ParseObject> asd = await dbRequester.GetApplicationsForParty(partyId);
            foreach (ParseObject obj in asd)
            {
                ParseUser user = await ((ParseUser)obj["guest"]).FetchAsync();
                this._users.Add(new UserProfile((ParseUser)obj["guest"]));
            }
        }
    }
}
