namespace KodtestCodeunit.Models;

public class PostModelRequest
{
    public string ReferenceId { get; set; } = string.Empty;
    public List<ProductModel> Products { get; set; } = new List<ProductModel>();
}