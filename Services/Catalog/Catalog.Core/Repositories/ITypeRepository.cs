using Catalog.Core.Entitiess;

namespace Catalog.Core.Repositories
{
    public interface ITypeRepository
    {
        Task<IEnumerable<ProductType>> GetAllTypes();
        Task<ProductType> GetByIdAsync(string id);
    }
}
