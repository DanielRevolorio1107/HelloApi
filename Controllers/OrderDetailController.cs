using HelloApi.Models.DTOs;
using HelloApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelloApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController(IOrderDetailService service) : ControllerBase
    {
        private readonly IOrderDetailService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderdetails = await _service.GetAllOrderDetailDtoAsync();
            return Ok(orderdetails);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var orderdetail = await _service.GetOrderDetailDtoById(id);
            if (orderdetail == null) return NotFound();
            return Ok(orderdetail);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDetailCreateDto dto)
        {
            var created = await _service.AddOrderDetailDtoAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.ItemId }, created);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteOrderDetailDtoAsync(id);
            if (deleted == null) return NotFound();
            return NoContent();
        }
    }
}

