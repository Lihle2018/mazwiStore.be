using mazwiStore.be.Models;
using mazwiStore.be.Models.ResponseModels;

namespace mazwiStore.be.Repositories.Interfaces
{
    public interface IPhoneRepository
    {
        Task<ResponseBase<IEnumerable<Phone>>> GetAllAsync();
        Task<ResponseBase<Phone>> GetByIdAsync(string id);
        Task<ResponseBase<Phone>> AddAsync(Phone phone);
        Task<ResponseBase<Phone>> AddPromotion(string id, Promotion promotion);
        Task<ResponseBase<Phone>> UpdateAsync(string id, Phone phone);
        Task<long> DeleteAsync(string id);
    }
}
