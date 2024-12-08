using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

string? connectionString = new ConfigurationManager().GetConnectionString("ConnectionString");


