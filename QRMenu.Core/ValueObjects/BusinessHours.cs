namespace QRMenu.Core.ValueObjects;

public class BusinessHours : IEquatable<BusinessHours>
{
    public DayOfWeek DayOfWeek { get; private set; }
    public TimeSpan OpenTime { get; private set; }
    public TimeSpan CloseTime { get; private set; }
    public bool IsClosed { get; private set; }

    private BusinessHours() { } // EF Core için

    private BusinessHours(DayOfWeek dayOfWeek, TimeSpan openTime, TimeSpan closeTime, bool isClosed = false)
    {
        DayOfWeek = dayOfWeek;
        OpenTime = openTime;
        CloseTime = closeTime;
        IsClosed = isClosed;
    }

    public static BusinessHours Create(DayOfWeek dayOfWeek, TimeSpan openTime, TimeSpan closeTime)
    {
        if (openTime >= closeTime)
            throw new ArgumentException("Open time must be before close time");

        return new BusinessHours(dayOfWeek, openTime, closeTime);
    }

    public static BusinessHours CreateClosed(DayOfWeek dayOfWeek)
    {
        return new BusinessHours(dayOfWeek, TimeSpan.Zero, TimeSpan.Zero, true);
    }

    public bool IsOpenAt(TimeSpan time)
    {
        if (IsClosed) return false;
        return time >= OpenTime && time <= CloseTime;
    }

    public bool Equals(BusinessHours other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return DayOfWeek == other.DayOfWeek &&
               OpenTime.Equals(other.OpenTime) &&
               CloseTime.Equals(other.CloseTime) &&
               IsClosed == other.IsClosed;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((BusinessHours)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(DayOfWeek, OpenTime, CloseTime, IsClosed);
    }

    public static bool operator ==(BusinessHours left, BusinessHours right)
    {
        if (ReferenceEquals(left, null)) return ReferenceEquals(right, null);
        return left.Equals(right);
    }

    public static bool operator !=(BusinessHours left, BusinessHours right)
    {
        return !(left == right);
    }
}