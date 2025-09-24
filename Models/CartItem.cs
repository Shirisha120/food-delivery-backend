using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryAPI.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string SessionId { get; set; } = string.Empty;

        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalPrice { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        // Navigation properties
        public virtual Product Product { get; set; } = null!;
    }
}
