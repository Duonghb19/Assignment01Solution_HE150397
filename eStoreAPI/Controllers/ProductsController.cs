using BusinessObject.Models;
using eStoreAPI.DTO.Requests.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Products;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly PRN231_AS1Context _context;
        private readonly IProductRepository repo;

        public ProductsController(PRN231_AS1Context context, IProductRepository repo)
        {
            _context = context;
            this.repo = repo;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return repo.GetProducts().ToList();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = repo.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, UpdateProductRequest request)
        {


            try
            {
                Product product = repo.GetProductById(id);
                product.ProductName = request.ProductName;
                product.UnitPrice = request.UnitPrice;
                product.UnitsInStock = request.UnitsInStock;
                product.CategoryId = request.CategoryId;
                product.Weight = request.Weight;
                repo.UpdateProduct(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(CreateProductRequest request)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'PRN231_AS1Context.Products'  is null.");
            }
            Product product = new Product
            {
                CategoryId = request.CategoryId,
                ProductName = request.ProductName,
                UnitPrice = request.UnitPrice,
                UnitsInStock = request.UnitsInStock,
                Weight = request.Weight
            };
            repo.SaveProduct(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = repo.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            repo.DeleteProduct(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
