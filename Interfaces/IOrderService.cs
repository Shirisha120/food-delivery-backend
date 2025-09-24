using FoodDeliveryAPI.DTOs;

namespace FoodDeliveryAPI.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto orderDto);
        Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();
        Task<OrderResponseDto?> GetOrderByIdAsync(int id);
        Task<OrderResponseDto?> UpdateOrderStatusAsync(int id, string status);
    }
}
