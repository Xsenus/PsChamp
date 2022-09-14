using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Link : Base, IEquatable<Link>
    {
        public Link() { }
        public Link(Session session) : base(session) { }

        [Size(2048)]
        public string Url { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Link);
        }

        public bool Equals(Link other)
        {
            return other != null &&
                   Url == other.Url;
        }

        public override int GetHashCode()
        {
            int hashCode = -1169443244;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Url);
            return hashCode;
        }

        public override string ToString()
        {
            return Url;
        }

        public static bool operator ==(Link left, Link right)
        {
            return EqualityComparer<Link>.Default.Equals(left, right);
        }

        public static bool operator !=(Link left, Link right)
        {
            return !(left == right);
        }
    }
}
