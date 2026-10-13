using Discount.Application.Commands;
using Discount.Application.DTOs;
using Discount.Core.Entities;
using Discount.Grpc.Protos;

namespace Discount.Application.Mappers
{
    public static class CouponMapper
    {
        public static CouponDto ToDto(this Coupon Coupon)
        {
            return new CouponDto(
                Coupon.Id,
                Coupon.ProductName,
                Coupon.Description,
                Coupon.Amount);

        }

        public static Coupon ToEntity(this CreateDiscountCommand command)
        {
            return new Coupon
            {

                ProductName = command.ProductName,
                Description = command.Description,
                Amount = command.Amount
            };
        }

        public static Coupon ToEntity(this UpdateDiscountCommand command)
        {
            return new Coupon
            {

                ProductName = command.ProductName,
                Description = command.Description,
                Amount = command.Amount
            };
        }

        public static CouponModel ToModel(this CouponDto dto)
        {
            return new CouponModel
            {
                Id = dto.Id,
                ProductName = dto.ProductName,
                Description = dto.Description,
                Amount = dto.Amount

            };
        }
    }
}
