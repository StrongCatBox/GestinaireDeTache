# Gestionnaire de Tâches (CRUD)

Projet réalisé dans le cadre de ma formation **Concepteur Développeur d’Applications (CDA)**.  
Il s’agit d’une application console C# permettant de gérer des tâches avec une base SQL Server.  
Les tests unitaires sont réalisés avec **NUnit**.

---

## Structure du projet

/ToDoList/ → Projet principal
/DataAccess/ → Accès aux données 
/Models/ → Modèles
/Services/ → Services applicatifs
appsettings.json → Configuration 

/TestProject1/ → Projet de tests NUnit
appsettings.Test.json → Configuration dédiée aux tests 
TacheServiceTest.cs → Tests unitaires CRUD

ToDoList.sln → Fichier de solution Visual Studio


---

##  Fonctionnalités

-  **Créer** une tâche (nom ≤ 15 caractères, description, date auto)
-  **Lister** toutes les tâches
-  **Terminer** une tâche (ajout d’une date de fermeture)
-  **Supprimer** une tâche

---

##  Base de données

Les tables sont générées automatiquement dans la base choisie (prod ou test).

Exemple de table `Taches` :

```sql
CREATE TABLE Taches (
    Id INT IDENTITY PRIMARY KEY,
    Nom NVARCHAR(15) NOT NULL,
    DateCreation DATETIME NOT NULL,
    DateFermeture DATETIME NULL,
    Description NVARCHAR(MAX) NULL
);
```
## Tests unitaires

Les tests utilisent NUnit avec une base de données de test créée/supprimée à chaque exécution.
Fichier principal : TestProject1/TacheServiceTest.cs

Scénarios testés :

 Création d’une tâche

 Lecture de toutes les tâches

 Terminaison d’une tâche

 Suppression d’une tâche


## Configuration

Les chaînes de connexion sont stockées dans :

ToDoList/appsettings.json → pour l’application principale (base locale/prod)

TestProject1/appsettings.Test.json → pour les tests automatiques (base de test)

Ces fichiers contiennent des infos sensibles (mots de passe SQL) et sont donc ignorés par Git (.gitignore).

Exemple de appsettings.template.json (à adapter) :

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost\\SQLSERVER2022;Initial Catalog=GestionTaches;User ID=SA;Password=****;TrustServerCertificate=True",
    "TestConnection": "Data Source=localhost\\SQLSERVER2022;Initial Catalog=GestionTaches_Test;User ID=SA;Password=****;TrustServerCertificate=True",
    "MasterConnection": "Data Source=localhost\\SQLSERVER2022;Initial Catalog=master;User ID=SA;Password=****;TrustServerCertificate=True"
  }
} 
```




