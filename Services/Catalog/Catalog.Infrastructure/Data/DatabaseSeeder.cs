using Catalog.Core.Entitiess;
using Catalog.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public class DatabaseSeeder
    {
        public static async Task SeedAsync(IOptions<DatabaseSettings> options)
        {
            var settings = options.Value;
            var client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.DatabaseName);
            var brand = db.GetCollection<ProductBrand>(settings.BrandCollectionName);
            var types = db.GetCollection<ProductType>(settings.TypeCollectionName);
            var products = db.GetCollection<Product>(settings.ProductCollectionName);

            // Resolve path from the executing assembly's base output folder
            var seedBasePath = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData");

            // Seed Brands
            if ((await brand.CountDocumentsAsync(_ => true)) == 0)
            {
                var filePath = Path.Combine(seedBasePath, "brands.json");
                if (File.Exists(filePath))
                {
                    var brandData = await File.ReadAllTextAsync(filePath);
                    var brandList = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    if (brandList != null && brandList.Count > 0)
                    {
                        await brand.InsertManyAsync(brandList);
                    }
                }
            }

            // Seed Types
            if ((await types.CountDocumentsAsync(_ => true)) == 0)
            {
                var filePath = Path.Combine(seedBasePath, "types.json");
                if (File.Exists(filePath))
                {
                    var typeData = await File.ReadAllTextAsync(filePath);
                    var typeList = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                    if (typeList != null && typeList.Count > 0)
                    {
                        await types.InsertManyAsync(typeList);
                    }
                }
            }

            // Seed Products
            if ((await products.CountDocumentsAsync(_ => true)) == 0)
            {
                var filePath = Path.Combine(seedBasePath, "products.json");
                if (File.Exists(filePath))
                {
                    var productData = await File.ReadAllTextAsync(filePath);
                    var productList = JsonSerializer.Deserialize<List<Product>>(productData);

                    if (productList != null && productList.Count > 0)
                    {
                        foreach (var product in productList)
                        {
                            product.Id = null; // Let MongoDB generate new ObjectId
                            if (product.CreatedDate == default)
                            {
                                product.CreatedDate = DateTimeOffset.UtcNow;
                            }
                        }
                        await products.InsertManyAsync(productList);
                    }
                }
            }
        }
    }
}