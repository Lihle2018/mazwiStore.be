using mazwiStore.be.Data.Interfaces;
using mazwiStore.be.Models;
using MongoDB.Driver;

namespace mazwiStore.be.Data
{
    public class StoreContext : IStoreContext
    {
        public StoreContext(string connectionString, string databaseName, string phonesCollectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            Phones = database.GetCollection<Phone>(phonesCollectionName);
        }
        public IMongoCollection<Phone> Phones { get; }
    }
}
