using API.Models.DTOs;
using API.Models.Entities;

namespace API.Models.Mappers;

public class BreakMapper
{
    public static Break CastDtoToEntity(BreakCDto request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new Break
        {
            Id = Guid.NewGuid().ToString(),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Duration = request.EndDate.Subtract(request.StartDate),
            Reason = request.Reason,
            Status = request.Status
        };
    }

    public static BreakRDto CastEntityToDto(Break @break)
    {
        ArgumentNullException.ThrowIfNull(@break);
        ArgumentNullException.ThrowIfNull(@break.Id);
        ArgumentNullException.ThrowIfNull(@break.UserId);

        return new BreakRDto
        {
            Id = @break.Id,
            UserId = @break.UserId,
            StartDate = @break.StartDate,
            EndDate = @break.EndDate,
            Duration = @break.Duration,
            Reason = @break.Reason,
            Status = @break.Status
        };
    }
}