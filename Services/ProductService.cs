using FoodDeliveryAPI.Data;
using FoodDeliveryAPI.DTOs;
using FoodDeliveryAPI.Interfaces;
using FoodDeliveryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly FoodDeliveryDbContext _context;

        public ProductService(FoodDeliveryDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Restaurant)
                .Where(p => p.IsActive)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Price = p.Price,
                    Discount = p.Discount,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.ToString(),
                    RestaurantName = p.Restaurant != null ? p.Restaurant.Name : null
                    //RestaurantName = p.Restaurant != null ? p.Restaurant.Name : string.Empty,
                    
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(CategoryType category)
        {
            return await _context.Products
                .Include(p => p.Restaurant)
                .Where(p => p.IsActive && p.Category == category)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Price = p.Price,
                    Discount = p.Discount,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.ToString(),
                    RestaurantName = p.Restaurant != null ? p.Restaurant.Name : null
                    //RestaurantName = p.Restaurant != null ? p.Restaurant.Name : string.Empty,
                })
                .ToListAsync();
        }
        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Restaurant)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);

            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                Discount = product.Discount,
                ImageUrl = product.ImageUrl,
                Category = product.Category.ToString(),
                RestaurantName = product.Restaurant?.Name
            };
        }
        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Title = productDto.Title,
                Description = productDto.Description,
                Price = productDto.Price,
                Discount = productDto.Discount,
                ImageUrl = productDto.ImageUrl,
                Category = Enum.Parse<CategoryType>(productDto.Category),
                CreatedDate = DateTime.UtcNow
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            productDto.Id = product.Id;
            return productDto;
        }
        public async Task<ProductDto?> UpdateProductAsync(int id, ProductDto productDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            product.Title = productDto.Title;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.Discount = productDto.Discount;
            product.ImageUrl = productDto.ImageUrl;
            product.Category = Enum.Parse<CategoryType>(productDto.Category);
            product.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return productDto;
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            product.IsActive = false;
            product.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
