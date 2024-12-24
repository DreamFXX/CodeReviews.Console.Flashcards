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
    new MainMenuOption() {Id = 2, Description = "Edit existing Stack."},
    new MainMenuOption() {Id = 3, Description = "Delete existing Stack."},
    new MainMenuOption() {Id = 4, Description = "Create new Card."},
    new MainMenuOption() {Id = 5, Description = "Edit existing Card."},
    new MainMenuOption() {Id = 6, Description = "Delete existing Card."},
    new MainMenuOption() {Id = 7, Description = "Study a Stack"},
    new MainMenuOption() {Id = 8, Description = "Show study sessions per month."},
    new MainMenuOption() {Id = 0, Description = "Exit Application."}

};
