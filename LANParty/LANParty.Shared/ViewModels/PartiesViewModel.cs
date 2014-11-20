﻿using LANParty.Common;
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
            this.PopulateData(category);
        }

        private async void PopulateData(string category)
        {
            ParseDatabaseRequester dbRequester = new ParseDatabaseRequester();
            IEnumerable<ParseObject> parseParties = await dbRequester.GetPartiesByCategory(category);
            foreach (ParseObject obj in parseParties)
            {
                this._parties.Add(new Party(obj));
            }
        }
    }
}
