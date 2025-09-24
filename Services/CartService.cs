using FoodDeliveryAPI.Data;
using FoodDeliveryAPI.DTOs;
using FoodDeliveryAPI.Interfaces;
using FoodDeliveryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryAPI.Services
{
    public class CartService : ICartService
    {
        private readonly FoodDeliveryDbContext _context;

        public CartService(FoodDeliveryDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CartItemDto>> GetCartItemsAsync(string sessionId)
        {
            return await _context.CartItems
                .Include(ci => ci.Product)
                .Where(ci => ci.SessionId == sessionId)
                .Select(ci => new CartItemDto
                {
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    ProductTitle = ci.Product.Title,
                    Price = ci.Product.Price,
                    Discount = ci.Product.Discount,
                    Quantity = ci.Quantity,
                    TotalPrice = ci.TotalPrice,
                    ImageUrl = ci.Product.ImageUrl
                })
                .ToListAsync();
        }
        public async Task<CartItemDto> AddToCartAsync(AddToCartDto addToCartDto)
        {
            var product = await _context.Products.FindAsync(addToCartDto.ProductId);
            if (product == null)
                throw new ArgumentException("Product not found");

            var existingCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.SessionId == addToCartDto.SessionId && ci.ProductId == addToCartDto.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += addToCartDto.Quantity;
                existingCartItem.TotalPrice = CalculateTotalPrice(product, existingCartItem.Quantity);
                existingCartItem.UpdatedDate = DateTime.UtcNow;
            }
            else
            {
                existingCartItem = new CartItem
                {
                    SessionId = addToCartDto.SessionId,
                    ProductId = addToCartDto.ProductId,
                    Quantity = addToCartDto.Quantity,
                    TotalPrice = CalculateTotalPrice(product, addToCartDto.Quantity),
                    CreatedDate = DateTime.UtcNow
                };
                _context.CartItems.Add(existingCartItem);
            }

            await _context.SaveChangesAsync();

            // Reload with product info
            await _context.Entry(existingCartItem).Reference(ci => ci.Product).LoadAsync();

            return new CartItemDto
            {
                Id = existingCartItem.Id,
                ProductId = product.Id,
                ProductTitle = product.Title,
                Price = product.Price,
                Discount = product.Discount,
                Quantity = existingCartItem.Quantity,
                TotalPrice = existingCartItem.TotalPrice,
                ImageUrl = product.ImageUrl
            };
        }
        public async Task<CartItemDto?> UpdateCartItemQuantityAsync(string sessionId, int productId, int quantity)
        {
            var cartItem = await _context.CartItems
                .Include(ci => ci.Product)
                .FirstOrDefaultAsync(ci => ci.SessionId == sessionId && ci.ProductId == productId);

            if (cartItem == null) return null;

            if (quantity <= 0)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
                return null;
            }

            cartItem.Quantity = quantity;
            cartItem.TotalPrice = CalculateTotalPrice(cartItem.Product, quantity);
            cartItem.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new CartItemDto
            {
                Id = cartItem.Id,
                ProductId = cartItem.Product.Id,
                ProductTitle = cartItem.Product.Title,
                Price = cartItem.Product.Price,
                Discount = cartItem.Product.Discount,
                Quantity = cartItem.Quantity,
                TotalPrice = cartItem.TotalPrice,
                ImageUrl = cartItem.Product.ImageUrl
            };
        }
        public async Task<bool> RemoveFromCartAsync(string sessionId, int productId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.SessionId == sessionId && ci.ProductId == productId);

            if (cartItem == null) return false;

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCartAsync(string sessionId)
        {
            var cartItems = await _context.CartItems
                .Where(ci => ci.SessionId == sessionId)
                .ToListAsync();

            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<decimal> GetCartTotalAsync(string sessionId)
        {
            return await _context.CartItems
                .Where(ci => ci.SessionId == sessionId)
                .SumAsync(ci => ci.TotalPrice);
        }

        private static decimal CalculateTotalPrice(Product product, int quantity)
        {
            var discountedPrice = product.Price * (1 - (decimal)product.Discount / 100);
            return discountedPrice * quantity;
        }
    }
}
