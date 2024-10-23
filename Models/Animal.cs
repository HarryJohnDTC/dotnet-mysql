using System.ComponentModel.DataAnnotations;

namespace DotnetMysql.Models
{
    public class Animal
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Nom is required")]
        public string Nom { get; set; }

        public string Couleur { get; set; }

        [Range(0, 10, ErrorMessage = "Pattes must be between 0 and 10")]
        public int Pattes { get; set; }

        // Assurez-vous d'ajouter cette propriété
        [Required(ErrorMessage = "ImageF is required")]
        public string ImageF { get; set; } // Ajoutez la propriété ImageF

        // Constructeur par défaut
        public Animal() { }

        // Constructeur avec paramètres
        public Animal(int id, string type, string nom, string couleur, int pattes, string imageF)
        {
            Id = id;
            Type = type;
            Nom = nom;
            Couleur = couleur;
            Pattes = pattes;
            ImageF = imageF;
        }
    }
}
