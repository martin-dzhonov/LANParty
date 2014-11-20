﻿using System;
using System.Collections.Generic;
using System.Text;
using Parse;
namespace LANParty.Models
{
    public class UserProfile
    {
        public string Username { get; set; }

        public ParseFile ProfilePic { get; set; }

        public UserProfile(ParseUser user)
        {
            this.Username = user.Username;
            this.ProfilePic = (ParseFile)user["profilePic"];
        }
    }
}
