using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoApi.Models;

public class City
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [ForeignKey(nameof(State))]
    public int StateId { get; set; }
    public State? State { get; set; }
}
