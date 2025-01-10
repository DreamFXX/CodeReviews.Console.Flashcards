using System.ComponentModel.DataAnnotations.Schema;

namespace Flashcards.DreamFXX.Models;

public class StudySession
{
    public int Id { get; set; }

    [ForeignKey("CardStackId")]
    public int CardStackId { get; set; }
    public List<Card>? Cards { get; set; }
    public DateTime EndTime { get; set; }
    public int CorrectAnswers { get; set; }
    public int WrongAnswers { get; set; }
    public CardStack? CardStack { get; set; }
}
