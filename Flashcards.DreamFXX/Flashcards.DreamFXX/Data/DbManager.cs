namespace Flashcards.DreamFXX.Data;

public class DbManager
{
    private readonly string _connectionString;
    public DbManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void CheckIfDbExists()
    {

    }
}
