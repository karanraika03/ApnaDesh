using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoApi.Models;

public class State
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [ForeignKey(nameof(Country))]
    public int CountryId { get; set; }
    public Country? Country { get; set; }

    public ICollection<City> Cities { get; set; } = new List<City>();
}
