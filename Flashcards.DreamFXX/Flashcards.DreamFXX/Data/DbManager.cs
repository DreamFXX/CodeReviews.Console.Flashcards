using Flashcards.DreamFXX.Models;
using Microsoft.Data.SqlClient;

namespace Flashcards.DreamFXX.Data;

public class DbManager(string connectionString)
{
    private readonly string _connectionString = connectionString;

    private SqlConnection ConnectionInit()
    {
        return new SqlConnection(_connectionString);
    }

    public void ExecuteNonQuery(string query)
    {
        using var connection = ConnectionInit();
        connection.Open();
        using var command = new SqlCommand(query, connection);
        command.ExecuteNonQuery();
    }

    public void CheckDatabaseExists()
    {
        var query = @"
                IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Flashcards_Data')
                BEGIN
                    CREATE DATABASE FlashCards_Data
                END
              ";

        ExecuteNonQuery(query);

        TablesExistOrInitialize();
    }

    public void TablesExistOrInitialize()
    {
        var query = @"
            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Stacks')
            BEGIN
              CREATE TABLE Stacks (
                  Id INT PRIMARY KEY IDENTITY(1,1),
                  Name NVARCHAR(100) NOT NULL,
                  Description NVARCHAR(1000) NOT NULL
              )
            END

            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Cards')
            BEGIN
              CREATE TABLE Cards (
                  Id INT PRIMARY KEY IDENTITY,
                  Question NVARCHAR(1000) NOT NULL,
                  Answer NVARCHAR(1000) UNIQUE NOT NULL,
                  StackId INT NOT NULL,
                  FOREIGN KEY (StackId) REFERENCES Stacks(Id)
               )
            END

            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'StudySessions')
            BEGIN
              CREATE TABLE StudySessions (
              Id INT PRIMARY KEY IDENTITY,
              StackId INT NOT NULL,
              EndTime DATETIME NOT NULL,
              CorrectAnswers INT NOT NULL,
              WrongAnswers INT NOT NULL,
              FOREIGN KEY (StackId) REFERENCES Stacks(Id) ON DELETE CASCADE
              )
           END
        ";

        ExecuteNonQuery(query);

        Console.WriteLine("Tables have been initialized, Success!");
    }

    public List<CardStack> GetCardStacks()
    {
        var cardStacks = new List<CardStack>();
        var query = "SELECT * FROM Stacks";

        return null;
    }
}
