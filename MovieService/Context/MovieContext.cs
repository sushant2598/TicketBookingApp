using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MovieService.Models;
using System;

namespace MovieService.Context
{
    public class MovieContext
    {
        MongoClient client;
        IMongoDatabase database;
        public MovieContext(IConfiguration configuration)
        {
            string server = Environment.GetEnvironmentVariable("Mongo_DB");
            if (server != null)
            {
                client = new MongoClient(server);
                database = client.GetDatabase(Environment.GetEnvironmentVariable("DB_NAME"));
            }
            else
            {
                client = new MongoClient(configuration.GetConnectionString("MongoDBConnection"));
                database = client.GetDatabase(configuration.GetSection("MongoDatabase").Value);
            }
        }
        public IMongoCollection<Movies> Movies => database.GetCollection<Movies>("Movies");
    }
}
