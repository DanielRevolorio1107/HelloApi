using HelloApi.Models;
using HelloApi.Models.DTOs;
using HelloApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelloApi.Controllers
{ 
        [ApiController]
        [Route ("api/[controller]")]
        public class OrderController(IOrderService service) : ControllerBase
        {
            private readonly IOrderService _service = service;

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var orders = await _service.GetAllOrderDtoAsync();
                return Ok(orders);
            }

            [HttpGet("{number:int}")]
            public async Task<IActionResult> GetById(int number)
            {
                var order = await _service.GetOrderDtoById(number);
                    if (order == null) return NotFound();
                    return Ok(order);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] OrderCreateDto dto)
            {
                var created = await _service.AddOrderDtoAsync(dto);
                return CreatedAtAction(nameof(GetById), new {number = created.Number}, created);
            }

            [HttpPut("{id:int}")]
            public async Task<IActionResult> Update(int id, [FromBody] OrderUpdateDto dto)
            {
                var updated = await _service.UpdateOrderDtoAsync(id, dto);
                if(updated == null) return NotFound();
                return Ok(updated);
            }
            [HttpDelete("{id:int}")]
            public async Task<IActionResult> Delete(int id)

            {
                var delete = await _service.DeleteOrderDtoAsync(id);
                if (delete == null) return NotFound();
                return Ok(delete);
            }
        }
    }

