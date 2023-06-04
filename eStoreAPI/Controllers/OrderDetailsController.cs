using BusinessObject.Models;
using eStoreAPI.DTO.Requests.OrderDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.OrderDetails;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly PRN231_AS1Context _context;
        private readonly IOrderDetailRepository repo;

        public OrderDetailsController(PRN231_AS1Context context, IOrderDetailRepository repo)
        {
            _context = context;
            this.repo = repo;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
        {
            if (_context.OrderDetails == null)
            {
                return NotFound();
            }
            return repo.GetOrderDetails();
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderByOrderId(int id)
        {
            if (_context.OrderDetails == null)
            {
                return NotFound();
            }
            var order = repo.GetOrderByOrderId(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }



        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(CreateOrderDetailRequest request)
        {
            if (_context.OrderDetails == null)
            {
                return Problem("Entity set 'PRN231_AS1Context.OrderDetails'  is null.");
            }
            var orderDetail = new OrderDetail
            {
                Discount = request.Discount,
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice
            };
            _context.OrderDetails.Add(orderDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderDetailExists(orderDetail.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderDetail", new { id = orderDetail.OrderId }, orderDetail);
        }

        private bool OrderDetailExists(int id)
        {
            return (_context.OrderDetails?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
