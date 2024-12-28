public class DateRange
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public bool Overlaps(DateRange other)
    {
        return Start < other.End && End > other.Start || Start == other.Start || End == other.End;
    }
}
