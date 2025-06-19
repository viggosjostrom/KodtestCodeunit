namespace KodtestCodeunit.Models;

public class ProductModel
{
    public string ProductId { get; set; } = string.Empty;
    public double ItemWeight { get; set; }
    public List<Variant> Variants { get; set; } = new List<Variant>();
    public string ProductDescription { get; set; } = string.Empty;
}