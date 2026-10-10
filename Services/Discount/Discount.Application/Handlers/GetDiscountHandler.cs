using Discount.Application.DTOs;
using Discount.Application.Extensions;
using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers
{
    public class GetDiscountHandler : IRequestHandler<GetDiscountQuery, CouponDto>
    {
        private readonly IDiscountRepository _discountRepository;

        public GetDiscountHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        public async Task<CouponDto> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            //Validate the input
            if (string.IsNullOrEmpty(request.productName))
            {
                var validationError = new Dictionary<string, string>()
                {
                    {"ProductName", "ProductName must not be empty" }
                };

                throw GrpcErrorHelper.CreateValidationException(validationError);
            }

            //Fetch from repo
            var coupon = await _discountRepository.GetDiscount(request.productName);
            if (coupon == null)
            {
                //throw new Exception($"Discount for the Product Name = {request.productName} not found. ");
                throw new RpcException(new Status(StatusCode.Internal, $"Could not create discount for product: {request.productName}"));
            }

            //Mapping
            return coupon.ToDto();

        }
    }
}
