using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace DotnetMysql.Models
{
    public class User
    {
        public int Id { get; set; }

        [DisplayName("Votre pseudo")]
        [Required(ErrorMessage = "Pseudo obligatoire")]
        public string Pseudo { get; set; }

        [DisplayName("Votre mot de passe")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mot de passe obligatoire")]
        public string Pass { get; set; }

        // Méthode pour hacher le mot de passe
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
