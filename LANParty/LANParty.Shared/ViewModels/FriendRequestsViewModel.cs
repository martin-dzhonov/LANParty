using LANParty.Common;
using LANParty.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LANParty.ViewModels
{
    public class FriendRequestsViewModel : Bindable
    {
        private ParseDatabaseRequester _dbRequester;
        private ObservableCollection<FriendshipRequest> _requests;

        public ObservableCollection<FriendshipRequest> Requests
        {
            get
            {
                return this._requests;
            }
            set
            {
                if (value == this._requests)
                {
                    return;
                }
                this._requests = value;
                OnPropertyChanged();
            }
        }

        public FriendRequestsViewModel()
        {
            this._requests = new ObservableCollection<FriendshipRequest>();
            this._dbRequester = new ParseDatabaseRequester();
            this.PopulateData();
        }

        private async void PopulateData()
        {
            IEnumerable<ParseObject> asd = await this._dbRequester.GetFriendRequestsForCurrentUser();
            foreach (ParseObject item in asd)
            {
                ParseUser friendCandidate = await ((ParseUser)item["friend"]).FetchAsync();
                Uri url = ((ParseFile)friendCandidate["profilePic"]).Url;
                this._requests.Add(new FriendshipRequest() { UserName = friendCandidate.Username, UserImage = url.ToString()});
            }
        }
    }
}
