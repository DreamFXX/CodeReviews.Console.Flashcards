namespace FlashcardsAssist.DreamFXX.Models;
public class FlashcardDto
{
    public int DisplayId { get; set; } // Sequential ID shown to the user within a stack
    public int DatabaseId { get; set; } // Actual ID from the database
    public string Front { get; set; } = string.Empty;
    public string Back { get; set; } = string.Empty;

}
