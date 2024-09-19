using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalog.Models;

public class Product
{
    public int ProductId { get; set; }
    [Required(ErrorMessage = "Name is required")]
    [StringLength(80)]
    public string? ProductName { get; set; }
    [Required]
    [StringLength(300)]
    public string? ProductDescription { get; set; }
    [Required]
    [Column(TypeName ="decimal(10,2)")]
    public decimal Price { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    public float Inventory { get; set; }
    public DateTime RegDate { get; set; }
    public int CategoryId { get; set; }
    [JsonIgnore]
    public Category? Category { get; set; }
}
