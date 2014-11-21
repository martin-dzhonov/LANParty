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
                this.IsLoading = true;
                IEnumerable<ParseObject> asd = await this._dbRequester.GetCreatedPartiesForUser(ParseUser.CurrentUser);
                foreach (ParseObject obj in asd)
                {
                    this._parties.Add(new Party(obj));
                }
                this.IsLoading = false;
            }
            if (category == "Joined")
            {
                this.IsLoading = true;
                IEnumerable<ParseObject> asd = await this._dbRequester.GetJoinedPartiesForUser(ParseUser.CurrentUser);
                foreach (ParseObject obj in asd)
                {
                    this._parties.Add(new Party(obj));
                }
                this.IsLoading = false;
            }
        }
    }
}
