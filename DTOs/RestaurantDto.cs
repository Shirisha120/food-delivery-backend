namespace FoodDeliveryAPI.DTOs
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public List<ProductDto> Menu { get; set; } = new List<ProductDto>();
    }
}
