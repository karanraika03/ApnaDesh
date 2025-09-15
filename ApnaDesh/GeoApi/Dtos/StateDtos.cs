using System.ComponentModel.DataAnnotations;

namespace GeoApi.Dtos;

public record StateCreateDto(
    [Required, StringLength(100)] string Name,
    [Required] int CountryId
);

public record StateUpdateDto(
    [Required, StringLength(100)] string Name,
    [Required] int CountryId
);

public record StateReadDto(int Id, string Name, int CountryId);
