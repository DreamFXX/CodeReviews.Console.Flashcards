using Flashcards.DreamFXX.Data;
using Flashcards.DreamFXX.Models;
using Flashcards.DreamFXX.Services;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

string dir = Directory.GetCurrentDirectory();
string rootDir = Path.Combine(dir, @"..\..\..\");

string appConfigFile = Path.Combine(rootDir, "Properties");
var cnnConfig = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json")
    .AddJsonFile($"{appConfigFile}\\appsettings.json", optional: true, reloadOnChange: true)
    .Build();

string? connectionString = cnnConfig.GetConnectionString("DefaultConnection");

var dbManager = new DbManager(connectionString);
var cardStackService = new CardStackService(dbManager);
var cardService = new CardService(dbManager);
var studySessionService = new StudySessionService(dbManager);

dbManager.DbExistCheck();

var mainMenuRoutes = new List<mainMenuRoutes>
{
    new() = { }
}

