using LANParty.Common;
using LANParty.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace LANParty.ViewModels
{
    public class PartyViewModel : Bindable
    {
        private Party _party;
        private ParseDatabaseRequester dbrequester;
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
            this._party = new Party(objectId);
        }

        private async void LoadParty(string objectId)
        {
            ParseObject parseParty = await dbrequester.GetPartyById(objectId);
            this._party = new Party(parseParty);
        }
    }
}
