using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFamilyHome.Models
{
    [Table("Users")] // força o nome da tabela
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public int GroupId { get; set; }
        public Group? Group { get; set; }
    }
}
