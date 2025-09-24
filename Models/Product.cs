using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryAPI.Models
{
    public class Product
    {
        public int Id {  get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(100)]
        public string Description { get; set; } =string.Empty;
       
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public int Discount { get; set; } = 0;

        [StringLength(200)]
        public string ImageUrl { get; set; } = string.Empty ;

        [Required]
        public CategoryType Category { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        // Foreign key
        public int? RestaurantId { get; set; }
        public virtual Restaurant? Restaurant { get; set; }

        // Navigation properties
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    }
    public enum CategoryType
    {
        Restaurant = 1,
        Grocery = 2,
        Bakery = 3,
        Drinks = 4
    }
}
