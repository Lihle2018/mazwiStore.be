using mazwiStore.be.Models.RequestModels;
using MongoDB.Bson.Serialization.Attributes;

namespace mazwiStore.be.Models
{
    public class Phone
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public Promotion Promotion { get; set; } = new Promotion();
        public bool IsPromotionActive => Promotion.IsActive;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Phone() { }
        public Phone(AddPhoneRequestModel requestModel)
        {
            Brand = requestModel.Brand;
            Model = requestModel.Model;
            Price = requestModel.Price;
            Description = requestModel.Description;
            Stock = requestModel.Stock;
            Color = requestModel.Color;
        }
    }

    public class Promotion
    {
        public decimal DiscountPercentage { get; set; }
        public string Model { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsActive => DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;

        public decimal GetDiscountedPrice(decimal originalPrice)
        {
            return IsActive
                ? Math.Round(originalPrice * (1 - DiscountPercentage / 100), 2)
                : originalPrice;
        }
    }

}
