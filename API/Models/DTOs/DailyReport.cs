namespace API.Models.DTOs;

public class DailyReport
{
    public DateTime Date { get; set; }
    public int TimeOffHours { get; set; }
    public int WorkHours { get; set; }
    public string TimeOffReason { get; set; }
}