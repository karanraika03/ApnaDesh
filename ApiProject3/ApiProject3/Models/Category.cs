using System.ComponentModel.DataAnnotations;

namespace ApiProject3.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? image { get; set; }
    public DateTime CreatedDate { get; set; }
}

