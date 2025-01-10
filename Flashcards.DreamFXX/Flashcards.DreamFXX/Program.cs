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

// DbManager
var cardStackService = new CardStackService();
// cards
// sessions
//

var mainMenuRoute = new List<MainMenuRoute>
{
        new () { Id = 1, Description = "Create new stack" },
        new () { Id = 2, Description = "Edit existing stack" },
        new () { Id = 3, Description = "Delete existing stack" },
        new () { Id = 4, Description = "Create new card" },
        new () { Id = 5, Description = "Edit existing card" },
        new () { Id = 6, Description = "Delete existing card" },
        new () { Id = 7, Description = "Study a stack" },
        new () { Id = 8, Description = "Show complete list of study sessions per month" },
        new () { Id = 0, Description = "Exit" }
};

while (true)
{
    Console.Clear();

    var menuSelection = AnsiConsole.Prompt(
        new SelectionPrompt<MainMenuRoute>()
        .Title("Write, edit, organize and most importantly...\n[yellow][underline]- EDUCATE! -[/][/]")
        .PageSize(10)
        .AddChoices(mainMenuRoute).UseConverter(route => route.Description));

    if (menuSelection.Id == 0)
    {
        break;
    }
    switch (menuSelection.Id)
    {
        case 1:
            Console.Clear();
            cardStackService.CreateNewStack();
            break;
        default:
            break;
    }
}
