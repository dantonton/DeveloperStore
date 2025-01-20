using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DeveloperStore.Infra
{
    public class MongoDbContext
    {


        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration["MongoDB:ConnectionString"];
            var databaseName = configuration["MongoDB:DatabaseName"];

            var client = new MongoClient(connectionString);
            Database = client.GetDatabase(databaseName);
        }

        public IMongoDatabase Database { get; }
    }
}
