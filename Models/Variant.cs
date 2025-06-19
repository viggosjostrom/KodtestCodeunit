namespace KodtestCodeunit.Models;
public class Variant
{
    public string Size { get; set; } = string.Empty;
    public string ColorName { get; set; } = string.Empty;
    public string ColorId { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public string Sku { get; set; } = string.Empty;
}