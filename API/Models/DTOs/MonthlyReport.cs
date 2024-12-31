namespace API.Models.DTOs;

public class MonthlyReport
{
   public List<Shift>? Shifts { get; set; }
   public List<TimeOff>? TimeOffs { get; set; }
}