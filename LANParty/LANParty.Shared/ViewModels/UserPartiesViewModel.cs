using LANParty.Common;
using LANParty.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LANParty.ViewModels
{
    class UserPartiesViewModel : Bindable
    {
        private ObservableCollection<Party> _parties;
        public ObservableCollection<Party> Parties
        {
            get
            {
                return this._parties;
            }
            set
            {
                if (value == this._parties)
                {
                    return;
                }
                this._parties = value;
                OnPropertyChanged();
            }
        }
        public UserPartiesViewModel(string category)
        {
            this._parties = new ObservableCollection<Party>();
            this.PopulateParties(category);
        }

        private async void PopulateParties(string category)
        {
            if (category == "Created")
            {
                ParseDatabaseRequester dbRequester = new ParseDatabaseRequester();
                IEnumerable<ParseObject> asd = await dbRequester.GetCreatedPartiesForUser(ParseUser.CurrentUser);
                foreach (ParseObject obj in asd)
                {
                    this._parties.Add(new Party(obj));
                }
            }
            if (category == "Joined")
            {
                ParseDatabaseRequester dbRequester = new ParseDatabaseRequester();
                IEnumerable<ParseObject> asd = await dbRequester.GetJoinedPartiesForUser(ParseUser.CurrentUser);
                foreach (ParseObject obj in asd)
                {
                    this._parties.Add(new Party(obj));
                }
            }
        }
    }
}
