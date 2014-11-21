using LANParty.Common;
using LANParty.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Streams;

namespace LANParty.ViewModels
{
    public class RegistrationViewModel : Bindable
    {
        private ParseDatabaseRequester _dbRequester;
        private string _userName;
        public string Username
        {
            get
            {
                return this._userName;
            }
            set
            {
                if (value == this._userName)
                {
                    return;
                }
                this._userName = value;
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
            }
            catch (Exception ex)
            {
                string asd = ex.Message;
                int i = 5;
            }
        }
        public RegistrationViewModel()
        {
            this._dbRequester = new ParseDatabaseRequester();
        }
    }
}
