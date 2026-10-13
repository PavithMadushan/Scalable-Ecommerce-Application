using Discount.Application.Commands;
using Discount.Application.DTOs;
using Discount.Application.Extensions;
using Discount.Application.Mappers;
using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers
{
    public class UpdateDiscountHandler : IRequestHandler<UpdateDiscountCommand, CouponDto>
    {
        private readonly IDiscountRepository _discountRepository;

        public UpdateDiscountHandler(IDiscountRepository discountRepository)
        {
            _discountRepository= discountRepository;

        }
        public async Task<CouponDto> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            //Validate the input
            var validateionErrors = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(request.ProductName))
                validateionErrors["ProductName"] = "ProductName must not be empty";

            if (string.IsNullOrEmpty(request.Description))
                validateionErrors["Discription"] = "ProductName Description must not be empty";

            if (request.Amount <= 0)
                validateionErrors["Amount"] = "Amount must be greater than zero";

            if (validateionErrors.Any())
                throw GrpcErrorHelper.CreateValidationException(validateionErrors);

            //Convert to Entity
            var coupon = request.ToEntity();

            //Update the Db
            var updated = await _discountRepository.UpdateDiscount(coupon);

            if (!updated)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Could not update discount for product: {request.ProductName}"));
            }

            //Return DTO
            return coupon.ToDto();
        }
    }
}
