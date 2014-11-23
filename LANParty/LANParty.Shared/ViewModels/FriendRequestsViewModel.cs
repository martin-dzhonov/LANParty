using LANParty.Common;
using LANParty.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.UI.Popups;

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
        public FriendRequestsViewModel()
        {
            this._requests = new ObservableCollection<FriendshipRequest>();
            this._dbRequester = new ParseDatabaseRequester();
            this.PopulateData();
        }

        private async void PopulateData()
        {
            this.IsLoading = true;
            IEnumerable<ParseObject> asd = await this._dbRequester.GetFriendRequestsForCurrentUser();
            foreach (ParseObject item in asd)
            {
                ParseUser friendCandidate = await ((ParseUser)item["friend"]).FetchAsync();
                Uri url = ((ParseFile)friendCandidate["profilePic"]).Url;
                this._requests.Add(new FriendshipRequest() {ObjectId = item.ObjectId, UserName = friendCandidate.Username, UserImage = url.ToString()});
            }
            this.IsLoading = false;
        }

        public async void DeclineRequest(FriendshipRequest request)
        {
            if (request != null)
            {
                ParseObject parseRequest = await this._dbRequester.GetFriendRequestById(request.ObjectId);
                parseRequest["declined"] = true;
                await parseRequest.SaveAsync();
                this._requests.Remove(request);
            }
        }
        public async void ApproveRequest(FriendshipRequest request)
        {
            if (request != null)
            {
                ParseObject parseRequest = await this._dbRequester.GetFriendRequestById(request.ObjectId);
                parseRequest["approved"] = true;
                parseRequest.SaveAsync();
                this._requests.Remove(request);
            }
        }
    }
}
