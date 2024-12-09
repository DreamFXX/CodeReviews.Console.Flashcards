using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

string? connectionString = @"Data Source=localhost;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

using SqlConnection connection = new SqlConnection(connectionString);
connection.Open();
SqlCommand command = connection.CreateCommand();
command.CommandText = @"CREATE TABLE IF NOT EXISTS Flashcards_Table (
                                                   Id INT IDENTITY(1,1),
                                                   PRIMARY KEY (Id),
                                                   Name TEXT NOT NULL)";
command.ExecuteNonQuery();
connection.Close();