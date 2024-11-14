using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Qel.Common.DateTimeUtils;

public static class DateUtils
{
    public static DateTime ToDateTime(this string date)
    {
        IFormatProvider formatProvider;
        formatProvider = CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat;
        
        string[] formats = ["yyyy-MM-dd"];
        var isSuccess = DateTime.TryParseExact(date, formats, formatProvider, DateTimeStyles.None, out DateTime result);
        if (isSuccess)
            return result;
        else
            return result;
    }
    public static DateTime ToDate(this string date)
    {
        var result = date.ToDateTime().Date;
        return result;
    }
    
    public static DateTime ToDate(this DateTime date)
    {
        var result = date.Date;
        return result;
    }

    public static bool EqualsDates(this DateTime date1, DateTime date2)
    {
        return date1.ToDate().Equals(date2.ToDate());
    }
    
    public static bool EqualsDates(this string date1, DateTime date2)
    {
        return date1.ToDate().Equals(date2.ToDate());
    }
    
    public static bool EqualsDates(this DateTime date1, string date2)
    {
        return date1.ToDate().Equals(date2.ToDate());
    }
    
    public static bool EqualsDates(this string date1, string date2)
    {
        return date1.ToDate().Equals(date2.ToDate());
    }
}
