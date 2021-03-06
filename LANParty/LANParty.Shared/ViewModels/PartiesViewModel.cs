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
        private ParseDatabaseRequester _dbRequester;
        private ObservableCollection<Party> _parties;
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
            this._dbRequester = new ParseDatabaseRequester();
            this._parties = new ObservableCollection<Party>();
            this.PopulateData(category);
        }

        private async void PopulateData(string category)
        {
            this.IsLoading = true;
            IEnumerable<ParseObject> parseParties = await this._dbRequester.GetPartiesByCategory(category);
            foreach (ParseObject obj in parseParties)
            {
                this._parties.Add(new Party(obj));
            }
            this.IsLoading = false;
        }
    }
}
