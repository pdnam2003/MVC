using WebShopMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private List<Category> categories = new List<Category>
        {
            new Category { Id = 1, CategoryName = "Điện thoại" },
            new Category { Id = 2, CategoryName = "Laptop" }
        };
        private List<Product> products = new List<Product>
        {
            new Product { Id = 1, ProductName = "Iphone 8", Price = 8000, ImageUrl = "/images/iphone1.jpg", CategoryId = 1 },
            new Product { Id = 2, ProductName = "Iphone 10", Price = 10000, ImageUrl = "/images/iphone2.png", CategoryId = 1 },
            new Product { Id = 3, ProductName = "Android", Price = 5000, ImageUrl = "/images/android.png", CategoryId = 1 },
            new Product { Id = 4, ProductName = "Sam Sung Galaxy", Price = 7000, ImageUrl = "/images/galaxy.png", CategoryId = 1 },

            new Product { Id = 5, ProductName = "M1", Price = 20000, ImageUrl = "/images/m1.png", CategoryId = 2 },
            new Product { Id = 6, ProductName = "M2", Price = 25000, ImageUrl = "/images/m2.png", CategoryId = 2 },
            new Product { Id = 7, ProductName = "M3", Price = 30000, ImageUrl = "/images/m3.png", CategoryId = 2 },
            new Product { Id = 8, ProductName = "M4", Price = 35000, ImageUrl = "/images/m4.png", CategoryId = 2 }
       };
        public IActionResult Index()
        {
            ViewBag.Categories = categories;
            return View(products);
        }
    }
}
