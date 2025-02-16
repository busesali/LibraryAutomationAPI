namespace LibraryAutomationAPI.Models
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; } // Eğer null ise ana kategori olur
    }
}
