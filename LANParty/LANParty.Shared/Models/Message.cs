using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace LANParty.Models
{
   public class Message
    {
        public string ObjectId { get; set; }
        public string SenderId { get; set; }
        public string RevieverId { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }

        public Message(ParseObject obj)
        {
            this.ObjectId = obj.ObjectId;
            this.SenderId = obj["senderId"].ToString();
            this.RevieverId = obj["recieverId"].ToString();
            this.Title = obj["title"].ToString();
            this.Body = obj["body"].ToString();
        }
    }
}
