﻿using LANParty.Common;
using LANParty.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LANParty.ViewModels
{
    public class MessagesViewModel : Bindable
    {
        private ParseDatabaseRequester _dbRequester;
        private ObservableCollection<Message> _messages;
        public ObservableCollection<Message> Messages
        {
            get
            {
                return this._messages;
            }
            set
            {
                if (value == this._messages)
                {
                    return;
                }
                this._messages = value;
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
        public MessagesViewModel()
        {
            this._messages = new ObservableCollection<Message>();
            this._dbRequester = new ParseDatabaseRequester();
            this.PopulateData();
        }

        private async void PopulateData()
        {
            this.IsLoading = true;
            IEnumerable<ParseObject> asd = await this._dbRequester.GetMessagesForCurrentUser();
            foreach (ParseObject obj in asd)
            {
                this._messages.Add(new Message(obj));
            }
            this.IsLoading = false;
        }
    }
}
