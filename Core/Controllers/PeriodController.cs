using Core.Models;
using DevExpress.Xpo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Controllers
{
    public static class PeriodController
    {
        public async static Task<Period> GetPeriod(Session session, Link link)
        {
            return await new XPQuery<Period>(session)
                .FirstOrDefaultAsync(f => 
                    f.Link != null 
                    && f.Link.Guid == link.Guid);
        }

        public async static Task<List<Period>> GetPeriods(Session session)
        {
            return await new XPQuery<Period>(session).ToListAsync();
        }

        public static Period Create(int yearFirst, int yearSecond)
        {
            return new Period()
            {
                YearFirst = yearFirst,
                YearSecond = yearSecond
            };
        }

        public static Period Create(Session session, int yearFirst, int yearSecond)
        {
            return new Period(session)
            {
                YearFirst = yearFirst,
                YearSecond = yearSecond
            };
        }

        public static Period Create(Session session, Period period)
        {
            return new Period(session)
            {
                YearFirst = period.YearFirst,
                YearSecond = period.YearSecond
            };
        }
    }
}
