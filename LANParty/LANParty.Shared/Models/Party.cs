using LANParty.Common;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace LANParty.Models
{
    public class Party : Bindable
    {
        public string ObjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Spots { get; set; }

        public Party()
        {

        }
        public Party(ParseObject obj)
        {
            this.ObjectId = obj.ObjectId;
            this.Title = obj["title"].ToString();
            this.Description = obj["description"].ToString();
        }
    }
}
