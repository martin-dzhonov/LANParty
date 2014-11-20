using LANParty.Common;
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
        public MessagesViewModel()
        {
            this._messages = new ObservableCollection<Message>();
            this.PopulateParties();
        }

        private async void PopulateParties()
        {
            ParseDatabaseRequester dbRequester = new ParseDatabaseRequester();
            IEnumerable<ParseObject> asd = await dbRequester.GetMessagesForCurrentUser();
            foreach (ParseObject obj in asd)
            {
                this._messages.Add(new Message(obj));
            }
        }
    }
}
