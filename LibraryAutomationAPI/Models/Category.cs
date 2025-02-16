using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAutomationAPI.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } // Kategori adı

        public string Description { get; set; } // Kategori açıklaması (isteğe bağlı)

        // 📌 Alt Kategori Desteği İçin ParentId ve ParentCategory Eklendi
        public int? ParentCategoryId { get; set; }

        [ForeignKey("ParentCategoryId")]
        [JsonIgnore] // Sonsuz döngüyü önlemek için JSON'a dahil edilmez
        public virtual Category ParentCategory { get; set; }

        // 📌 Alt kategoriler listesi
        public virtual ICollection<Category> SubCategories { get; set; }
    }
}
