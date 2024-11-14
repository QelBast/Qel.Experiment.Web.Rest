namespace Qel.Common.DateTimeUtils;

public class AgeUtils
{
    public static int GetAge(DateTime birthDate)
    {
        var age = DateTime.UtcNow.Year - birthDate.Year;
        return age;
    }
}
