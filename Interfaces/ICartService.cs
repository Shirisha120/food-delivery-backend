using FoodDeliveryAPI.DTOs;

namespace FoodDeliveryAPI.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemDto>> GetCartItemsAsync(string sessionId);
        Task<CartItemDto> AddToCartAsync(AddToCartDto addToCartDto);
        Task<CartItemDto?> UpdateCartItemQuantityAsync(string sessionId, int productId, int quantity);
        Task<bool> RemoveFromCartAsync(string sessionId, int productId);
        Task<bool> ClearCartAsync(string sessionId);
        Task<decimal> GetCartTotalAsync(string sessionId);
    }
}
