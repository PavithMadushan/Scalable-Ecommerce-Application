using Discount.Application.DTOs;
using Discount.Core.Entities;

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
    }
}
