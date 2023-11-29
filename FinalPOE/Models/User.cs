using System.ComponentModel.DataAnnotations;

namespace FinalPOE.Models
{
    public class User
    {
        internal static object Identity;

        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

    }
}
