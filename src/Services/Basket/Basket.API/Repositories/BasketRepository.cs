using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<ShoppingCart> GetBascket(string userName)
        {
            var bascket = await _redisCache.GetStringAsync(userName);
            if (String.IsNullOrEmpty(bascket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(bascket);
        }

        public async Task<ShoppingCart> UpdateBascket(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBascket(basket.UserName);
        }

        public async Task DeleteBascket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }


    }
}
