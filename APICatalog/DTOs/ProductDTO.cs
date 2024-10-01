using APICatalog.Models;
using APICatalog.Validations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICatalog.DTOs;

public class ProductDTO
{
    public int ProductId { get; set; }
    [Required(ErrorMessage = "Name is required")]
    [StringLength(80)]
    [FirstLetterUpperCase]
    public string? ProductName { get; set; }
    [Required]
    [StringLength(300)]
    public string? ProductDescription { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(1, 100000, ErrorMessage = "Price must be between {1} and {2}")]
    public decimal Price { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }

    public int CategoryId { get; set; }
}

