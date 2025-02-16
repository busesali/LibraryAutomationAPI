using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAutomationAPI.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        // 📌 Kullanıcı artık CategoryName değil, CategoryId girecek.
        [Required]
        public int CategoryId { get; set; }

        // 📌 Kategori ile ilişki kurduk
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        // 📌 Güncelleme yapan kullanıcı artık string değil, UserId olacak.
        public string LastModifiedBy { get; set; }
    }
}