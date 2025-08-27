using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;




namespace ToDoList
{
    internal class Connexion
    {

        private readonly string _connexion;


        public Connexion(string name ="DefaultConnection")
        {

            var envOverride = Environment.GetEnvironmentVariable("TEST_DB_CONNECTION");

            if (!string.IsNullOrWhiteSpace(envOverride))
            {
                _connexion = envOverride;
                return;
            }


            var env = System.Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
            var basePath = AppContext.BaseDirectory;
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                .Build();

            _connexion = config.GetConnectionString(name) ?? throw new InvalidOperationException($"ConnectionString '{name}' introuvable dans {basePath} (env={env}).");

          
      
          
        }

        public SqlConnection GetConnection()
        {
            var connexion = new SqlConnection(_connexion);
            connexion.Open();
            return connexion;
        }
    }
}
