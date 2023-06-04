using BusinessObject.Models;
using eStoreAPI.DTO.Requests.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Categories;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly PRN231_AS1Context _context;
        private readonly ICategoryRepository repo;

        public CategoriesController(PRN231_AS1Context context, ICategoryRepository repo)
        {
            _context = context;
            this.repo = repo;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            return repo.GetCategories();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = repo.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, UpdateCategoryRequest request)
        {



            try
            {
                Category category = repo.GetCategoryById(id);
                category.CategoryName = request.CategoryName;
                repo.UpdateCategory(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CreateCategoryRequest request)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'PRN231_AS1Context.Categories'  is null.");
            }
            Category category = new Category
            {
                CategoryName = request.CategoryName
            };
            repo.SaveCategory(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = repo.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            repo.DeleteCategory(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
