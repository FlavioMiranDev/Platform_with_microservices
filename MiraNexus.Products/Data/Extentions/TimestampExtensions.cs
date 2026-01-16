using Google.Protobuf.WellKnownTypes;

namespace MiraNexus.Products.Data.Extentions;

public static class TimestampExtensions
{
    public static Timestamp ToTimestamp(this DateTime dateTime)
    {
        return Timestamp.FromDateTime(
            dateTime.Kind == DateTimeKind.Utc
                ? dateTime
                : dateTime.ToUniversalTime()
        );
    }

    public static Timestamp? ToTimestamp(this DateTime? dateTime)
    {
        return dateTime.HasValue ? dateTime.Value.ToTimestamp() : null;
    }
}
