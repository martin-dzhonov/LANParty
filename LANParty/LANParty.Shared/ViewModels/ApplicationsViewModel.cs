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
    class ApplicationsViewModel : Bindable
    {
        private ParseDatabaseRequester _dbRequester;
        private ObservableCollection<UserProfile> _users;
        private ObservableCollection<string> _applicationsIds;
        public ObservableCollection<UserProfile> Users
        {
            get
            {
                return this._users;
            }
            set
            {
                if (value == this._users)
                {
                    return;
                }
                this._users = value;
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
        public ApplicationsViewModel(string partyId)
        {
            this._dbRequester = new ParseDatabaseRequester();
            this._users = new ObservableCollection<UserProfile>();
            this._applicationsIds = new ObservableCollection<string>();
            this.PopulateParties(partyId);
        }

        private async void PopulateParties(string partyId)
        {
            this.IsLoading = true;
            IEnumerable<ParseObject> asd = await this._dbRequester.GetApplicationsForParty(partyId);
            foreach (ParseObject obj in asd)
            {
                ParseUser user = await ((ParseUser)obj["guest"]).FetchAsync();
                this._users.Add(new UserProfile((ParseUser)obj["guest"]));
                this._applicationsIds.Add(obj.ObjectId.ToString());
            }
            this.IsLoading = false;
        }

        private int _index;
        public async void DeclineUserAtIndex(int index)
        {
            this.IsLoading = true;

            ParseObject application = await this._dbRequester.GetApplicationById(this._applicationsIds[_index]);
            application["declined"] = true;
            try
            {
                await application.SaveAsync();
                this._users.RemoveAt(_index);
                this._applicationsIds.RemoveAt(_index);
                this.IsLoading = false;
            }
            catch (Exception ex)
            {
                MessageDialog msgDialog = new MessageDialog(ex.Message);
                this.IsLoading = false;
                msgDialog.ShowAsync();
            }
        }
        public async void ApproveUserAtIndex(int index)
        {
            this._index = index;
            MessageDialog msgDialog = new MessageDialog("Approve user ?");

            UICommand okBtn = new UICommand("OK");
            okBtn.Invoked = OkBtnClick;
            msgDialog.Commands.Add(okBtn);

            UICommand cancelBtn = new UICommand("Cancel");
            cancelBtn.Invoked = CancelBtnClick;
            msgDialog.Commands.Add(cancelBtn);

            await msgDialog.ShowAsync();
        }
        private async void CancelBtnClick(IUICommand command)
        {

        }

        private async void OkBtnClick(IUICommand command)
        {
            this.IsLoading = true;
            ParseObject application = await this._dbRequester.GetApplicationById(this._applicationsIds[_index]);
            ParseObject party = await this._dbRequester.GetPartyById(application["partyId"].ToString());
            long spots = (Int64)party["spots"];
            if (spots <= 0)
            {
                MessageDialog msgDialog = new MessageDialog("No more spots left !");
                this.IsLoading = false;
                msgDialog.ShowAsync();
            }
            else
            {
                party["spots"] = spots - 1;
                application["approved"] = true;

                try
                {
                    await application.SaveAsync();
                    await party.SaveAsync();
                    this.Users.RemoveAt(this._index);
                    this._applicationsIds.RemoveAt(this._index);
                    MessageDialog msgDialog = new MessageDialog("User approved !");
                    this.IsLoading = false;
                    msgDialog.ShowAsync();

                }
                catch (Exception ex)
                {
                    MessageDialog msgDialog = new MessageDialog(ex.Message);
                    this.IsLoading = false;
                    msgDialog.ShowAsync();
                }
            }

        }
    }
}
