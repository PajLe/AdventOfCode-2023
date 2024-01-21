namespace task5
{
    public class RangeComparer : IComparer<Range>
    {
        public int Compare(Range x, Range y)
        {
            if (x.Start < y.Start)
            {
                return -1;
            }
            else if (x.Start > y.Start)
            {
                return 1;
            }
            return 0;
        }
    }
}
