using Basket.Application.Responses;
using MediatR;

namespace Basket.Application.Queries
{
    public record class GetBasketByUserNameQuery(string UserName) : IRequest<ShoppingCartResponse>;
    
}
