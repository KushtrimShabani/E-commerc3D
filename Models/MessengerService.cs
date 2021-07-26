using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerc3D.Data;
using E_commerc3D.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace E_commerc3D.Models
{
    public class MessengerService
    {
        private readonly IMongoCollection<Message> _messenger;
        private readonly DeveloperDatabaseConfiguration _settings;
        public MessengerService(IOptions<DeveloperDatabaseConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _messenger = database.GetCollection<Message>(_settings.CustomerCollectionName);
        }
        public async Task<List<Message>> GetAllAsync()
        {
            return await _messenger.Find(c => true).ToListAsync();
        }
        public async Task<Message> GetByIdAsync(string id)
        {
            return await _messenger.Find<Message>(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Message> CreateAsync(Message customer)
        {
            await _messenger.InsertOneAsync(customer);
            return customer;
        }
        public async Task UpdateAsync(string id, Message customer)
        {
            await _messenger.ReplaceOneAsync(c => c.Id == id, customer);
        }
        public async Task DeleteAsync(string id)
        {
            await _messenger.DeleteOneAsync(c => c.Id == id);
        }
    }
}
