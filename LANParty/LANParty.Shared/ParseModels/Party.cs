using System;
using System.Collections.Generic;
using System.Text;
using Parse;

namespace LANParty.ParseModels
{
   public class Party : ParseObject
    {
        [ParseFieldName("title")]
        public string Title
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
        [ParseFieldName("description")]
        public string Descripton
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
   

        [ParseFieldName("HostId")]
        public ParseRelation<ParseUser> Host
        {
            get { return GetRelationProperty<ParseUser>(); }
            set { SetProperty<ParseRelation<ParseUser>>(value); }
        }

        [ParseFieldName("spots")]
        public int Spots
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
    }
}
