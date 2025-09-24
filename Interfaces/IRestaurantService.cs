using FoodDeliveryAPI.DTOs;

namespace FoodDeliveryAPI.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync();
        Task<RestaurantDto?> GetRestaurantByIdAsync(int id);
        Task<RestaurantDto> CreateRestaurantAsync(RestaurantDto restaurantDto);
        Task<RestaurantDto?> UpdateRestaurantAsync(int id, RestaurantDto restaurantDto);
        Task<bool> DeleteRestaurantAsync(int id);
    }
}
