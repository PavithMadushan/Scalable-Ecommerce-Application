using Catalog.Application.Commands;
using Catalog.Application.DTOs;
using Catalog.Application.Responses;
using Catalog.Core.Entitiess;
using Catalog.Core.Specifications;

namespace Catalog.Application.Mappers
{
    public static class ProductMapper
    {
        public static ProductResponse ToResponse(this Product product)
        {
            if (product == null) return null;
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Summery = product.Summery,
                Description = product.Description,
                ImageFile = product.ImageFile,
                Price = product.Price,
                Brand = product.Brand,
                Type = product.Type,
                CreatedDate = product.CreatedDate
            };

        }
        public static Pagination<ProductResponse> ToResponse(this Pagination<Product> products)
        => new Pagination<ProductResponse>(
            products.PageIndex,
            products.PageSize,
            products.Count,
            products.Data.Select(p => p.ToResponse()).ToList());

        public static IList<ProductResponse> ToResponseList(this IEnumerable<Product> products) =>
            products.Select(p => p.ToResponse()).ToList();

        public static Product ToEntity(this CreateProductCommand command, ProductBrand brand, ProductType type) =>

            new Product
            {
                Name = command.Name,
                Summery = command.Summery,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Brand = brand,
                Type = type,
                Price = command.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

        public static Product ToUpdateEntity(this UpdateProductCommand command, Product existing, ProductBrand brand, ProductType type)
        {
            return new Product
            {
                Id = command.Id,
                Name = command.Name,
                Summery = command.Summery,
                ImageFile = command.ImageFile,
                Brand = brand,
                Type = type,
                Price = command.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

        }

        public static ProductDto ToDto(this ProductResponse product)
        {
            if (product == null) return null;
            return new ProductDto(
                product.Id,
                product.Name,
                product.Summery,
                product.Description,
                product.ImageFile,
                new BrandDto(product.Brand.Id, product.Brand.Name),
                new TypeDto(product.Type.Id, product.Type.Name),
                product.Price,
                DateTimeOffset.UtcNow
            );

        }

        public static UpdateProductCommand ToCommand(this UpdateProductDto dto,string id) 
        { 
            return new UpdateProductCommand
            {
                Id = id,
                Name = dto.Name,
                Summery = dto.Summary,
                Description = dto.Description,
                ImageFile = dto.ImageFile,
                BrandId = dto.BrandId,
                TypeId = dto.TypeId,
                Price = dto.Price
            };

        }
    }
}
