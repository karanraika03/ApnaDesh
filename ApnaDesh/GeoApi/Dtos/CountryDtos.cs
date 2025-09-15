using System.ComponentModel.DataAnnotations;

namespace GeoApi.Dtos;

public record CountryCreateDto(
    [Required, StringLength(100)] string Name
);

public record CountryUpdateDto(
    [Required, StringLength(100)] string Name
);

public record CountryReadDto(int Id, string Name);
