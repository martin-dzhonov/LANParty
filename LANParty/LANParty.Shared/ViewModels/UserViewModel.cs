using LANParty.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Parse;
using LANParty.Models;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Streams;
using LANParty.Pages;
using Windows.UI.Popups;
namespace LANParty.ViewModels
{
    public class UserViewModel : Bindable
    {
        private ParseDatabaseRequester _dbRequester;

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
        private string _email;
        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                if (value == this._email)
                {
                    return;
                }
                this._email = value;

                OnPropertyChanged();
            }
        }
        private string _password;
        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                if (value == this._password)
                {
                    return;
                }
                this._password = value;
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

        private ICommand _registerCommand;
        public ICommand Register
        {
            get
            {
                if (this._registerCommand == null)
                {
                    this._registerCommand = new DelegateCommand(this.RegisterUser);
                }
                return this._registerCommand;
            }
        }

        private ICommand _loginCommand;
        public ICommand Login
        {
            get
            {
                if (this._loginCommand == null)
                {
                    this._loginCommand = new DelegateCommand(this.LoginUser);
                }
                return this._loginCommand;
            }
        }
        private async void LoginUser()
        {
            try
            {
                await ParseUser.LogInAsync(this.Username, this.Password);
                App.RootFrame.Navigate(typeof(ProfilePage));
            }
            catch (Exception ex)
            {
                MessageDialog msgDialog = new MessageDialog(ex.Message, "Error");
                msgDialog.ShowAsync();
            }
        }
        private async void RegisterUser()
        {
            var user = new ParseUser()
            {
                Username = this.Username,
                Password = this.Password,
                Email = this.Email
            };
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/DefaultProfilePic.jpg"));
            var bytes = new Byte[0];
            using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
            {
                var reader = new DataReader(fileStream.GetInputStreamAt(0));
                bytes = new Byte[fileStream.Size];
                await reader.LoadAsync((uint)fileStream.Size);
                reader.ReadBytes(bytes);
            }
            ParseFile imgFile = new ParseFile("profilePic.jpg", bytes);
            user["profilePic"] = imgFile;
            try
            {
                await user.SignUpAsync();
                MessageDialog msgDialog = new MessageDialog("Registration succesfull");
                this.Username = "";
                this.Password = "";
                this.Email = "";
                msgDialog.ShowAsync();
            }
            catch (Exception ex)
            {
                MessageDialog msgDialog = new MessageDialog(ex.Message, "Error");
                msgDialog.ShowAsync();
            }
        }
        public UserViewModel()
        {
            this._dbRequester = new ParseDatabaseRequester();
        }

        public UserViewModel(string userId)
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
