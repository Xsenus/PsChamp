using Core.Models;
using DevExpress.Xpo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Controllers
{
    public static class LinkController
    {
        public async static Task<List<Link>> GetLinksAsync(Session session)
        {
            return await new XPQuery<Link>(session).ToListAsync();
        }

        public static Link Create(string url)
        {
            return new Link()
            {
                Url = url
            };
        }

        public static Link Create(Session session, string url)
        {
            return new Link(session)
            {
                Url = url
            };
        }

        public static Link Create(Session session, Link team)
        {
            return new Link(session)
            {
                Url = team.Url
            };
        }
    }
}
