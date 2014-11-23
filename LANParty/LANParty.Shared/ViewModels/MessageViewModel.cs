using LANParty.Common;
using LANParty.Models;
using LANParty.Pages;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace LANParty.ViewModels
{
    public class MessageViewModel : Bindable
    {
        private string _senderId;
        private ParseDatabaseRequester _dbRequester;
        public string SenderId
        {
            get
            {
                return this._senderId;
            }
            set
            {
                if (value == this._senderId)
                {
                    return;
                }
                this._senderId = value;
                OnPropertyChanged();
            }
        }
        private string _senderUsername;

        public string SenderUsername
        {
            get
            {
                return this._senderUsername;
            }
            set
            {
                if (value == this._senderUsername)
                {
                    return;
                }
                this._senderUsername = value;
                OnPropertyChanged();
            }
        }
        private string _title;

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                if (value == this._title)
                {
                    return;
                }
                this._title = value;
                OnPropertyChanged();
            }
        }
        private string _body;

        public string Body
        {
            get
            {
                return this._body;
            }
            set
            {
                if (value == this._body)
                {
                    return;
                }
                this._body = value;
                OnPropertyChanged();
            }
        }
        private ICommand _replyCommand;

        public ICommand Reply
        {
            get
            {
                if (this._replyCommand == null)
                {
                    this._replyCommand = new DelegateCommand(this.MessageReply);
                }
                return this._replyCommand;
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
        private void MessageReply()
        {
            App.RootFrame.Navigate(typeof(SendMessagePage), this._senderId);
        }
        public MessageViewModel(string messageId)
        {
            this._dbRequester = new ParseDatabaseRequester();
            this.PopulateData(messageId);
        }

        private async void PopulateData(string messageId)
        {
            this.IsLoading = true;
            ParseObject parseMessage = await this._dbRequester.GetMessageById(messageId);
            ParseUser user = await this._dbRequester.GetUserById(parseMessage["senderId"].ToString());
            this.SenderId = user.ObjectId;
            this.SenderUsername = user.Username;
            this.Title = parseMessage["title"].ToString();
            this.Body = parseMessage["body"].ToString();
            this.IsLoading = false;
        }
    }
}
