# ProjectsSharp

## Description

ProjectsSharp est un projet de groupe en C# développé dans le cadre du module C#. Ce projet regroupe plusieurs applications, notamment un Morpion, une Calculatrice, une application météo (connectée à une API), une application de quiz, et un Calendrier.

Chaque application est implémentée avec une architecture MVC (Model-View-Controller) en utilisant ASP.NET Core.
Prérequis

Avant de lancer le projet, assurez-vous d'avoir les éléments suivants installés :
```
.NET SDK 6.0 ou supérieur
Git
Un éditeur de code (comme Visual Studio Code ou JetBrains Rider)
```
## Configuration de l'environnement

### Étape 1 : Cloner le dépôt
```bash
  git clone https://github.com/Juliendnte/Projects-CSharp-GroupeB
  cd ProjectsSharp/ProjectsSharp
```
### Étape 2 : Créer le fichier .env

Dans le dossier /ProjectsSharp/ProjectsSharp, créez un fichier .env avec le contenu suivant :
```dotenv
DB_CONNECTION_STRING='Server=your_server;Database=your_database;User Id=your_username;Password=your_password;'
```
Assurez-vous de remplacer 
###### your_server, your_database, your_username, your_password 
par vos informations de connexion à la base de données.

### Étape 3 : Installer les dépendances

Exécutez la commande suivante pour restaurer les packages NuGet :
```bash
  dotnet restore
```

## Lancer le projet

Pour démarrer l'application, restez dans le répertoire /ProjectsSharp/ProjectsSharp et lancez la commande suivante :
```bash
  dotnet run --launch-profile http
```
Une fois le projet lancé, un lien http://localhost:xxxx apparaîtra dans le terminal. Cliquez dessus pour accéder à l'application dans votre navigateur.
## Fonctionnalités
### Morpion

    Jouez au Morpion en mode joueur contre joueur.

### Calculatrice

    Effectuez des opérations de base telles que l'addition, la soustraction, la multiplication et la division.

### Application Météo

    Consultez la météo actuelle en utilisant une API.

### QuizApp

    Répondez à une série de questions à choix multiples stockées dans un fichier JSON.

### Calendrier

    Gérer vos tâches avec un calendrier interactif.
    Ajouter, modifier ou supprimer des événements.