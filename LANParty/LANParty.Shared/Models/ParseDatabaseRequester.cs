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
            var query = from gameScore in ParseObject.GetQuery("Party")
                        where gameScore.Get<string>("category") == category
                        select gameScore;
            IEnumerable<ParseObject> results = await query.FindAsync();
            return results;
        }
    }
}
