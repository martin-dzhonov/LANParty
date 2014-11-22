using LANParty.Common;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace LANParty.Models
{
    public class Party : Bindable
    {
        private string _title;

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                if (value == this._title)
                {
                    return;
                }
                this._title = value;
                OnPropertyChanged();
            }
        }
        private string _description;

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                if (value == this._description)
                {
                    return;
                }
                this._description = value;
                OnPropertyChanged();
            }
        }

        private string _category;

        public string Category
        {
            get
            {
                return this._category;
            }
            set
            {
                if (value == this._category)
                {
                    return;
                }
                this._category = value;
                OnPropertyChanged();
            }
        }

        private string _spots;

        public string Spots
        {
            get
            {
                return this._spots;
            }
            set
            {
                if (value == this._spots)
                {
                    return;
                }
                this._spots = value;
                OnPropertyChanged();
            }
        }

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
        private DateTime _date;

        public DateTime Date
        {
            get
            {
                return this._date;
            }
            set
            {
                if (value == this._date)
                {
                    return;
                }
                this._date = value;
                OnPropertyChanged();
            }
        }

        public Party()
        {

        }
        public Party(ParseObject obj)
        {
            this._objectId = obj.ObjectId;
            this._title = obj["title"].ToString();
            this._description = obj["description"].ToString();
            this._spots = obj["spots"].ToString();
            this._date = (DateTime)obj["date"];
        }
    }
}
