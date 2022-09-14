using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Team : BaseDictionary, IEquatable<Team>
    {
        public Team() { }
        public Team(Session session) : base(session) { }

        public override bool Equals(object obj)
        {
            return Equals(obj as Team);
        }

        public bool Equals(Team other)
        {
            return other != null &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            int hashCode = 890389916;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }

        public static bool operator ==(Team left, Team right)
        {
            return EqualityComparer<Team>.Default.Equals(left, right);
        }

        public static bool operator !=(Team left, Team right)
        {
            return !(left == right);
        }
    }
}
