using mazwiStore.be.Models;
using MongoDB.Driver;

namespace mazwiStore.be.Data.Interfaces
{
    public interface IStoreContext
    {
        IMongoCollection<Phone> Phones { get; }
    }
}
