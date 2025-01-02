using API.Models.DTOs;
using API.Models.Entities;
using TimeOff = API.Models.Entities.TimeOff;

namespace API.Models.Mappers;

public static class TimeOffMapper
{
    public static TimeOff CastCreateRequestToModel(TimeOffCDto request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new TimeOff
        {
            Id = Guid.NewGuid().ToString(),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Description = request.Description,
            Status = request.Status
        };
    }

    public static DTOs.TimeOffDto CastModelToDto(TimeOff timeOff)
    {
        ArgumentNullException.ThrowIfNull(timeOff);
        ArgumentNullException.ThrowIfNull(timeOff.Id);
        ArgumentNullException.ThrowIfNull(timeOff.UserId);

        return new DTOs.TimeOffDto
        {
            Id = timeOff.Id,
            UserId = timeOff.UserId,
            StartDate = timeOff.StartDate,
            EndDate = timeOff.EndDate,
            Description = timeOff.Description,
            Status = timeOff.Status
        };
    }

    public static TimeOff UpdateModelFromDto(TimeOff entry, DTOs.TimeOffDto dto)
    {
        if (dto.EndDate <= dto.StartDate) throw new ArgumentException("EndDate must be after StartDate.");

        entry.StartDate = dto.StartDate;
        entry.EndDate = dto.EndDate;
        entry.Description = dto.Description;
        entry.Status = dto.Status;

        return entry;
    }
}