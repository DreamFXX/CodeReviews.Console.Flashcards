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

    public void TablesExistOrInitialize()
    {
        var query = @"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Stacks')
                            BEGIN
                                CREATE TABLE Stacks (
                                    Id INT PRIMARY KEY IDENTITY(1,1),
                                    Name NVARCHAR(100) NOT NULL,
                                    Description NVARCHAR(1000) NOT NULL
                                )
                            END
                           ";

        ExecuteNonQuery(query);
    }
}
