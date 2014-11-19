using System;
using System.Collections.Generic;
using System.Text;
using Parse;
namespace LANParty.Models
{
    public class UserProfile
    {
        public string Username { get; set; }

        public ParseFile ProfilePic { get; set; }

        public UserProfile()
        {
            ParseUser currentUser = ParseUser.CurrentUser;
            this.Username = currentUser.Username;
            this.ProfilePic = (ParseFile)currentUser["profilePic"];
        }
    }
}
