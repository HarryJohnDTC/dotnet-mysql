using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
    }
}
