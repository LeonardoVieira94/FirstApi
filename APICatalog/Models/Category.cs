using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace APICatalog.Models;

public class Category
{
    public int CategoryId { get; set; }
    [Required]
    [StringLength(80)]
    public string? Name { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    public ICollection<Product>? Products { get; set; } = new Collection<Product>(); 
}
