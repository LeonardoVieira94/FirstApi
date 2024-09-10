using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace APICatalog.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<Product>? Products { get; set; } = new Collection<Product>(); 
}
