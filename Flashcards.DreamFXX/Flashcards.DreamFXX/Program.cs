using Microsoft.Extensions.Configuration;
using Spectre.Console;

string dir = Directory.GetCurrentDirectory();
string rootDir = Path.Combine(dir, @"..\..\..\");

string appConfigFile = Path.Combine(rootDir, "Properties\\appsettings.json");


var cnnConfig = new ConfigurationBuilder();




