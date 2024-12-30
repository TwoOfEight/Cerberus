using API.Models.DTOs;
using API.Models.Entities;

namespace API.Models.Mappers;

public class TimeOffMapper
{
    public static TimeOff CastDtoToEntity(TimeOffCreateDto request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new TimeOff
        {
            Id = Guid.NewGuid().ToString(),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Duration = request.EndDate.Subtract(request.StartDate),
            Reason = request.Reason,
            Status = request.Status
        };
    }

    public static TimeOffReadDto CastEntityToDto(TimeOff timeOff)
    {
        ArgumentNullException.ThrowIfNull(timeOff);
        ArgumentNullException.ThrowIfNull(timeOff.Id);
        ArgumentNullException.ThrowIfNull(timeOff.UserId);

        return new TimeOffReadDto
        {
            Id = timeOff.Id,
            UserId = timeOff.UserId,
            StartDate = timeOff.StartDate,
            EndDate = timeOff.EndDate,
            Duration = timeOff.Duration,
            Reason = timeOff.Reason,
            Status = timeOff.Status
        };
    }
}