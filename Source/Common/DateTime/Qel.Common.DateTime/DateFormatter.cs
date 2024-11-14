using System.Globalization;

namespace Qel.Common.DateTimeUtils;

internal class DateFormatter : ICustomFormatter, IFormatProvider
{
    public string Format(string? format, object? arg, IFormatProvider? formatProvider)
    {
        throw new NotImplementedException();
    }

    public object? GetFormat(Type? formatType)
    {
        if(formatType == typeof(ICustomFormatter))
            return this;
        else
            return null;
    }
}
