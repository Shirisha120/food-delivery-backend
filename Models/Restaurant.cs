using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryAPI.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        [StringLength(200)]
        public string ImageUrl { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        //Navigation properties
        public virtual ICollection<Product> Products { get; set;} = new List<Product>();
    }
}
