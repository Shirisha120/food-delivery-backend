using FoodDeliveryAPI.DTOs;
using FoodDeliveryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Cart : ControllerBase
    {
        private readonly ICartService _cartService;

        public Cart(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{sessionId}")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetCartItems(string sessionId)
        {
            try
            {
                var cartItems = await _cartService.GetCartItemsAsync(sessionId);
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpPost]
        public async Task<ActionResult<CartItemDto>> AddToCart([FromBody] AddToCartDto addToCartDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var cartItem = await _cartService.AddToCartAsync(addToCartDto);
                return Ok(cartItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpPut("{sessionId}/items/{productId}")]
        public async Task<ActionResult<CartItemDto>> UpdateCartItemQuantity(
           string sessionId,
           int productId,
           [FromBody] UpdateQuantityDto updateDto)
        {
            try
            {
                var cartItem = await _cartService.UpdateCartItemQuantityAsync(sessionId, productId, updateDto.Quantity);
                if (cartItem == null)
                    return NotFound(new { message = "Cart item not found" });

                return Ok(cartItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpDelete("{sessionId}/items/{productId}")]
        public async Task<IActionResult> RemoveFromCart(string sessionId, int productId)
        {
            try
            {
                var result = await _cartService.RemoveFromCartAsync(sessionId, productId);
                if (!result)
                    return NotFound(new { message = "Cart item not found" });

                return Ok(new { message = "Item removed from cart" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpDelete("{sessionId}")]
        public async Task<IActionResult> ClearCart(string sessionId)
        {
            try
            {
                await _cartService.ClearCartAsync(sessionId);
                return Ok(new { message = "Cart cleared" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpGet("{sessionId}/total")]
        public async Task<ActionResult<object>> GetCartTotal(string sessionId)
        {
            try
            {
                var total = await _cartService.GetCartTotalAsync(sessionId);
                return Ok(new { total = total });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}
