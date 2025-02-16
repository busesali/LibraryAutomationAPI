namespace LibraryAutomationAPI.Models
{
    public class BookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }
        public int CategoryId { get; set; }  // Kullanıcı kategori ID'sini girecek.
    }
}
