using E_commerc3D.Data;
using E_commerc3D.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerc3D.Controllers
{
    public class CartController : Controller
    {
        private MongoClient client = new MongoClient("mongodb://localhost:27017/");
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        public CartController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var database = client.GetDatabase("Messenger");
            var table = database.GetCollection<Cart>("ShopingCart");
            var messages = table.Find(FilterDefinition<Cart>.Empty).ToList();

            return View(messages);
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> DetailsCart(string id )
        {
            var applicationDbContext = _context.Products.Include(s => s.Categories);
            List<Product> listofProduct = await _context.Products.ToListAsync();

            var database = client.GetDatabase("Messenger");
            var table = database.GetCollection<CartListcs>("ListCart");
            var messages = table.Find(c => c.Id == id).FirstOrDefault();

            CartListViewModel cartListViewModel = new CartListViewModel();

            cartListViewModel.ProductIndexViewM = listofProduct;
            cartListViewModel.CartListViewM = messages;



            if (messages == null)
            {
                return NotFound();
            }


            return View(cartListViewModel);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,City,Shipping,When,Price,Quantityc,Idc,ProductIDc")] CartListViewModel message)
        {
            CartListcs cartm = new CartListcs
            {
                Id = message.Idc,
              Quantity = message.Quantityc,
                ProductID = message.ProductIDc,
            };


            List<CartListcs> list = new List<CartListcs>();
           list.Add(cartm);
            Cart cartlist = new Cart
            {
                UserName = message.UserName,
                City = message.City,
                When = message.When,
                CartListcsID = list,
                Price = message.Price, 
                Shipping = message.Shipping,
            };

            var database = client.GetDatabase("Messenger");
            var table = database.GetCollection<Cart>("ShopingCart");
            cartlist.Id = Guid.NewGuid().ToString();
            table.InsertOne(cartlist);



            return RedirectToAction("DetailsOrder", new { id = cartlist.Id });
        }

        public async Task<IActionResult> DetailsOrder(string id)
        {
            

            var database = client.GetDatabase("Messenger");
            var table = database.GetCollection<Cart>("ShopingCart");
            var messages = table.Find(c => c.Id == id).FirstOrDefault();

            var applicationDbContext = _context.Products.Include(s => s.Categories);
            List<Product> listofProduct = await _context.Products.ToListAsync();

            OrdersView orderListViewModel = new OrdersView();

            orderListViewModel.CartViewM = messages;
            orderListViewModel.ProductIndex = listofProduct;

            if (messages == null)
            {
                return NotFound();
            }
            return View(orderListViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder([Bind("Id,UserID,ContactName,City,Country,Mobile,Status,Email,AddressShipping,Quantity,AddressBilling,OrderNotes,Address,CartId")] OrdersView message)
        {
            Cart cartm = new Cart();
            var databasee = client.GetDatabase("Messenger");
            var tablee = databasee.GetCollection<Cart>("ShopingCart");
            cartm = tablee.Find(c => c.Id == message.CartId).FirstOrDefault();

            Order cartlist = new Order
            {
                ContactName = message.ContactName,
                City = message.City,
                UserID = message.UserID,
                Country = message.Country,
                Mobile = message.Mobile,
                Status = message.Status,
                Email = message.Email,
                Address = message.Address,
                AddressBilling = message.AddressBilling,
                AddressShipping = message.AddressShipping,
                Quantity = message.Quantity,
                OrderNotes = message.OrderNotes,
                CartVM = cartm,

            };

            var database = client.GetDatabase("Messenger");
            var table = database.GetCollection<Order>("Order");
            cartlist.Id = Guid.NewGuid().ToString();
            table.InsertOne(cartlist);

            return RedirectToAction("Index", "Home");


        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var database = client.GetDatabase("Messenger");
            var table = database.GetCollection<Cart>("ShopingCart");
            var messages = table.Find(c => c.Id == id).FirstOrDefault();
            if (messages == null)
            {
                return NotFound();
            }
            return View();
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Text,When,UserID")] Cart message)
        {
            if (id != message.Id)
            {
                return NotFound();

            }
            var database = client.GetDatabase("Messenger");
            var table = database.GetCollection<Cart>("ShopingCart");
            table.ReplaceOne(c => c.Id == message.Id, message);



            return RedirectToAction("Index");

        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(string id)
        {

            var database = client.GetDatabase("Messenger");
            var table = database.GetCollection<Cart>("ShopingCart");
            var messages = table.Find(c => c.Id == id).FirstOrDefault();
            if (messages == null)
            {
                return NotFound();
            }

            return View(messages);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var database = client.GetDatabase("Messenger");
            var table = database.GetCollection<Cart>("ShopingCart");
            table.DeleteOne(c => c.Id == id);


            return RedirectToAction("Index");
        }

        private bool CartExists(string id)
        {
            return false;
        }
    }
}
