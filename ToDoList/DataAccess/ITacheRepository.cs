using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.DataAccess
{
    internal interface ITacheRepository
    {
        void Add(Tache t);

        List<Tache> GetAll();
        void Delete(int id);
        void Terminer(int id);
    }
}
