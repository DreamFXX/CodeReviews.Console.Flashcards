using System.Configuration;
using Microsoft.Data;
using Microsoft.Data.SqlClient;

namespace FlashCards.DreamFXX.Data;

public class DatabaseHelper(string connectionString)
{
    private readonly string? _connectionString = connectionString;
    //
    // Helpers (new class?)
    //
    private SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }
    void ExecuteNonQuery(string query)
    {
        using var connection = GetConnection();
        connection.Open();
        using var command = new SqlCommand(query, connection);
        command.ExecuteNonQuery();
    }
    //
    // Checks
    //
    public void CheckDatabaseExistOrInit()
    {
        var query = @"
                IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Flashcards_Data')
                    BEGIN
                        CREATE DATABASE FlashCards_Data
                    END
                    ";

        ExecuteNonQuery(query);

        EnsureTablesExist();
    }

    public void EnsureTablesExist()
    {
        var query = @"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Stacks')
                    BEGIN
                        CREATE TABLE Stacks (
                            Id INT PRIMARY KEY IDENTITY,
                            Name NVARCHAR(100) NOT NULL,
                            Description NVARCHAR(1000) NOT NULL
                        )
                    END
                    ";


        ExecuteNonQuery(query);
    }



}

