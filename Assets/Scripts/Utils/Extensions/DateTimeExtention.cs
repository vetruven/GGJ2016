using System;

public static class DateTimeExtention
{
    private static readonly DateTime unixTimestampStart = new DateTime(1970, 1, 1);

    /// <summary>
    /// Converts the date to int in POSIX format (amount of seconds since 1970-01-01 00:00:01)
    /// </summary>
    public static Int32 GetPosixTimestamp(this DateTime _this)
    {
       return (Int32) (_this.Subtract(unixTimestampStart)).TotalSeconds;
    }
}