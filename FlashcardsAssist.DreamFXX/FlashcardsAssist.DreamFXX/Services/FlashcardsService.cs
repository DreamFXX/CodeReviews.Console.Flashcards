using FlashcardsAssist.DreamFXX.Models;
using FlashcardsAssist.DreamFXX.Data;
using Spectre.Console;

namespace FlashcardsAssist.DreamFXX.Services;
public class FlashcardsService
{
    private readonly DatabaseService _dbService;
    private readonly StacksService _stacksService;

    public FlashcardsService(DatabaseService dbService, StacksService stacksService)
    {
        _dbService = dbService;
        _stacksService = stacksService;
    }

    public async Task AddFlashcardAsync()
    {
        var front = AnsiConsole.Ask<string>("[yellow]Enter the front of the flashcard:[/]");
        if (string.IsNullOrWhiteSpace(front))
        {
            AnsiConsole.MarkupLine("[red]Front of the flashcard cannot be empty.[/]");
            return;
        }

        var back = AnsiConsole.Ask<string>("[yellow]Enter the back of the flashcard:[/]");
        if (string.IsNullOrWhiteSpace(back))
        {
            AnsiConsole.MarkupLine("[red]Back of the flashcard cannot be empty.[/]");
            return;
        }

        var stack = await _stacksService.SelectStackAsync();
        if (stack == null) return;

        front = AnsiConsole.Ask<string>("[yellow]Enter the front of the flashcard:[/]");
        back = AnsiConsole.Ask<string>("[yellow]Enter the back of the flashcard:[/]");
        try
        {
            await _dbService.AddFlashcardAsync(stack.Id, front, back);
            AnsiConsole.MarkupLine($"[green]Flashcard added to stack '{stack.Name}' successfully![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error with adding a flashcard: {ex.Message}[/]");
        }
    }

    public async Task ViewFlashcardsAsync()
    {
        var stack = await _stacksService.SelectStackAsync();
        if (stack == null) return;

        var flashcards = await _dbService.GetFlashcardsForStackAsync(stack.Name);
        if (!flashcards.Any())
        {
            AnsiConsole.MarkupLine($"[yellow]No flashcards found in stack '{stack.Name}'.[/]");
            return;
        }

        var table = new Table()
            .Title($"[yellow]Flashcards in Stack: {stack.Name}[/]")
            .AddColumn(new TableColumn("ID").Centered()) // This ID is the DisplayId
            .AddColumn(new TableColumn("Front").LeftAligned())
            .AddColumn(new TableColumn("Back").LeftAligned());

        foreach (var card in flashcards)
        {
            table.AddRow(card.DisplayId.ToString(), card.Front, card.Back); // Use DisplayId
        }

        AnsiConsole.Write(table);
    }

    public async Task<List<Flashcard>> GetFlashcardsForStudyAsync(string stackName)
    {
        // This method might need adjustment if study logic relies on DatabaseId,
        // but for now, it fetches the full Flashcard object which includes the DB Id.
        return await _dbService.GetFlashcardsForStudyAsync(stackName);
    }

    // Helper method to select a flashcard by its DisplayId
    private async Task<FlashcardDto?> SelectFlashcardAsync(string stackName)
    {
        var flashcards = await _dbService.GetFlashcardsForStackAsync(stackName);
        if (!flashcards.Any())
        {
            AnsiConsole.MarkupLine($"[yellow]No flashcards found in stack '{stackName}' to select.[/]");
            return null;
        }

        AnsiConsole.MarkupLine($"[cyan]Flashcards in Stack: {stackName}[/]");
        var table = new Table()
            .AddColumn(new TableColumn("ID").Centered()) // DisplayId
            .AddColumn(new TableColumn("Front").LeftAligned())
            .AddColumn(new TableColumn("Back").LeftAligned());

        foreach (var card in flashcards)
        {
            table.AddRow(card.DisplayId.ToString(), card.Front, card.Back);
        }
        AnsiConsole.Write(table);

        while (true)
        {
            var displayIdInput = AnsiConsole.Ask<string>("[yellow]Enter the ID of the flashcard to select (or type 'cancel'):[/]");
            if (displayIdInput.Equals("cancel", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            if (int.TryParse(displayIdInput, out int displayId))
            {
                var selectedCard = flashcards.FirstOrDefault(f => f.DisplayId == displayId);
                if (selectedCard != null)
                {
                    return selectedCard; // Return the DTO which contains the DatabaseId
                }
                else
                {
                    AnsiConsole.MarkupLine($"[red]Invalid ID '{displayId}'. Please try again.[/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid input. Please enter a number.[/]");
            }
        }
    }

    public async Task UpdateFlashcardAsync()
    {
        var stack = await _stacksService.SelectStackAsync();
        if (stack == null) return;

        var flashcardToUpdate = await SelectFlashcardAsync(stack.Name);
        if (flashcardToUpdate == null)
        {
            AnsiConsole.MarkupLine("[yellow]Update cancelled.[/]");
            return;
        }

        AnsiConsole.MarkupLine($"\n[cyan]Updating Flashcard ID: {flashcardToUpdate.DisplayId}[/]");
        AnsiConsole.MarkupLine($"[grey]Current Front: {flashcardToUpdate.Front}[/]");
        var newFront = AnsiConsole.Ask<string>("[yellow]Enter the new front (leave blank to keep current):[/]");
        if (string.IsNullOrWhiteSpace(newFront))
        {
            newFront = flashcardToUpdate.Front; // Keep current if blank
        }

        AnsiConsole.MarkupLine($"[grey]Current Back: {flashcardToUpdate.Back}[/]");
        var newBack = AnsiConsole.Ask<string>("[yellow]Enter the new back (leave blank to keep current):[/]");
        if (string.IsNullOrWhiteSpace(newBack))
        {
            newBack = flashcardToUpdate.Back; // Keep current if blank
        }

        try
        {
            await _dbService.UpdateFlashcardAsync(flashcardToUpdate.DatabaseId, newFront, newBack);
            AnsiConsole.MarkupLine($"[green]Flashcard ID {flashcardToUpdate.DisplayId} in stack '{stack.Name}' updated successfully![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error updating flashcard: {ex.Message}[/]");
        }
    }

    public async Task DeleteFlashcardAsync()
    {
        var stack = await _stacksService.SelectStackAsync();
        if (stack == null) return;

        var flashcardToDelete = await SelectFlashcardAsync(stack.Name);
        if (flashcardToDelete == null)
        {
            AnsiConsole.MarkupLine("[yellow]Delete cancelled.[/]");
            return;
        }

        AnsiConsole.MarkupLine($"\n[red]You are about to delete Flashcard ID: {flashcardToDelete.DisplayId}[/]");
        AnsiConsole.MarkupLine($"[grey]Front: {flashcardToDelete.Front}[/]");
        AnsiConsole.MarkupLine($"[grey]Back: {flashcardToDelete.Back}[/]");

        if (!AnsiConsole.Confirm($"[red on yellow]Are you sure you want to delete this flashcard from stack '{stack.Name}'?[/]"))
        {
            AnsiConsole.MarkupLine("[yellow]Deletion cancelled.[/]");
            return;
        }

        try
        {
            await _dbService.DeleteFlashcardAsync(flashcardToDelete.DatabaseId);
            AnsiConsole.MarkupLine($"[green]Flashcard ID {flashcardToDelete.DisplayId} deleted from stack '{stack.Name}' successfully![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error deleting flashcard: {ex.Message}[/]");
        }
    }
}
