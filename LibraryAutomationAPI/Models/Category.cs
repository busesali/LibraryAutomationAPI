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
        public string Name { get; set; } 

        public string Description { get; set; } 

        public int? ParentCategoryId { get; set; }

        [ForeignKey("ParentCategoryId")]
        [JsonIgnore] 
        public virtual Category ParentCategory { get; set; }

        // Alt kategoriler listesi
        public virtual ICollection<Category> SubCategories { get; set; }
    }
}
