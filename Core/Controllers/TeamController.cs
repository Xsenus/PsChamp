using Core.Models;
using DevExpress.Xpo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Controllers
{
    public static class TeamController
    {
        public async static Task<List<Team>> GetTeamsAsync(Session session)
        {
            return await new XPQuery<Team>(session).ToListAsync();
        }

        public static Team Create(string name)
        {
            return new Team()
            {
                Name = name
            };
        }

        public static Team Create(Session session, string name)
        {
            return new Team(session)
            {
                Name = name
            };
        }

        public static Team Create(Session session, Team team)
        {
            return new Team(session)
            {
                Name = team.Name
            };
        }
    }
}
