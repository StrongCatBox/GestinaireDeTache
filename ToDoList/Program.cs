using GestionTaches.Services;
using System;

namespace GestionTaches
{
    internal class Program
    {
        static void Main()
        {
            TacheService service = new TacheService();
            bool continuer = true;

            while (continuer)
            {
                Console.WriteLine("\n--- MENU ---");
                Console.WriteLine("1. Créer une tâche");
                Console.WriteLine("2. Lister les tâches");
                Console.WriteLine("3. Supprimer une tâche");
                Console.WriteLine("4. Terminer une tâche");
                Console.WriteLine("5. Quitter");
                Console.Write("Choix : ");
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1": service.CreerTache(); break;
                    case "2": service.ListerTaches(); break;
                    case "3": service.SupprimerTache(); break;
                    case "4": service.TerminerTache(); break;
                    case "5": continuer = false; break;
                }
            }
        }
    }
}
