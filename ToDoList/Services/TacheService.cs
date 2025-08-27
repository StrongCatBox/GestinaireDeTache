using ToDoList.Models;
using ToDoList.DataAccess;
using System;
using System.Collections.Generic;
using ToDoList.Models;

namespace GestionTaches.Services
{
    internal class TacheService
    {
        private readonly TacheRepository _repo = new TacheRepository();

        public void CreerTache()
        {
            Console.Write("Nom : ");
            string nom = Console.ReadLine();

            Console.Write("Description : ");
            string desc = Console.ReadLine();

            Tache tache = new Tache
            {
                Nom = nom,
                DateCreation = DateTime.Now,
                Description = desc
            };

            _repo.Add(tache);
            Console.WriteLine("Tâche ajoutée.");
        }

        public void ListerTaches()
        {
            List<Tache> taches = _repo.GetAll();
            Console.WriteLine("\n Liste des tâches :");
            foreach (var t in taches)
            {
                t.Afficher();
            }
        }

        public void SupprimerTache()
        {
            Console.Write("Id de la tâche à supprimer : ");
            int id = int.Parse(Console.ReadLine());
            _repo.Delete(id);
            Console.WriteLine(" Tâche supprimée.");
        }

        public void TerminerTache()
        {
            Console.Write("Id de la tâche à terminer : ");
            int id = int.Parse(Console.ReadLine());
            _repo.Terminer(id);
            Console.WriteLine(" Tâche terminée.");
        }
    }
}
