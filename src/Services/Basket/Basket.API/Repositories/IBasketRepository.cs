using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBascket(string userName);
        Task<ShoppingCart> UpdateBascket(ShoppingCart basket);
        Task DeleteBascket(string userName);
    }
}
