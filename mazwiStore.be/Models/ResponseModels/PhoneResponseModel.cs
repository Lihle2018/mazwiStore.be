namespace mazwiStore.be.Models.ResponseModels
{
    public class PhoneResponseModel
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public bool IsPromotionActive { get; set; }
    }
}
