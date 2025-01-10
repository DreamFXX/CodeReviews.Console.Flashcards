namespace Flashcards.DreamFXX.Models;

public class Stack
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public List<CardStack> Cards { get; set; }
}
