using API.Models.DTOs;
using API.Models.Entities;

namespace API.Models.Mappers;

public static class ShiftMapper
{
    public static Shift CastCreateRequestToModel(ShiftCDto request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new Shift
        {
            Id = Guid.NewGuid().ToString(),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Description = request.Description,
        };
    }

    public static ShiftDto CastModelToDto(Shift model)
    {
        ArgumentNullException.ThrowIfNull(model);
        ArgumentNullException.ThrowIfNull(model.Id);
        ArgumentNullException.ThrowIfNull(model.UserId);

        return new ShiftDto
        {
            Id = model.Id,
            UserId = model.UserId,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Description = model.Description,
        };
    }

    public static Shift UpdateModelFromDto(Shift entry, ShiftDto dto)
    {
        if (dto.EndDate <= dto.StartDate) throw new ArgumentException("EndDate must be after StartDate.");

        entry.StartDate = dto.StartDate;
        entry.EndDate = dto.EndDate;
        entry.Description = dto.Description;

        return entry;
    }
}