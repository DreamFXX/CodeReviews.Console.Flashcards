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



