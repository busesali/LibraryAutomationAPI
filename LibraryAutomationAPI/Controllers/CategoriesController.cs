using LibraryAutomationAPI.Data;
using LibraryAutomationAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAutomationAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Tüm Kategorileri Listeleme (Alt Kategorilerle Birlikte)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetCategories()
        {
            var categories = await _context.Categories
                .Include(c => c.SubCategories)  // Alt kategorileri dahil et
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.Description,
                    ParentCategory = c.ParentCategoryId == null ? "Ana Kategori" : _context.Categories.Where(pc => pc.Id == c.ParentCategoryId).Select(pc => pc.Name).FirstOrDefault(),
                    SubCategories = c.SubCategories.Select(sc => new { sc.Id, sc.Name }).ToList()
                })
                .ToListAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetCategory(int id)
        {
            var category = await _context.Categories
                .Include(c => c.SubCategories)
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.Description,
                    ParentCategory = c.ParentCategoryId == null ? "Ana Kategori" : _context.Categories.Where(pc => pc.Id == c.ParentCategoryId).Select(pc => pc.Name).FirstOrDefault(),
                    SubCategories = c.SubCategories.Select(sc => new { sc.Id, sc.Name }).ToList()
                })
                .FirstOrDefaultAsync();

            if (category == null)
                return NotFound("Kategori bulunamadı.");

            return Ok(category);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Category>> AddCategory([FromBody] CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                ParentCategoryId = categoryDto.ParentCategoryId
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound("Kategori bulunamadı.");

            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;
            category.ParentCategoryId = categoryDto.ParentCategoryId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Kategori Silme (Eğer Alt Kategorisi Varsa Silinemez)
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.Include(c => c.SubCategories).FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
                return NotFound("Kategori bulunamadı.");

            if (category.SubCategories.Any())
                return BadRequest("Bu kategori alt kategoriler içerdiği için silinemez.");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}