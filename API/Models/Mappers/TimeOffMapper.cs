using API.Models.DTOs;
using API.Models.Entities;

namespace API.Models.Mappers;

public static class TimeOffMapper
{
    public static TimeOffModel CastCreateRequestToModel(TimeOffCreate request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new TimeOffModel
        {
            Id = Guid.NewGuid().ToString(),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Duration = request.EndDate.Subtract(request.StartDate),
            Reason = request.Reason,
            Status = request.Status
        };
    }

    public static TimeOff CastModelToDto(TimeOffModel timeOffModel)
    {
        ArgumentNullException.ThrowIfNull(timeOffModel);
        ArgumentNullException.ThrowIfNull(timeOffModel.Id);
        ArgumentNullException.ThrowIfNull(timeOffModel.UserId);

        return new TimeOff
        {
            Id = timeOffModel.Id,
            UserId = timeOffModel.UserId,
            StartDate = timeOffModel.StartDate,
            EndDate = timeOffModel.EndDate,
            Duration = timeOffModel.Duration,
            Reason = timeOffModel.Reason,
            Status = timeOffModel.Status
        };
    }

    public static TimeOffModel UpdateModelFromDto(TimeOffModel entry, TimeOff dto)
    {
        if (dto.EndDate <= dto.StartDate) throw new ArgumentException("EndDate must be after StartDate.");

        entry.StartDate = dto.StartDate;
        entry.EndDate = dto.EndDate;
        entry.Duration = dto.EndDate.Subtract(dto.StartDate);
        entry.Reason = dto.Reason;
        entry.Status = dto.Status;

        return entry;
    }
}