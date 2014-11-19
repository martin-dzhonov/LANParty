using LANParty.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Parse;
namespace LANParty.ViewModels
{
    public class ProfileViewModel : Bindable
    {
        private string _username;
        public string Username
        {
            get
            {
                return this._username;
            }
            private set
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
            private set
            {
                if (value == this._profilePic)
                {
                    return;
                }
                this._profilePic = value;
                OnPropertyChanged();
            }
        }

        public ProfileViewModel(ParseUser user)
        {
            this._username = user.Username;
            this._profilePic = (ParseFile)user["profilePic"];
        }
    }
}
