namespace ToDoList.Models
{
    internal class Tache
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateFermeture { get; set; }
        public string Description { get; set; }

        public void Afficher()
        {
            Console.WriteLine($"{Id} | {Nom} | Créée: {DateCreation} | Fermée: {DateFermeture} | {Description}");
        }
    }
}
