using FoodDeliveryAPI.Data;
using FoodDeliveryAPI.DTOs;
using FoodDeliveryAPI.Interfaces;
using FoodDeliveryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly FoodDeliveryDbContext _context;

        public OrderService(FoodDeliveryDbContext context)
        {
            _context = context;
        }
        public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto orderDto)
        {
            var order = new Order
            {
                CustomerName = orderDto.CustomerName,
                CustomerPhone = orderDto.CustomerPhone,
                CustomerAddress = orderDto.CustomerAddress,
                TotalAmount = orderDto.Items.Sum(i => i.UnitPrice * i.Quantity),
                Status = OrderStatus.Pending,
                CreatedDate = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderItems = orderDto.Items.Select(item => new OrderItem
            {
                OrderId = order.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.UnitPrice * item.Quantity,
                CreatedDate = DateTime.UtcNow
            }).ToList();

            _context.OrderItems.AddRange(orderItems);
            await _context.SaveChangesAsync();

            return await GetOrderByIdAsync(order.Id) ?? new OrderResponseDto();
        }
        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.CreatedDate)
                .Select(o => new OrderResponseDto
                {
                    Id = o.Id,
                    CustomerName = o.CustomerName,
                    CustomerPhone = o.CustomerPhone,
                    CustomerAddress = o.CustomerAddress,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status.ToString(),
                    CreatedDate = o.CreatedDate,
                    Items = o.OrderItems.Select(oi => new OrderItemDetailDto
                    {
                        ProductTitle = oi.Product.Title,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice,
                        TotalPrice = oi.TotalPrice
                    }).ToList()
                })
                .ToListAsync();
        }
        public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return null;

            return new OrderResponseDto
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                CustomerPhone = order.CustomerPhone,
                CustomerAddress = order.CustomerAddress,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                CreatedDate = order.CreatedDate,
                Items = order.OrderItems.Select(oi => new OrderItemDetailDto
                {
                    ProductTitle = oi.Product.Title,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    TotalPrice = oi.TotalPrice
                }).ToList()
            };
        }
        public async Task<OrderResponseDto?> UpdateOrderStatusAsync(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return null;

            if (Enum.TryParse<OrderStatus>(status, out var orderStatus))
            {
                order.Status = orderStatus;
                order.UpdatedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return await GetOrderByIdAsync(id);
            }

            return null;
        }

    }
}
