using System.ComponentModel.DataAnnotations;

namespace mazwiStore.be.Models.RequestModels;

public class AddPhoneRequestModel
{
    [Required]
    public string Brand { get; set; } = string.Empty;
    [Required]
    public string Model { get; set; } = string.Empty;
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public int Stock { get; set; }
    [Required]
    public string Color { get; set; } = string.Empty;
    [Required]
    public IFormFile Image { get; set; } = null!;

}
