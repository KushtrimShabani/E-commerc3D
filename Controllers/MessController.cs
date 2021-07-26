using E_commerc3D.Data;
using E_commerc3D.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerc3D.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class MessController : Controller
    {
        private readonly MessengerService _customerService;
        public MessController(MessengerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _customerService.GetAllAsync());
        }
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Message customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _customerService.CreateAsync(customer);
            return Ok(customer.Id);
        }
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Message customerIn)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            await _customerService.UpdateAsync(id, customerIn);
            return NoContent();
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            await _customerService.DeleteAsync(customer.Id);
            return NoContent();
        }
    }
}
