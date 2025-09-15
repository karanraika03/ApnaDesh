
using System.ComponentModel.DataAnnotations;

namespace ProfileApi.Countries;

public class Country
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Createddate { get; set; }
    public DateTime Updateddate { get; set; }

}

