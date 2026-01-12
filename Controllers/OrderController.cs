using Media_MS.Data;
using Media_MS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Media_MS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly MediaContext _context;


        public OrderController(MediaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Orders.ToList());
        }

        [HttpPost]
        public IActionResult Add(Order orders)
        {
            _context.Orders.Add(orders);
            int count = _context.SaveChanges();
            if (count > 0)
                return Ok(orders);
            else
                return BadRequest("Order add failed");
        }

        [HttpDelete]
        public IActionResult Delete(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(x => x.OrderId == orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                int count = _context.SaveChanges();
                if (count > 0)
                    return Ok(order);
                else
                    return BadRequest("Order deletion failed");
            }
            return BadRequest("Order not found.");
        }
    }
}
