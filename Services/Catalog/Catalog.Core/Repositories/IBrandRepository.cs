using Catalog.Core.Entitiess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Repositories
{
    public interface IBrandRepository
    {
        Task<IEnumerable<ProductBrand>> GetAllBrands();
        Task<ProductBrand> GetBrandAsync(string id);
    }
}
