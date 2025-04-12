namespace mazwiStore.be.Services.Interfaces
{
    public interface IStorageService
    {
        Task<string> SaveAsync(IFormFile file);
    }
}
