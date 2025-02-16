using LibraryAutomationAPI.Data;
using LibraryAutomationAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LibraryAutomationAPI.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetBooks()
        {
            var books = await _context.Books
                .Include(b => b.Category) // 📌 Kategoriyi de getiriyoruz
                .Select(b => new
                {
                    b.Id,
                    b.Title,
                    b.Author,
                    b.Genre,
                    b.PublishDate,
                    CategoryName = b.Category.Name,  // 📌 Kullanıcı kategori ID yerine adını görecek
                    LastModifiedBy = b.LastModifiedBy // 📌 Giriş yapan kullanıcının adı string olarak dönecek
                })
                .ToListAsync();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.Category)
                .Where(b => b.Id == id)
                .Select(b => new
                {
                    b.Id,
                    b.Title,
                    b.Author,
                    b.Genre,
                    b.PublishDate,
                    CategoryName = b.Category.Name,  // 📌 Kullanıcı kategori ID yerine adını görecek
                    LastModifiedBy = b.LastModifiedBy // 📌 Giriş yapan kullanıcının adı string olarak dönecek
                })
                .FirstOrDefaultAsync();

            if (book == null)
                return NotFound("Kitap bulunamadı.");

            return Ok(book);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Book>> AddBook(BookDto bookDto)
        {
            // 📌 Kullanıcı adını JWT içindeki 'sub' claim'inden alıyoruz
            var usernameClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized($"Geçersiz kullanıcı kimliği. Okunan değer: {usernameClaim?.ToString() ?? "YOK"}");
            }

            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Genre = bookDto.Genre,
                PublishDate = bookDto.PublishDate,
                CategoryId = bookDto.CategoryId,
                LastModifiedBy = username // 📌 Kullanıcının adı otomatik atanacak
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateBook(int id, BookDto bookDto)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound("Kitap bulunamadı.");

            // 📌 Kullanıcı adını JWT içindeki 'sub' claim'inden alıyoruz
            var usernameClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var username = usernameClaim?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized($"Geçersiz kullanıcı kimliği. Okunan değer: {usernameClaim?.ToString() ?? "YOK"}");
            }

            // 📌 Kitap bilgilerini güncelle
            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.Genre = bookDto.Genre;
            book.PublishDate = bookDto.PublishDate;
            book.CategoryId = bookDto.CategoryId;
            book.LastModifiedBy = username; // 📌 Güncelleyen kullanıcı adı otomatik atanacak

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound("Kitap bulunamadı."); // 📌 Eğer kitap yoksa 404 döndür

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent(); // 📌 Başarıyla silindiyse 204 No Content döndür
        }
    }
}
