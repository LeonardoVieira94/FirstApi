using APICatalog.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalog.Models;

public class Product //: IValidatableObject
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
    [Column(TypeName ="decimal(10,2)")]
    [Range(1, 100000, ErrorMessage = "Price must be between {1} and {2}")]
    public decimal Price { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    public float Inventory { get; set; }
    public DateTime RegDate { get; set; }
    public int CategoryId { get; set; }
    [JsonIgnore]
    public Category? Category { get; set; }

   /* public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(ProductName))
        {
            var firstLetter = ProductName[0].ToString();

            if (firstLetter != firstLetter.ToUpper())
            {
                yield return new ValidationResult("First letter need to be upper case", new[] { nameof(ProductName) });
            }
        }
    }*/
}
