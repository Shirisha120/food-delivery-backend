using FoodDeliveryAPI.DTOs;
using FoodDeliveryAPI.Interfaces;
using FoodDeliveryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Products : ControllerBase
    {
        private readonly IProductService _productService;

        public Products(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpGet("groceries")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetGroceries()
        {
            try
            {
                var products = await _productService.GetProductsByCategoryAsync(CategoryType.Grocery);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpGet("bakery")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetBakeryItems()
        {
            try
            {
                var products = await _productService.GetProductsByCategoryAsync(CategoryType.Bakery);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpGet("drinks")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetDrinks()
        {
            try
            {
                var products = await _productService.GetProductsByCategoryAsync(CategoryType.Drinks);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                    return NotFound(new { message = $"Product with id {id} not found" });

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}
