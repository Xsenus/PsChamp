using DevExpress.Xpo;
using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Match : Base, IEquatable<Match>
    {
        public Match() { }
        public Match(Session session) : base(session) { }
        
        public int Position { get; set; }
        public string Period { get; set; }
        public string Tour { get; set; }
        public DateTime? Date { get; set; }
        public Team TeamFirst { get; set; }
        public int? ScoreFirst { get; set; }
        public Team TeamSecond { get; set; }
        public int? ScoreSecond { get; set; }
        public string ThisString => ToString();

        public void UpdateScore(int scoreFirst, int scoreSecond)
        {
            ScoreFirst = scoreFirst;
            ScoreSecond = scoreSecond;
        }

        public void UpdatePosition(int position)
        {
            Position = position;
        }

        public void UpdateScore(Match match) 
        {
            ScoreFirst = match.ScoreFirst;
            ScoreSecond = match.ScoreSecond;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Match);
        }

        public bool Equals(Match other)
        {
            return other != null &&
                   Period == other.Period &&
                   Tour == other.Tour &&
                   Date == other.Date &&
                   EqualityComparer<Team>.Default.Equals(TeamFirst, other.TeamFirst) &&
                   EqualityComparer<Team>.Default.Equals(TeamSecond, other.TeamSecond);
        }

        public override int GetHashCode()
        {
            int hashCode = 874541414;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Period);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Tour);
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Team>.Default.GetHashCode(TeamFirst);
            hashCode = hashCode * -1521134295 + EqualityComparer<Team>.Default.GetHashCode(TeamSecond);
            return hashCode;
        }

        public override string ToString()
        {
            var result = "(-:-)";
            if (ScoreFirst is int && ScoreSecond is int)
            {
                result = $"({ScoreFirst}:{ScoreSecond})";
            }
                
            return $"[{Period}] {Tour}. {TeamFirst} - {TeamSecond} {result}";
        }

        public static bool operator ==(Match left, Match right)
        {
            return EqualityComparer<Match>.Default.Equals(left, right);
        }

        public static bool operator !=(Match left, Match right)
        {
            return !(left == right);
        }
    }
}
