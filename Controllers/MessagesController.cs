using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_commerc3D.Data;
using E_commerc3D.Models;
using MongoDB.Driver;

namespace E_commerc3D.Controllers
{
    public class MessagesController : Controller
    {
        private MongoClient client = new MongoClient ("mongodb://localhost:27017/");

     

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var database = client.GetDatabase("Messenger");
            var table = database.GetCollection<Message>("Messenger");
            var messages = table.Find(FilterDefinition<Message>.Empty).ToList();

            return View(messages);
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var database = client.GetDatabase("Messenger");
            var table = database.GetCollection<Message>("Messenger");
            var messages = table.Find(c => c.Id == id).FirstOrDefault();
            if(messages == null)
            {
                return NotFound();
            }


            return View(messages);
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
        public async Task<IActionResult> Create([Bind("Id,UserName,Text,When,UserID")] Message message)
        {
            var database = client.GetDatabase("Messenger");
            var table = database.GetCollection<Message>("Messenger");
            message.Id = Guid.NewGuid().ToString();
            table.InsertOne(message);
            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var database = client.GetDatabase("Messenger");
            var table = database.GetCollection<Message>("Messenger");
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
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Text,When,UserID")] Message message)
        {
            if (id != message.Id)
            {
                return NotFound();

            }
            var database = client.GetDatabase("Messenger");
            var table = database.GetCollection<Message>("Messenger");
            table.ReplaceOne(c => c.Id == message.Id ,message);

           
                
                return RedirectToAction("Index");
          
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(string id)
        {

            var database = client.GetDatabase("Messenger");
            var table = database.GetCollection<Message>("Messenger");
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
            var table = database.GetCollection<Message>("Messenger");
            table.DeleteOne(c => c.Id == id);

           
            return RedirectToAction("Index");
        }

        private bool MessageExists(string id)
        {
            return false;
        }
    }
}
