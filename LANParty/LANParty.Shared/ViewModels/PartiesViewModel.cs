using LANParty.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using LANParty.Models;
using Parse;

namespace LANParty.ViewModels
{
    public class PartiesViewModel : Bindable
    {
        private ObservableCollection<Party> _parties;
        private IEnumerable<ParseObject> asd;
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
        public PartiesViewModel(string category)
        {
            this._parties = new ObservableCollection<Party>();
            PopulateParties();
        }

        private async void PopulateParties()
        {
            ParseDatabaseRequester dbRequester = new ParseDatabaseRequester();
            asd = await dbRequester.GetPartiesByCategory("Dota2");
            foreach (ParseObject obj in asd)
            {
                this._parties.Add(new Party(obj));
            }
        }
    }
}
