using mazwiStore.be.Data.Interfaces;
using mazwiStore.be.Models;
using mazwiStore.be.Models.ResponseModels;
using mazwiStore.be.Repositories.Interfaces;
using MongoDB.Driver;

namespace mazwiStore.be.Repositories
{
    public class PhoneRepository(IStoreContext _context) : IPhoneRepository
    {
        public async Task<ResponseBase<Phone>> AddAsync(Phone phone)
        {
            try
            {
                await _context.Phones.InsertOneAsync(phone);
                return ResponseBase<Phone>.SuccessResponse(phone, "Phone added successfully.");
            }
            catch (Exception ex)
            {
             return ResponseBase<Phone>.FailureResponse($"Something went wrong. EXCEPTION: {ex.Message}");
            }
        }

        public async Task<ResponseBase<Phone>> AddPromotion(string id, Promotion promotion)
        {
            var filter = Builders<Phone>.Filter.Eq(p => p.Id, id);
            var update = Builders<Phone>.Update.Set(p => p.Promotion, promotion)
                                               .Set(p => p.UpdatedAt, DateTime.UtcNow);

            var result = await _context.Phones.UpdateOneAsync(filter, update);
            if (result.ModifiedCount == 0)
            {
                return ResponseBase<Phone>.FailureResponse("Phone not found or no changes made.");
            }
            return ResponseBase<Phone>.SuccessResponse(null, "Promotion added successfully.");
        }

        public async Task<long> DeleteAsync(string id)
        {
            var filter = Builders<Phone>.Filter.Eq(p => p.Id, id);
            var result = await _context.Phones.DeleteOneAsync(filter);
            return result.DeletedCount ;
        }

        public async Task<ResponseBase<IEnumerable<Phone>>> GetAllAsync()
        {
            try
            {
                var phones = await _context.Phones.Find(x => true).ToListAsync();
                return ResponseBase<IEnumerable<Phone>>.SuccessResponse(phones, "Phones retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseBase<IEnumerable<Phone>>.FailureResponse($"Something went wrong. EXCEPTION: {ex.Message}");
            }
        }

        public async Task<ResponseBase<Phone>> GetByIdAsync(string id)
        {
            try
            {
                var phone = await _context.Phones.Find(p => p.Id == id).FirstOrDefaultAsync();
                if (phone == null)
                {
                    return ResponseBase<Phone>.FailureResponse("Phone not found.");
                }
                return ResponseBase<Phone>.SuccessResponse(phone, "Phone retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ResponseBase<Phone>.FailureResponse($"Something went wrong. EXCEPTION: {ex.Message}");
            }
        }

        public async Task<ResponseBase<Phone>> UpdateAsync(string id, Phone phone)
        {
            try
            {
                var filter = Builders<Phone>.Filter.Eq(p => p.Id, id);
                var update = Builders<Phone>.Update
                    .Set(p => p.Model, phone.Model)
                    .Set(p => p.Brand, phone.Brand)
                    .Set(p => p.Stock, phone.Stock)
                    .Set(p => p.Color, phone.Color)
                    .Set(p => p.Promotion, phone.Promotion)
                    .Set(p => p.Description, phone.Description)
                    .Set(p => p.Price, phone.Price)
                    .Set(p => p.UpdatedAt, DateTime.UtcNow);
                var result = await _context.Phones.UpdateOneAsync(filter, update);

                if (result.ModifiedCount == 0)
                {
                    return ResponseBase<Phone>.FailureResponse("Phone not found or no changes made.");
                }
                return ResponseBase<Phone>.SuccessResponse(phone, "Phone updated successfully.");
            }
            catch (Exception ex)
            {
                return ResponseBase<Phone>.FailureResponse($"Something went wrong. EXCEPTION: {ex.Message}");
            }
        }
    }
}
