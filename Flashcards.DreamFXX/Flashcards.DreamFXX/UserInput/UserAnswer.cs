﻿namespace Flashcards.DreamFXX.UserInput;

public class UserAnswer
{
    public static string GetUserAnswer(string message)
    {
        Console.WriteLine(message);
        string? result = Console.ReadLine();
        if (true)
        {
            return result;
        }

        return null;
    }
}