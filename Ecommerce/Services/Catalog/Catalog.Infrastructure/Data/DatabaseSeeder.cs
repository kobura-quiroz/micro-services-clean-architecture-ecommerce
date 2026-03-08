using Catalog.Core.Entities;
using Catalog.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;

public class DatabaseSeeder
{
    public static async Task SeedAsync(IOptions<DatabaseSettings> options)
    {
        var settings = options.Value;
        var client = new MongoClient(settings.ConnectionString);
        var db = client.GetDatabase(settings.DatabaseName);
        var brands = db.GetCollection<ProductBrand>(settings.BrandCollectionName);
        var types = db.GetCollection<ProductType>(settings.TypeCollectionName);
        var products = db.GetCollection<Product>(settings.ProductCollectionName);

        var SeedBasePath = Path.Combine("Data", "SeedData");

        // Seed Brands
        List<ProductBrand> brandList = await brands.Find(_ => true).ToListAsync();
        if (await brands.CountDocumentsAsync(_ => true) == 0)
        {
            var brandData = await File.ReadAllTextAsync(Path.Combine(SeedBasePath, "brands.json"));
            brandList = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
            await brands.InsertManyAsync(brandList);
        }

        // Seed Types
        List<ProductType> typeList = await types.Find(_ => true).ToListAsync();
        if (await types.CountDocumentsAsync(_ => true) == 0)
        {
            var typeData = await File.ReadAllTextAsync(Path.Combine(SeedBasePath, "types.json"));
            typeList = JsonSerializer.Deserialize<List<ProductType>>(typeData);
            await types.InsertManyAsync(typeList);
        }

        // Seed Products
        List<Product> productList = await products.Find(_ => true).ToListAsync();
        if (await products.CountDocumentsAsync(_ => true) == 0)
        {
            var productData = await File.ReadAllTextAsync(Path.Combine(SeedBasePath, "products.json"));
            productList = JsonSerializer.Deserialize<List<Product>>(productData);
            foreach (var product in productList)
            {
                // Reset Id to let Mongo generate one
                product.Id = null;
                // Default Created On date if not set
                if (product.CreatedOn == default)
                {
                    product.CreatedOn = DateTime.UtcNow;
                }
                await products.InsertManyAsync(productList);
            }
        }
    }
}
