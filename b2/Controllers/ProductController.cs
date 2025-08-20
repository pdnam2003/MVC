using b2.Models;
using Microsoft.AspNetCore.Mvc;

namespace b2.Controllers
{
    public class ProductController : Controller
    {
        List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Product A", Price = 100 },
            new Product { Id = 2, Name = "Product B", Price = 200 },
            new Product { Id = 3, Name = "Product C", Price = 300 }
        };
        public IActionResult Index()
        {
            return View(products); // Sử dụng được View() vì đã kế thừa đúng Controller
        }
        public IActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
