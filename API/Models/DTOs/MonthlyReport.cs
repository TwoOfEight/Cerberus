namespace API.Models.DTOs;

public class MonthlyReport
{
   public List<ShiftDto>? Shifts { get; set; }
   public List<TimeOffDto>? TimeOffs { get; set; }
}