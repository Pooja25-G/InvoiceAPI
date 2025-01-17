using InvoiceMangementApi.Interfaces;
using InvoiceMangementApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace InvoiceMangementApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _repository;
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceController(IInvoiceRepository repository, ILogger<InvoiceController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all invoices.");

                // Example of accessing the user's claims
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // e.g., User ID from token
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;  // e.g., User role from token

                _logger.LogInformation("User ID: {UserId}, Role: {Role}", userId, userRole);

                // Check if user is authorized, this could be role-based or other custom claims
                if (userRole != "Admin")
                {
                    return Unauthorized("You do not have permission to access this resource.");
                }

                var invoices = await _repository.GetAllAsync();
                _logger.LogInformation("Fetched {Count} invoices.", invoices.Count());
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all invoices.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching invoice with ID: {Id}.", id);
                var invoice = await _repository.GetByIdAsync(id);

                if (invoice == null)
                {
                    _logger.LogWarning("Invoice with ID: {Id} not found.", id);
                    return NotFound();
                }

                _logger.LogInformation("Fetched invoice with ID: {Id}.", id);
                return Ok(invoice);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching invoice with ID: {Id}.", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceData invoice)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for creating invoice.");
                    return BadRequest(ModelState);
                }

                await _repository.AddAsync(invoice);
                _logger.LogInformation("Created a new invoice with ID: {Id}.", invoice.Id);
                return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, invoice);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new invoice.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] InvoiceData invoice)
        {
            try
            {
                if (id != invoice.Id)
                {
                    _logger.LogWarning("Invoice ID mismatch for update. Route ID: {RouteId}, Invoice ID: {InvoiceId}.", id, invoice.Id);
                    return BadRequest();
                }

                await _repository.UpdateAsync(invoice);
                _logger.LogInformation("Updated invoice with ID: {Id}.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating invoice with ID: {Id}.", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Deleting invoice with ID: {Id}.", id);
                await _repository.DeleteAsync(id);
                _logger.LogInformation("Deleted invoice with ID: {Id}.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting invoice with ID: {Id}.", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
