using Ordering.Application.DTOs;
using Ordering.Core.Entities;

namespace Ordering.Application.Mapper
{
    public static class OrderMapper
    {
        public static OrderDto ToDto(this Order order) =>
            new(order.Id, order.UserName!, order.TotalPrice ?? 0, order.FirstName, order.LastName!, 
                order.EmailAddress!, order.AddressLine!, order.Country! , order.State!, order.ZipCode!,
                order.CardName!, order.CardNumber!, order.EmailAddress!, order.Cvv!, order.PaymentMethod ?? 0);
    }
}
