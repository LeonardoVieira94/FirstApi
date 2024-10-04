using APICatalog.Models;
using APICatalog.Validations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICatalog.DTOs;

public class ProductDTOUpdateResponse
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public float Inventory { get; set; }
    public DateTime RegDate { get; set; }
    public int CategoryId { get; set; }
}
