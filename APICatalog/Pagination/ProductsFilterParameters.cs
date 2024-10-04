namespace APICatalog.Pagination;

public class ProductsFilterParameters : QueryStringParameters
{
    public decimal? Price { get; set; }
    public string? PriceCriteria { get; set; } // more, less or equal
}
