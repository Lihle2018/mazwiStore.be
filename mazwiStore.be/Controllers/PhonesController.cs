using mazwiStore.be.Models;
using mazwiStore.be.Models.RequestModels;
using mazwiStore.be.Repositories.Interfaces;
using mazwiStore.be.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace mazwiStore.be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly IStorageService _storageService;
        public PhonesController(IPhoneRepository phoneRepository, IStorageService storageService)
        {
            _phoneRepository = phoneRepository;
            _storageService = storageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPhones()
        {
            var result = await _phoneRepository.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoneById(string id)
        {
            var result = await _phoneRepository.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddPhone([FromForm] AddPhoneRequestModel request)
        {
            try
            {
                var files = Request.Form.Files;
                if (files.Count == 0)
                {
                    return BadRequest("No files uploaded.");
                }
                var file = files[0];
                var url = await _storageService.SaveAsync(file);
                var phone = new Phone(request);
                phone.ImageUrl = url;
                var result = await _phoneRepository.AddAsync(phone);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: ERROR: {ex.Message}");
            }
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdatePhone(string id, [FromBody] Phone request)
        {
            var result = await _phoneRepository.UpdateAsync(id, request);
            return Ok(result);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePhone(string id)
        {
            var result = await _phoneRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
