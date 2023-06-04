using BusinessObject.Models;
using eStoreAPI.DTO.Requests.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Orders;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly PRN231_AS1Context _context;
        private readonly IOrderRepository repo;

        public OrdersController(PRN231_AS1Context context, IOrderRepository repo)
        {
            _context = context;
            this.repo = repo;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            return repo.GetOrders();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = repo.GetOrderById(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, UpdateOrderRequest request)
        {
            try
            {
                var order = repo.GetOrderById(id);
                order.MemberId = request.MemberId;
                order.RequireDate = request.RequireDate;
                order.OrderDate = request.OrderDate;
                order.ShippedDate = request.ShippedDate;
                order.Freight = request.Freight;
                repo.UpdateOrder(order);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(CreateOrderRequest request)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'PRN231_AS1Context.Orders'  is null.");
            }
            var order = new Order
            {
                MemberId = request.MemberId,
                OrderDate = request.OrderDate,
                Freight = request.Freight,
                ShippedDate = request.ShippedDate,
                RequireDate = request.RequireDate
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = repo.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            repo.DeleteOrder(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
