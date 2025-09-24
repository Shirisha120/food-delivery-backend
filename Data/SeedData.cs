using FoodDeliveryAPI.Models;
namespace FoodDeliveryAPI.Data
{
    public  static class SeedData
    {
        public static void Initialize(FoodDeliveryDbContext context)
        {
            if (context.Restaurants.Any()) return;

            // Seed Restaurants
            var restaurants = new[]
            {
                new Restaurant { Name = "Paradise", Description = "Indian & Chinese cuisine", ImageUrl = "assets/images/paradise.jpg" },
                new Restaurant { Name = "Fusion 9", Description = "Italian pizzas & pastas", ImageUrl = "assets/images/fusion9.jpg" },
                new Restaurant { Name = "Mefil", Description = "biryani", ImageUrl = "assets/images/mefil.jpg" },
                new Restaurant { Name = "Bawarchi", Description = "Authentic Hyderabadi biryani", ImageUrl = "assets/images/bawarchi.jpg" },
                new Restaurant { Name = "Barbeque Nation", Description = "Grill & buffet experience", ImageUrl = "assets/images/barbequenation.jpg" },
                new Restaurant { Name = "Chutneys", Description = "South Indian thalis & dosas", ImageUrl = "assets/images/chutneys.jpg" },
                new Restaurant { Name = "Ohri’s", Description = "Multi-cuisine dining & desserts", ImageUrl = "assets/images/ohris.jpg" },
                new Restaurant { Name = "Minerva Coffee Shop", Description = "Veg South Indian tiffins", ImageUrl = "assets/images/minerva.jpg" },
                new Restaurant { Name = "Mandarin Chef", Description = "Chinese & Asian specials", ImageUrl = "assets/images/mandarinchef.jpg" },
                new Restaurant { Name = "Little Italy", Description = "Authentic Italian vegetarian", ImageUrl = "assets/images/littleitaly.jpg" },
                new Restaurant { Name = "Sahib Sindh Sultan", Description = "Royal Indian fine dining", ImageUrl = "assets/images/sahibsindhsultan.jpg" },
                new Restaurant { Name = "Tatva", Description = "Vegetarian global fusion food", ImageUrl = "assets/images/tatva.jpg" },
                new Restaurant { Name = "Pista House", Description = "Famous haleem & kebabs", ImageUrl = "assets/images/pistahouse.jpg" }

            };
            context.Restaurants.AddRange(restaurants);
            context.SaveChanges();

            // Seed Products
            var products = new[]
            {
                // Restaurant items
                new Product { Title = "Chicken Biryani", Description = "Aromatic basmati rice with chicken", Price = 100, Discount = 10, ImageUrl = "assets/images/chicken-biryani.jpg", Category = CategoryType.Restaurant, RestaurantId = 1 },
                new Product { Title = "Paneer Butter Masala", Description = "Creamy paneer curry", Price = 120, Discount = 5, ImageUrl = "assets/images/paneer-butter-masala.jpg", Category = CategoryType.Restaurant, RestaurantId = 1 },
                new Product { Title = "Fried Rice", Description = "Chinese style fried rice", Price = 80, Discount = 0, ImageUrl = "assets/images/fride-rice.jpg", Category = CategoryType.Restaurant, RestaurantId = 1 },
                new Product { Title = "Margherita Pizza", Description = "Classic Italian pizza", Price = 150, Discount = 15, ImageUrl = "assets/images/Margherita-Pizza.jpg", Category = CategoryType.Restaurant, RestaurantId = 2 },
                new Product { Title = "Pepperoni Pizza", Description = "Pizza with pepperoni", Price = 180, Discount = 10, ImageUrl = "assets/images/Pepperoni-Pizza.jpg.jpg", Category = CategoryType.Restaurant, RestaurantId = 2 },
                new Product { Title = "Pasta Alfredo", Description = "Creamy pasta dish", Price = 130, Discount = 5, ImageUrl = "assets/images/Pasta-Alfredo.jpg", Category = CategoryType.Restaurant, RestaurantId = 2 },
                new Product { Title = "Cheese Burger", Description = "Juicy cheese burger", Price = 90, Discount = 0, ImageUrl = "assets/images/Cheese-burger.jpg", Category = CategoryType.Restaurant, RestaurantId = 3 },
                new Product { Title = "Veggie Burger", Description = "Healthy veggie burger", Price = 85, Discount = 0, ImageUrl = "assets/images/Veggie-Burger.jpg", Category = CategoryType.Restaurant, RestaurantId = 3 },
                new Product { Title = "French Fries", Description = "Crispy golden fries", Price = 50, Discount = 0, ImageUrl = "assets/images/French-Fries.jpg", Category = CategoryType.Restaurant, RestaurantId = 3 },
                
                // Groceries
                new Product { Title = "Fresh Apples", Description = "Crisp and juicy apples", Price = 3.99m, Discount = 0, ImageUrl = "assets/images/apple.jpg", Category = CategoryType.Grocery },
                new Product { Title = "Bananas", Description = "Ripe yellow bananas", Price = 1.99m, Discount = 0, ImageUrl = "assets/images/banana.jpg", Category = CategoryType.Grocery },
                new Product { Title = "Carrots", Description = "Organic orange carrots", Price = 2.49m, Discount = 0, ImageUrl = "assets/images/carrot.jpg", Category = CategoryType.Grocery },
                new Product { Title = "Tomatoes", Description = "Fresh red tomatoes", Price = 2.99m, Discount = 0, ImageUrl = "assets/images/tomatoes.jpg", Category = CategoryType.Grocery },
                new Product { Title = "Milk", Description = "1L Full cream milk", Price = 1.49m, Discount = 0, ImageUrl = "assets/images/milk.jpg", Category = CategoryType.Grocery },
                new Product { Title = "Bread", Description = "Whole wheat bread loaf", Price = 2.19m, Discount = 0, ImageUrl = "assets/images/bread.jpg", Category = CategoryType.Grocery },
                
                // Bakery
                new Product { Title = "Chocolate Cake", Description = "Rich and moist", Price = 12.99m, Discount = 0, ImageUrl = "assets/images/cake.jpg", Category = CategoryType.Bakery },
                new Product { Title = "Croissant", Description = "Flaky and buttery", Price = 3.49m, Discount = 0, ImageUrl = "assets/images/croissant.jpg", Category = CategoryType.Bakery },
                new Product { Title = "Baguette", Description = "Freshly baked", Price = 2.99m, Discount = 0, ImageUrl = "assets/images/baguette.jpg", Category = CategoryType.Bakery },
                new Product { Title = "Cupcake", Description = "Vanilla or chocolate", Price = 2.49m, Discount = 0, ImageUrl = "assets/images/cupcake.jpg", Category = CategoryType.Bakery },
                
                // Drinks
                new Product { Title = "Coca-Cola", Description = "Chilled soft drink", Price = 1.99m, Discount = 0, ImageUrl = "assets/images/coke.jpg", Category = CategoryType.Drinks },
                new Product { Title = "Orange Juice", Description = "Freshly squeezed", Price = 2.49m, Discount = 0, ImageUrl = "assets/images/orange-juice.jpg", Category = CategoryType.Drinks },
                new Product { Title = "Green Tea", Description = "Organic and healthy", Price = 3.49m, Discount = 0, ImageUrl = "assets/images/green-tea.jpg", Category = CategoryType.Drinks },
                new Product { Title = "Coffee", Description = "Hot brewed coffee", Price = 2.99m, Discount = 0, ImageUrl = "assets/images/coffee.jpg", Category = CategoryType.Drinks }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
