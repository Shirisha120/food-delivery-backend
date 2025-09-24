using FoodDeliveryAPI.DTOs;
using FoodDeliveryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Restaurants : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public Restaurants(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants()
        {
            try
            {
                var restaurants = await _restaurantService.GetAllRestaurantsAsync();
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto>> GetRestaurant(int id)
        {
            try
            {
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
                if (restaurant == null)
                    return NotFound(new { message = $"Restaurant with id {id} not found" });

                return Ok(restaurant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> CreateRestaurant([FromBody] RestaurantDto restaurantDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdRestaurant = await _restaurantService.CreateRestaurantAsync(restaurantDto);
                return CreatedAtAction(nameof(GetRestaurant), new { id = createdRestaurant.Id }, createdRestaurant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<RestaurantDto>> UpdateRestaurant(int id, [FromBody] RestaurantDto restaurantDto)
        {
            try
            {
                var updatedRestaurant = await _restaurantService.UpdateRestaurantAsync(id, restaurantDto);
                if (updatedRestaurant == null)
                    return NotFound(new { message = $"Restaurant with id {id} not found" });

                return Ok(updatedRestaurant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            try
            {
                var result = await _restaurantService.DeleteRestaurantAsync(id);
                if (!result)
                    return NotFound(new { message = $"Restaurant with id {id} not found" });

                return Ok(new { message = "Restaurant deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}
