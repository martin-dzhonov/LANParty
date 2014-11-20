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
        private ParseDatabaseRequester _dbRequester;
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
            this._dbRequester = new ParseDatabaseRequester();
            this._parties = new ObservableCollection<Party>();
            this.PopulateParties(category);
        }

        private async void PopulateParties(string category)
        {
            if (category == "Created")
            {
                IEnumerable<ParseObject> asd = await this._dbRequester.GetCreatedPartiesForUser(ParseUser.CurrentUser);
                foreach (ParseObject obj in asd)
                {
                    this._parties.Add(new Party(obj));
                }
            }
            if (category == "Joined")
            {
                IEnumerable<ParseObject> asd = await this._dbRequester.GetJoinedPartiesForUser(ParseUser.CurrentUser);
                foreach (ParseObject obj in asd)
                {
                    this._parties.Add(new Party(obj));
                }
            }
        }
    }
}
