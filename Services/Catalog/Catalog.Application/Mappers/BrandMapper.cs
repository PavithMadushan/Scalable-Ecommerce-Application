using Catalog.Application.Responses;
using Catalog.Core.Entitiess;

namespace Catalog.Application.Mappers
{
    public static class BrandMapper
    {
        public static BrandResponse ToResponse(this ProductBrand brand)
        {
            return new BrandResponse
            {
                Id = brand.Id,
                Name = brand.Name,
            };
        }

        public static IList<BrandResponse> ToResponseList(this IEnumerable<ProductBrand> brand) { 
        
            return brand.Select(b=>b.ToResponse()).ToList();
        
        }
    }
}
