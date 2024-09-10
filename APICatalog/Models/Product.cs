﻿namespace APICatalog.Models;

public class Product
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public float Inventory { get; set; }
    public DateTime RegDate { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
