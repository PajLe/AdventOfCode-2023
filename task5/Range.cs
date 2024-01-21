using System.Globalization;

namespace task5
{
    public class Range
    {
        public long Start { get; set; }

        public long End { get; set; }

        public long Length 
        { 
            get 
            {
                return End - Start;
            } 
        }

        public Range()
        {

        }

        public Range (long start, long end)
        {
            Start = start;
            End = end;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is Range r)
            {
                return r.Start == Start && r.End == End;
            }
            return base.Equals(obj);
        }

        public override string ToString()
        {
            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";
            return $"{Start.ToString("#,0", nfi)} - {End.ToString("#,0", nfi)}";
        }

        public long MinimalContainedNumber(Range otherRange)
        {
            if (otherRange.End < Start
                || otherRange.Start > End) return -1;

            long min = Math.Max(Start, otherRange.Start);

            return min;
        }

        public bool IsValid()
        {
            if (Start <= End) 
            {
                return true;
            }

            return false;
        }
    }
}
