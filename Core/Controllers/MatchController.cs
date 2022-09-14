using Core.Models;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Controllers
{
    public static class MatchController
    {
        public async static Task<List<Match>> GetMatchesAsync(Session session)
        {
            var matches = await new XPQuery<Match>(session)?.ToListAsync();            
            return matches.OrderBy(o => o.Date)?.ThenBy(t => t.Position)?.ToList();
        }

        public static Match Create(string period, string tour, DateTime? date, Team teamFirst,
            int? scoreFirst, Team teamSecond, int? scoreSecond)
        {
            return new Match()
            {
                Period = period,
                Tour = tour,
                Date = date,
                TeamFirst = teamFirst,
                ScoreFirst = scoreFirst,
                TeamSecond = teamSecond,
                ScoreSecond = scoreSecond
            };
        }

        public static Match Create(Session session, string period, string tour, DateTime? date, Team teamFirst,
            int? scoreFirst, Team teamSecond, int? scoreSecond)
        {
            return new Match(session)
            {
                Period = period,
                Tour = tour,
                Date = date,
                TeamFirst = teamFirst,
                ScoreFirst = scoreFirst,
                TeamSecond = teamSecond,
                ScoreSecond = scoreSecond
            };
        }

        public static Match Create(Session session, Match match)
        {
            return new Match(session)
            {
                Period = match.Period,
                Tour = match.Tour,
                Date = match.Date,
                TeamFirst = match.TeamFirst,
                ScoreFirst = match.ScoreFirst,
                TeamSecond = match.TeamSecond,
                ScoreSecond = match.ScoreSecond
            };
        }

        public async static Task<Match> GetAsync(Session session, string period, string tour, DateTime? date,
            Team teamFirst, int? scoreFirst, Team teamSecond, int? scoreSecond) 
        {
            return await new XPQuery<Match>(session)
                .FirstOrDefaultAsync(f => f.Period == period
                    && f.Tour == tour
                    && f.Date == date
                    && f.TeamFirst == teamFirst
                    && f.ScoreFirst == scoreFirst
                    && f.TeamSecond == teamSecond
                    && f.ScoreSecond == scoreSecond);
        }

        public async static Task<Match> GetAsync(Session session, Match match)
        {
            return await new XPQuery<Match>(session).FirstOrDefaultAsync(f => f.Equals(match));
        }

        public async static Task<Match> GetAsync(Session session, Guid guid)
        {
            return await new XPQuery<Match>(session).FirstOrDefaultAsync(f => f.Guid == guid);
        }
    }
}
