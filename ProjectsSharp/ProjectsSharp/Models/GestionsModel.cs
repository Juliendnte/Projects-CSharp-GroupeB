using System.Data;
using System.Text.Json;
using dotenv.net;
using MySql.Data.MySqlClient;
using ProjectsSharp.Service;

namespace ProjectsSharp.Models;

public class GestionsModels
{
    public GestionsModels()
    {
        DotEnv.Load();
    }

    private string GetConnectionString()
    {
        return Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    }

    public bool CreateTache(Tache newTache)
    {
        using (var mySqlCn = new MySqlConnection(GetConnectionString()))
        {
            using (var mySqlCmd =
                   new MySqlCommand(
                       "INSERT INTO tache (Titre, Description, date_debut, date_fin) VALUES (@Titre, @Description, @date_debut, @date_fin)",
                       mySqlCn))
            {
                mySqlCmd.Parameters.AddWithValue("@Titre", newTache.Titre);
                mySqlCmd.Parameters.AddWithValue("@Description", newTache.Description);
                mySqlCmd.Parameters.AddWithValue("@date_debut", newTache.date_debut);
                mySqlCmd.Parameters.AddWithValue("@date_fin", newTache.date_fin);

                mySqlCn.Open();
                var rowsAffected = mySqlCmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }

    public string GetTache()
    {
        var taches = new List<Tache>();

        using (var mySqlCn = new MySqlConnection(GetConnectionString()))
        {
            using (var mySqlCmd = new MySqlCommand("SELECT * FROM tache", mySqlCn))
            {
                mySqlCn.Open();
                using (var mySqlReader = mySqlCmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (mySqlReader.Read())
                    {
                        var tache = new Tache
                        {
                            Id = mySqlReader.GetInt32(0),
                            Titre = mySqlReader.GetString(1),
                            Description = mySqlReader.GetString(2),
                            date_debut = mySqlReader.GetDateTime(3),
                            date_fin = mySqlReader.GetDateTime(4)
                        };
                        taches.Add(tache);
                    }
                }
            }
        }

        return JsonSerializer.Serialize(taches);
    }

    public bool UpdateTache(int id, Tache updatedTache)
    {
        using (var mySqlCn = new MySqlConnection(GetConnectionString()))
        {
            using (var mySqlCmd =
                   new MySqlCommand(
                       "UPDATE tache SET Titre = @Titre, Description = @Description, date_debut = @date_debut, date_fin = @date_fin WHERE Id = @Id",
                       mySqlCn))
            {
                mySqlCmd.Parameters.AddWithValue("@Id", id);
                mySqlCmd.Parameters.AddWithValue("@Titre", updatedTache.Titre);
                mySqlCmd.Parameters.AddWithValue("@Description", updatedTache.Description);
                mySqlCmd.Parameters.AddWithValue("@date_debut", updatedTache.date_debut);
                mySqlCmd.Parameters.AddWithValue("@date_fin", updatedTache.date_fin);

                mySqlCn.Open();
                var rowsAffected = mySqlCmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }

    public bool DeleteTache(int id)
    {
        using (var mySqlCn = new MySqlConnection(GetConnectionString()))
        {
            using (var mySqlCmd = new MySqlCommand("DELETE FROM tache WHERE Id = @Id", mySqlCn))
            {
                mySqlCmd.Parameters.AddWithValue("@Id", id);

                mySqlCn.Open();
                var rowsAffected = mySqlCmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}