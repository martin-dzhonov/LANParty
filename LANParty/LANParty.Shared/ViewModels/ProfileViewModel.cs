using LANParty.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Parse;
using LANParty.Models;
namespace LANParty.ViewModels
{
    public class ProfileViewModel : Bindable
    {
        private string _objectId;
        public string ObjectId
        {
            get
            {
                return this._objectId;
            }
            set
            {
                if (value == this._objectId)
                {
                    return;
                }
                this._objectId = value;
                OnPropertyChanged();
            }
        }

        private string _username;
        public string Username
        {
            get
            {
                return this._username;
            }
            set
            {
                if (value == this._username)
                {
                    return;
                }
                this._username = value;
                OnPropertyChanged();
            }
        }

        private ParseFile _profilePic;
        public ParseFile ProfilePic
        {
            get
            {
                return this._profilePic;
            }
            set
            {
                if (value == this._profilePic)
                {
                    return;
                }
                this._profilePic = value;
                
                OnPropertyChanged();
            }
        }

        private ParseDatabaseRequester _dbRequester;
        public ProfileViewModel(string userId)
        {
            this._dbRequester = new ParseDatabaseRequester();
            this.PopulateData(userId);
        }

        private async void PopulateData(string userId)
        {
            ParseUser user = await this._dbRequester.GetUserById(userId);
            this._objectId = user.ObjectId;
            this._username = user.Username;
            this._profilePic = (ParseFile)user["profilePic"];
        }
    }
}
