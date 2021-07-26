using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_commerc3D.Data;
using E_commerc3D.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using MongoDB.Driver;

namespace E_commerc3D.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        private MongoClient client = new MongoClient("mongodb://localhost:27017/");

        public ProductsController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(s => s.Categories);
            return View(await _context.Products.ToListAsync());
        }
        public async Task<IActionResult> IndexCategories()
        {
            var applicationDbContext = _context.Products.Include(s => s.Categories);
            List<Product> listofProduck = await _context.Products.ToListAsync();

            List<Review> listofReviews = new List<Review>();
            listofReviews = await _context.Review.ToListAsync();

            List<Categories> listofCategory = new List<Categories>();
            listofCategory = await _context.Category.ToListAsync();



            ProductCategories productCategories = new ProductCategories();

            productCategories.CategoriesIndexViewModel = listofCategory;
            productCategories.ReviewIndexViewModel = listofReviews;
            productCategories.ProductIndexViewModel = listofProduck;



            return View(productCategories);
        }

        public async Task<IActionResult> DetailsAndReviews (int? id)
        {
            List<Review> listofReviews = new List<Review>();
            listofReviews = await _context.Review.Where(m => m.ProductID == id).ToListAsync();

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(s => s.Categories)
                .FirstOrDefaultAsync(m => m.Id == id);

            ProductDetails productDetails = new ProductDetails();

            productDetails.ProductsDetailsViewModel = product;
            productDetails.ReviewIndexViewModel = listofReviews;
           


            if (product == null)
            {
                return NotFound();
            }

            return View(productDetails);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReview([Bind("Id,Name,Email,Rating,Reviews,ProductID")] ProductDetails reviewdetails)
        {

           


                Review review = new Review
                {
                    Id = reviewdetails.Id,
                    Name = reviewdetails.Name,
                    Email = reviewdetails.Email,
                    Rating = reviewdetails.Rating,
                    Reviews = reviewdetails.Reviews,
                    ProductID = reviewdetails.ProductID,
                };
                _context.Add(review);
                await _context.SaveChangesAsync();
            return RedirectToAction("DetailsAndReviews", new { id = reviewdetails.ProductID });


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCartList([Bind("Id,Quantity,Pid")] ProductDetails cartListcs)
        {
            

            CartListcs cartlist = new CartListcs
            { 
                Quantity = cartListcs.Quantity,
                ProductID = cartListcs.Pid,
            };
            var database = client.GetDatabase("Messenger");
            var table = database.GetCollection<CartListcs>("ListCart");
            cartlist.Id = Guid.NewGuid().ToString();
            table.InsertOne(cartlist);





            return RedirectToAction("DetailsCart", "Cart", new { id = cartlist.Id });

        }



        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(s => s.Categories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Category, "Name", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       // [Authorize(Policy = "CreateProductsPolicy")]
        public async Task<IActionResult> Create([Bind("Id,Name,Price_buy,Price_sell,Quantity,Measure,Active,Image,Photo,CategoryID,CreateBy,CreateData,UpdateBy,UpdateData")] ProductCreateViewModel product)
        {
           
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (product.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "image");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + product.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    product.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Product newProduct = new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price_buy = product.Price_buy,
                    Price_sell = product.Price_sell,
                    Quantity = product.Quantity,
                    Measure = product.Measure,
                    Active = product.Active,
                    Image = product.Image,
                    ImageBallina = uniqueFileName,
                    CategoryID = product.CategoryID,
                    CreateBy = product.CreateBy,
                    CreateData = product.CreateData,
                    UpdateBy = product.UpdateBy,
                    UpdateData = product.UpdateData

                };
                _context.Add(newProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "Id", "Id", product.CategoryID);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "Id", "Id", product.CategoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Policy = "EditProductsPolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price_buy,Price_sell,Quantity,Measure,Active,Image,ImageBallina,CategoryID,CreateBy,CreateData,UpdateBy,UpdateData")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "Id", "Id", product.CategoryID);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeleteProductsPolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
