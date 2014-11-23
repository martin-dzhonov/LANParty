using Parse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LANParty.Models
{
    public class ParseDatabaseRequester
    {

        public async Task<IEnumerable<ParseObject>> GetPartiesByCategory(string category)
        {
            var query = from party in ParseObject.GetQuery("Party")
                        where party.Get<string>("category") == category
                        select party;
            IEnumerable<ParseObject> results = await query.FindAsync();
            return results;
        }

        public async Task<IEnumerable<ParseUser>> GetFriendsFroCurrentUser()
        {
            var query = from party in ParseObject.GetQuery("FriendRequest")
                        where party.Get<string>("recieverId").Equals(ParseUser.CurrentUser.ObjectId)
                        && party.Get<bool>("approved") == true
                        select party;
            IEnumerable<ParseObject> result = await query.FindAsync();
            List<ParseUser> users = new List<ParseUser>();
            foreach (ParseObject item in result)
            {
                ParseUser user = await ((ParseUser)item["friend"]).FetchAsync();
                users.Add(user);
            }
            return users;
        }
        public async Task<ParseUser> GetUserById(string objectId)
        {
            var query2 = ParseUser.Query.WhereEqualTo("objectId", objectId);
            ParseUser result = await query2.FirstOrDefaultAsync();

            return result;
        }
        public async Task<ParseObject> GetPartyById(string objectId)
        {
            var query = from party in ParseObject.GetQuery("Party")
                        where party.Get<string>("objectId").Equals(objectId)
                        select party;
            ParseObject result = await query.FirstOrDefaultAsync();
            return result;
        }
        public async Task<ParseObject> GetFriendRequestById(string objectId)
        {
            var query = from party in ParseObject.GetQuery("FriendRequest")
                        where party.Get<string>("objectId").Equals(objectId) 
                        select party;
            ParseObject result = await query.FirstOrDefaultAsync();
            return result;
        }
        public async Task<ParseObject> GetMessageById(string objectId)
        {
            var query = from party in ParseObject.GetQuery("Message")
                        where party.Get<string>("objectId").Equals(objectId)
                        select party;
            ParseObject result = await query.FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<ParseObject>> GetCreatedPartiesForUser(ParseUser user)
        {
            var query = from party in ParseObject.GetQuery("Party")
                        where party.Get<ParseUser>("host").Equals(user)
                        select party;
            IEnumerable<ParseObject> results = await query.FindAsync();
            return results;
        }
        public async Task<IEnumerable<ParseObject>> GetJoinedPartiesForUser(ParseUser user)
        {
            var query = from party in ParseObject.GetQuery("Application")
                        where party.Get<ParseUser>("guest").Equals(user) && party.Get<bool>("approved") == true
                        select party;
            IEnumerable<ParseObject> results = await query.FindAsync();
            List<ParseObject> results2 = new List<ParseObject>();
            foreach (ParseObject item in results)
            {
                results2.Add(await this.GetPartyById((string)item["partyId"]));
            }
            return results2;
        }
        public async Task<IEnumerable<ParseObject>> GetApplicationsForParty(string partyId)
        {
            var query = from application in ParseObject.GetQuery("Application")
                        where application.Get<string>("partyId").Equals(partyId) && 
                        application.Get<bool>("approved").Equals(false) && 
                        application.Get<bool>("declined").Equals(false)
                        select application;
            IEnumerable<ParseObject> results = await query.FindAsync();
            return results;
        }

        public async Task<IEnumerable<ParseObject>> GetApplicantsForParty(string partyId)
        {
            var query = from application in ParseObject.GetQuery("Application")
                        where application.Get<string>("partyId").Equals(partyId) && application.Get<bool>("declined").Equals(false)
                        select application;
            IEnumerable<ParseObject> results = await query.FindAsync();
            List<ParseUser> results2 = new List<ParseUser>();
            foreach (ParseObject item in results)
            {
                results2.Add((ParseUser)item["host"]);
            }
            return results2;
        }

        public async Task<IEnumerable<ParseObject>> GetApprovedUsersForParty(string partyId)
        {
            var query = from application in ParseObject.GetQuery("Application")
                        where application.Get<string>("partyId").Equals(partyId) && application.Get<bool>("approved") == true
                        select application;
            IEnumerable<ParseObject> results = await query.FindAsync();
            List<ParseUser> results2 = new List<ParseUser>();
            foreach (ParseObject item in results)
            {
                results2.Add(await ((ParseUser)item["guest"]).FetchIfNeededAsync());
            }
            return results2;
        }

        public async Task<IEnumerable<ParseObject>> GetMessagesForCurrentUser()
        {
            var query = from application in ParseObject.GetQuery("Message")
                        where application.Get<string>("recieverId").Equals(ParseUser.CurrentUser.ObjectId)
                        select application;
            IEnumerable<ParseObject> results = await query.FindAsync();
            return results;
        }

        public async Task<ParseObject> GetApplicationById(string objectId)
        {
            var query = from party in ParseObject.GetQuery("Application")
                        where party.Get<string>("objectId").Equals(objectId)
                        select party;
            ParseObject result = await query.FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<ParseObject>> GetFriendRequestsForCurrentUser()
        {
            var query = from party in ParseObject.GetQuery("FriendRequest")
                        where party.Get<string>("recieverId").Equals(ParseUser.CurrentUser.ObjectId) 
                        && party.Get<bool>("approved") != true
                        && party.Get<bool>("declined") != true
                        select party;
            IEnumerable<ParseObject> result = await query.FindAsync();
            return result;
        }
    }
}
