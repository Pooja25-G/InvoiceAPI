using InvoiceMangementApi.Interfaces;
using InvoiceMangementApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceMangementApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
        {
        private readonly IInvoiceRepository _repository;

        public InvoiceController(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllI() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _repository.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceData invoice)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _repository.AddAsync(invoice);
            return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, invoice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] InvoiceData invoice)
        {
            if (id != invoice.Id) return BadRequest();
            await _repository.UpdateAsync(invoice);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
  }

