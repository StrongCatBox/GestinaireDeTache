using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using ToDoList.Models;
using ToDoList;

namespace ToDoList.DataAccess
{
    internal class TacheRepository : ITacheRepository
    {
        private readonly Connexion _connexion;

        public TacheRepository()
        {
            _connexion = new Connexion();
        }

        public TacheRepository(Connexion connexion)
        {
            _connexion = connexion;
        }

        // Ajouter une tâche
        public void Add(Tache tache)
        {
            using (SqlConnection connection = _connexion.GetConnection())
            {
                string sql = "INSERT INTO Taches (Nom, DateCreation, Description) VALUES (@nom, @date, @desc)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@nom", tache.Nom);
                command.Parameters.AddWithValue("@date", tache.DateCreation);
                command.Parameters.AddWithValue("@desc", tache.Description);
                command.ExecuteNonQuery();
            }
        }

        // Lister toutes les tâches
        public List<Tache> GetAll()
        {
            List<Tache> liste = new List<Tache>();

            using (SqlConnection connection = _connexion.GetConnection())
            {
                string sql = "SELECT Id, Nom, DateCreation, DateFermeture, Description FROM Taches";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    liste.Add(new Tache
                    {
                        Id = (int)reader["Id"],
                        Nom = reader["Nom"].ToString(),
                        DateCreation = (DateTime)reader["DateCreation"],
                        DateFermeture = reader["DateFermeture"] as DateTime?,
                        Description = reader["Description"].ToString()
                    });
                }
            }

            return liste;
        }

        // Supprimer une tâche
        public void Delete(int id)
        {
            using (SqlConnection connection = _connexion.GetConnection())
            {
                string sql = "DELETE FROM Taches WHERE Id = @id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        // Terminer une tâche
        public void Terminer(int id)
        {
            using (SqlConnection connection = _connexion.GetConnection())
            {
                string sql = "UPDATE Taches SET DateFermeture = @now WHERE Id = @id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@now", DateTime.Now);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
