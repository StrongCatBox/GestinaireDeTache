using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Linq;
using ToDoList;               
using ToDoList.DataAccess;  
using ToDoList.Models;


namespace TestProject1
{
    
    [TestFixture]
    internal class TacheServiceTest
    {

        private IConfiguration _config;
        private string _dbName;
        private string _connexionMaster;
        private string _connexionTestTemplate;
        private string _connexionTestFinal;
        private TacheRepository _repository;


        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Charge les appsettings du PROJET DE TESTS
            _config = new ConfigurationBuilder()
                .SetBasePath(TestContext.CurrentContext.TestDirectory)
                .AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true)
                .Build();

            // Récupère les chaînes depuis appsettings.json (tests)
            _connexionMaster = _config.GetConnectionString("MasterConnection");
            _connexionTestTemplate = _config.GetConnectionString("TestConnection");
        }


        [SetUp]
        public void Setup()
        {
            _dbName = "GestionTaches_Test";
            _connexionTestFinal = _connexionTestTemplate.Replace("{DB_NAME}", _dbName);

            // 2) CREATE DATABASE
            using (var conn = new SqlConnection(_connexionMaster))
            {
                conn.Open();
                using var cmd = new SqlCommand($"CREATE DATABASE [{_dbName}]", conn);
                cmd.ExecuteNonQuery();
            }

            using (var conn = new SqlConnection(_connexionTestFinal))
            {
                conn.Open();
                string createTable = @"
                    CREATE TABLE Taches (
                        Id INT IDENTITY PRIMARY KEY,
                        Nom NVARCHAR(15) NOT NULL,
                        DateCreation DATETIME NOT NULL,
                        DateFermeture DATETIME NULL,
                        Description NVARCHAR(MAX) NULL
                    )";

                using (var cmd = new SqlCommand(createTable, conn))

                    cmd.ExecuteNonQuery();
            }
                
                Environment.SetEnvironmentVariable("TEST_DB_CONNECTION", _connexionTestFinal);

            _repository = new TacheRepository();


            


        }

        [TearDown]
        public void TearDown()
        {
            // Supprime la DB
            using var conn = new SqlConnection(_connexionMaster);
            conn.Open();
            var drop = $@"
              ALTER DATABASE [{_dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
              DROP DATABASE [{_dbName}];";
            using var cmd = new SqlCommand(drop, conn);
            cmd.ExecuteNonQuery();

            // Nettoie l'ENV
            Environment.SetEnvironmentVariable("TEST_DB_CONNECTION", null);
        }

        // get all
        [Test]
        public void TestGetAll()
        {
            //on insère 2 tâches
            var tache = new Tache { Nom = "Test1", Description = "Description", DateCreation = DateTime.Now };
            _repository.Add(tache);

            //  on lit tout
            var toutes = _repository.GetAll();

            // Assert (vérification) 
            Assert.That(toutes, Is.Not.Null);           
            Assert.That(toutes.Count, Is.EqualTo(1));   
        }

       // creer
        [Test]
        public void TestAddNewTask()
        {
            
            var t = new Tache
            {
                Nom = "TacheSimple",
                Description = "Description simple",
                DateCreation = DateTime.Now             
               
            };

            _repository.Add(t);
            var toutes = _repository.GetAll();

            Assert.That(toutes.Count, Is.EqualTo(1));                 
            Assert.That(toutes[0].Nom, Is.EqualTo("TacheSimple"));   
            Assert.That(toutes[0].Description, Is.EqualTo("Description simple")); 
            Assert.That(toutes[0].DateCreation, Is.Not.EqualTo(default(DateTime))); 
            Assert.That(toutes[0].DateFermeture, Is.Null);            
        }

       // terminer
        [Test]
        public void TestTerminerTask()
        {
            
            _repository.Add(new Tache { Nom = "A_terminer", Description = "à finir", DateCreation = DateTime.Now });

            
            var id = _repository.GetAll()[0].Id;

            
            _repository.Terminer(id);

           
            var apres = _repository.GetAll().First(x => x.Id == id);

            Assert.That(apres.DateFermeture, Is.Not.Null);
        }

     // supprimer
        [Test]
        public void TestDeleteTask()
        {
           
            _repository.Add(new Tache { Nom = "A_supprimer", Description = "à supprimer", DateCreation = DateTime.Now });

         
            var id = _repository.GetAll()[0].Id;

            
            _repository.Delete(id);

         
            var toutes = _repository.GetAll();
            Assert.That(toutes.Count, Is.EqualTo(0));
        }

    }
}
