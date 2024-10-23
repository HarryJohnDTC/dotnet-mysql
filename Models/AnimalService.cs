using MySql.Data.MySqlClient; // Assurez-vous que cet espace de noms est inclus
using System.Collections.Generic;
using System.Configuration; // Pour la gestion des chaînes de connexion
using DotnetMysql.Models; // Remplacez par l'espace de noms correct pour votre modèle Animal

public class AnimalService
{
    // Utilisation de System.Configuration pour récupérer la chaîne de connexion
    private string cnx = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

    public List<Animal> GetAnimals()
    {
        List<Animal> animals = new List<Animal>();

        try
        {
            using (MySqlConnection connection = new MySqlConnection(cnx))
            {
                connection.Open();
                string query = "SELECT Id, Type, Nom, Couleur, Pattes, ImageF FROM animal";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32("Id");
                    string type = reader.GetString("Type");
                    string nom = reader.GetString("Nom");
                    string couleur = reader.GetString("Couleur");
                    int pattes = reader.GetInt32("Pattes");
                    string? imageF = reader.IsDBNull(reader.GetOrdinal("ImageF")) ? null : reader.GetString("ImageF");

                    Animal animal = new Animal(id, type, nom, couleur, pattes, imageF);
                    animals.Add(animal);
                }
            }
        }
        catch (Exception ex)
        {
            // Gérez l'erreur
            Console.WriteLine(ex.Message);
        }

        return animals;
    }
}
