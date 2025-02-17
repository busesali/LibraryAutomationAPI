using System.ComponentModel.DataAnnotations;

namespace LibraryAutomationAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }  // Şifreyi hash'li olarak tut.

    }
}
