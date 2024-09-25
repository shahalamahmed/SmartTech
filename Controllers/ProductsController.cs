using Microsoft.AspNetCore.Mvc;
using SmartTech.Data;
using SmartTech.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using System.Threading.Channels;

namespace SmartTech.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string NameFilter)
        {
            var products = from e in _db.Products
                           select e;

            if (!string.IsNullOrEmpty(NameFilter))
            {
                products = products.Where(e => e.ProductName.Contains(NameFilter));
            }

            // Get the filtered products list and pass it to the view
            List<Product> objProductList = products.ToList();

            // Store the NameFilter value to pass it back to the view for display
            ViewData["NameFilter"] = NameFilter;

            return View(objProductList);
        }

        // Your 'Create' action remains the same
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // Upload Image //
        [HttpPost]
        [Route("api/upload/photo")]
        public async Task<IActionResult> UploadImage(IFormFile croppedImage)
        {
            if (croppedImage != null && croppedImage.Length > 0)
            {
                // Save the image to the server (e.g., in a directory on the server)
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var fileName = Path.GetRandomFileName() + Path.GetExtension(croppedImage.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await croppedImage.CopyToAsync(stream);
                }

                return Ok(new { status = "success", filePath = "/uploads/" + fileName });
            }

            return BadRequest(new { status = "fail", message = "Image upload failed." });
        }
        // Edit existing employee - GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Retrieve the product from the database by id
            Product productFromDb = _db.Products.Find(id);

            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb); // Pass the product data to the View
        }

        // Edit existing employee - POST
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Update(product); // Update the product details
                _db.SaveChanges(); // Save changes to the database
                TempData["success"] = "Product updated successfully."; // Success message
                return RedirectToAction("Index"); // Redirect to Index action after update
            }

            return View(product); // If model state is invalid, return the view with the product data
        }
        // Delete employee - GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product productFromDb = _db.Products.Find(id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        // Delete employee - POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product Product = _db.Products.Find(id);
            if (Product == null)
            {
                return NotFound();
            }
            _db.Products.Remove(Product);
            _db.SaveChanges();
            TempData["success"] = "Product deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}

