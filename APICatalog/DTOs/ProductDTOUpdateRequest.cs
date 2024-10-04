using System.ComponentModel.DataAnnotations;

namespace APICatalog.DTOs;

public class ProductDTOUpdateRequest : IValidatableObject
{
    [Range(1, 9999, ErrorMessage = "Value must be between 1 and 9999")]
    public float Inventory { get; set; }
    
    public DateTime RegDate { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (RegDate.Date < DateTime.Now.Date)
        {
            yield return new ValidationResult("Date must be higher than current date",
                new[] { nameof(this.RegDate) });
        }
    }
}
