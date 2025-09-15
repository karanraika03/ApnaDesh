using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeoApi.Models;

public class Country
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<State> States { get; set; } = new List<State>();
}
