using System.ComponentModel.DataAnnotations;

namespace GeoApi.Dtos;

public record CityCreateDto(
    [Required, StringLength(100)] string Name,
    [Required] int StateId
);

public record CityUpdateDto(
    [Required, StringLength(100)] string Name,
    [Required] int StateId
);

public record CityReadDto(int Id, string Name, int StateId);
