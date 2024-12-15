using System.Configuration;
using Spectre.Console;
using FlashCards.DreamFXX.Data;
using FlashCards.DreamFXX.Models;
//using FlashCards.DreamFXX.Services;

string currentDir = Directory.GetCurrentDirectory();
string projectDirectory = Path.Combine(currentDir, @"..\..\..");
string configPath = Path.Combine(projectDirectory, "App.config");

string? connectionString = ConfigurationManager.ConnectionStrings["DefaultCnn"].ConnectionString;

var dbManager = new DatabaseHelper(connectionString);
//Services are not implemented yet



dbManager.CheckDatabaseExistOrInit();

var mainMenuOption = new List<MainMenuOption>
{
    new MainMenuOption() {Id = 1, Description = "Create new Stack."},
    new MainMenuOption() {Id = 2, Description = "Edit new Stack."},
    new MainMenuOption() {Id = 3, Description = "Delete new Stack."},
    new MainMenuOption() {Id = 4, Description = "Create new Card."},
    // + 4 more and exit
};
