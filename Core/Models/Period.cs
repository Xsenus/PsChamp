using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Period : Base, IEquatable<Period>
    {
        public Period() { }
        public Period(Session session) : base(session) { }
                
        public int? YearFirst { get; set; }
        public int? YearSecond { get; set; }

        public Link Link { get; set; }

        public void SetLink(Link link)
        {
            Link = link;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Period);
        }

        public bool Equals(Period other)
        {
            return other != null &&
                   YearFirst == other.YearFirst &&
                   YearSecond == other.YearSecond;
        }

        public override int GetHashCode()
        {
            int hashCode = 799056464;
            hashCode = hashCode * -1521134295 + YearFirst.GetHashCode();
            hashCode = hashCode * -1521134295 + YearSecond.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{YearFirst}/{YearSecond}";
        }

        public static bool operator ==(Period left, Period right)
        {
            return EqualityComparer<Period>.Default.Equals(left, right);
        }

        public static bool operator !=(Period left, Period right)
        {
            return !(left == right);
        }
    }
}
