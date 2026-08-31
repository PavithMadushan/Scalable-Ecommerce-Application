using Catalog.Core.Entitiess;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IMongoCollection<ProductBrand> _brands;    
        public BrandRepository(IOptions<DatabaseSettings> options) { 
          var settings = options.Value;
          var client = new MongoClient(settings.ConnectionString);
          var db = client.GetDatabase(settings.DatabaseName);
          _brands = db.GetCollection<ProductBrand>(settings.BrandCollectionName);
        }
        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await _brands.Find(x => true).ToListAsync();
        }
        public async Task<ProductBrand> GetBrandAsync(string id)
        {
            return await _brands.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
