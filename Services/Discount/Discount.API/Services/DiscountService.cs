using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Discount.Application.Mappers;
using MediatR;
using Grpc.Core;

namespace Discount.API.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediator;

        public DiscountService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var query = new GetDiscountQuery(request.ProductName);
            var dto = await _mediator.Send(query);
            return dto.ToModel();
        }
    }
}
