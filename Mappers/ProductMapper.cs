namespace KodtestCodeunit.Mappers;
using KodtestCodeunit.Models;
using KodtestCodeunit.Services;
using System.Collections.Generic;

public class ProductMapper
{
    public PostModelRequest MapToPostModel(GetDataResponse getDataResponse)
    {
        if (getDataResponse == null || getDataResponse.Products == null || !getDataResponse.Products.Any())
            throw new ArgumentException("No product data to map.");

        var postModelRequest = new PostModelRequest
        {
            ReferenceId = getDataResponse.ReferenceId,
            Products = new List<ProductModel>()
        };

        var groupedProducts = getDataResponse.Products
            .GroupBy(p => p.Code)
            .ToList();

        foreach (var group in groupedProducts)
        {
            var firstProduct = group.First();

            var productModel = new ProductModel
            {
                ProductId = firstProduct.Code,
                ItemWeight = firstProduct.Weight,
                ProductDescription = firstProduct.Description,
                Variants = new List<Variant>()
            };

            foreach (var item in group)
            {
                if (string.IsNullOrWhiteSpace(item.Code) ||
                    string.IsNullOrWhiteSpace(item.ColorId) ||
                    string.IsNullOrWhiteSpace(item.Size))
                {
                    throw new InvalidOperationException($"Invalid product data: Code, ColorId, or Size is missing for one of the products. Product: {item.Code}, ColorId: {item.ColorId}, Size: {item.Size}");
                }

                var sku = $"{item.Code}-{item.ColorId}-{item.Size}";
                if (sku.Contains("--") || sku.StartsWith("-") || sku.EndsWith("-"))
                {
                    throw new InvalidOperationException($"Invalid SKU generated: {sku}");
                }

                var variant = new Variant
                {
                    Size = item.Size,
                    ColorName = item.Color,
                    ColorId = item.ColorId,
                    Price = item.UnitPrice,
                    Cost = item.UnitCost,
                    Sku = sku
                };

                productModel.Variants.Add(variant);
            }

            postModelRequest.Products.Add(productModel);
        }

        return postModelRequest;
    }
}