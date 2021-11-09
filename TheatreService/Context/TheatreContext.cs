using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using TheatreService.Models;

namespace TheatreService.Context
{
    public class TheatreContext
    {
        MongoClient client;
        IMongoDatabase database;
        public TheatreContext(IConfiguration config)
        {
            string server = Environment.GetEnvironmentVariable("Mongo_DB");
            if (server != null)
            {
                client = new MongoClient(server);
                database = client.GetDatabase(Environment.GetEnvironmentVariable("DB_NAME"));
            }
            else
            {
                client = new MongoClient(config.GetConnectionString("MongoDBConnection"));
                database = client.GetDatabase(config.GetSection("MongoDatabase").Value);
            }
        }
        public IMongoCollection<Theatre> Theatres => database.GetCollection<Theatre>("Theatres");
    }
}
