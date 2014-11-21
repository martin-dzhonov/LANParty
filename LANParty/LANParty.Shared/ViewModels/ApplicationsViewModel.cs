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
        public ApplicationsViewModel(string partyId)
        {
            this._dbRequester = new ParseDatabaseRequester();
            this._users = new ObservableCollection<UserProfile>();
            this._applicationsIds = new ObservableCollection<string>();
            this.PopulateParties(partyId);
        }

        private async void PopulateParties(string partyId)
        {
            IEnumerable<ParseObject> asd = await this._dbRequester.GetApplicationsForParty(partyId);
            foreach (ParseObject obj in asd)
            {
                ParseUser user = await ((ParseUser)obj["guest"]).FetchAsync();
                this._users.Add(new UserProfile((ParseUser)obj["guest"]));
                this._applicationsIds.Add(obj.ObjectId.ToString());
            }
        }

        private int _index;
        public async void RemoveUserAtIndex(int index)
        {
            this._index = index;
            MessageDialog msgDialog = new MessageDialog("Approve user ?");

            //OK Button
            UICommand okBtn = new UICommand("OK");
            okBtn.Invoked = OkBtnClick;
            msgDialog.Commands.Add(okBtn);

            //Cancel Button
            UICommand cancelBtn = new UICommand("Cancel");
            cancelBtn.Invoked = CancelBtnClick;
            msgDialog.Commands.Add(cancelBtn);

            //Show message
            msgDialog.ShowAsync();
        }
        private async void CancelBtnClick(IUICommand command)
        {

        }

        private async void OkBtnClick(IUICommand command)
        {
            ParseObject application = await this._dbRequester.GetApplicationById(this._applicationsIds[_index]);
            application["approved"] = true;

            try
            {
                await application.SaveAsync();
                this.Users.RemoveAt(this._index);
                MessageDialog msgDialog = new MessageDialog("User approved !");
                msgDialog.ShowAsync();

            }
            catch (Exception ex)
            {

            }


        }
    }
}
