namespace API.Services;

public static class ReportService
{
    public static List<DateTime> GetDaysInMonth(int month, int year)
    {
        var days = new List<DateTime>();

        // Get the number of days in the month
        var daysInMonth = DateTime.DaysInMonth(year, month);

        for (var day = 1; day <= daysInMonth; day++)
        {
            days.Add(new DateTime(year, month, day));
        }

        return days;
    }
}