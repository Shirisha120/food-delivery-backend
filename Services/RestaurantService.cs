using FoodDeliveryAPI.Data;
using FoodDeliveryAPI.DTOs;
using FoodDeliveryAPI.Interfaces;
using FoodDeliveryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly FoodDeliveryDbContext _context;

        public RestaurantService(FoodDeliveryDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
        {
            return await _context.Restaurants
                .Include(r => r.Products.Where(p => p.IsActive))
                .Where(r => r.IsActive)
                .Select(r => new RestaurantDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    ImageUrl = r.ImageUrl,
                    Menu = r.Products.Select(p => new ProductDto
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        Price = p.Price,
                        Discount = p.Discount,
                        ImageUrl = p.ImageUrl,
                        Category = p.Category.ToString()
                    }).ToList()
                })
                .ToListAsync();
        }
        public async Task<RestaurantDto?> GetRestaurantByIdAsync(int id)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.Products.Where(p => p.IsActive))
                .FirstOrDefaultAsync(r => r.Id == id && r.IsActive);

            if (restaurant == null) return null;

            return new RestaurantDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                ImageUrl = restaurant.ImageUrl,
                Menu = restaurant.Products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Price = p.Price,
                    Discount = p.Discount,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.ToString()
                }).ToList()
            };
        }

        public async Task<RestaurantDto> CreateRestaurantAsync(RestaurantDto restaurantDto)
        {
            var restaurant = new Restaurant
            {
                Name = restaurantDto.Name,
                Description = restaurantDto.Description,
                ImageUrl = restaurantDto.ImageUrl,
                CreatedDate = DateTime.UtcNow
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            restaurantDto.Id = restaurant.Id;
            return restaurantDto;
        }

        public async Task<RestaurantDto?> UpdateRestaurantAsync(int id, RestaurantDto restaurantDto)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return null;

            restaurant.Name = restaurantDto.Name;
            restaurant.Description = restaurantDto.Description;
            restaurant.ImageUrl = restaurantDto.ImageUrl;
            restaurant.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return restaurantDto;
        }

        public async Task<bool> DeleteRestaurantAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return false;

            restaurant.IsActive = false;
            restaurant.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
