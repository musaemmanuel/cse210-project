using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Would you like to enter your own scripture? (yes/no)");
        string userResponse = Console.ReadLine().ToLower();
        Scripture scripture;

        if (userResponse == "yes")
        {
            Console.Write("Enter the scripture reference (e.g., John 3:16): ");
            string reference = Console.ReadLine();
            Console.Write("Enter the scripture text: ");
            string text = Console.ReadLine();
            scripture = new Scripture(new ScriptureReference(reference), text);
        }
        else
        {
            List<Scripture> scriptures = LoadScriptures("scriptures.txt");
            scripture = scriptures[new Random().Next(scriptures.Count)];
        }
        
        Random random = new Random();
        while (!scripture.IsCompletelyHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide words, type 'reveal' to show hidden words, or type 'quit' to exit.");
            string input = Console.ReadLine().ToLower();
            
            if (input == "quit")
                return;
            if (input == "reveal")
                scripture.RevealWords();
            else
                scripture.HideRandomWords(random, 3);
        }
    }

    static List<Scripture> LoadScriptures(string filePath)
    {
        List<Scripture> scriptures = new List<Scripture>();
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 2)
                {
                    scriptures.Add(new Scripture(new ScriptureReference(parts[0]), parts[1]));
                }
            }
        }
        return scriptures;
    }
}

class ScriptureReference
{
    public string Book { get; }
    public string ChapterVerse { get; }

    public ScriptureReference(string reference)
    {
        var parts = reference.Split(' ');
        Book = parts[0];
        ChapterVerse = parts.Length > 1 ? parts[1] : "";
    }

    public override string ToString()
    {
        return $"{Book} {ChapterVerse}".Trim();
    }
}

class Word
{
    public string Text { get; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public void Reveal()
    {
        IsHidden = false;
    }

    public override string ToString()
    {
        return IsHidden ? new string('_', Text.Length) : Text;
    }
}

class Scripture
{
    public ScriptureReference Reference { get; }
    private List<Word> Words { get; }

    public Scripture(ScriptureReference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public string GetDisplayText()
    {
        return $"{Reference}\n{string.Join(" ", Words)}";
    }

    public void HideRandomWords(Random random, int count)
    {
        var visibleWords = Words.Where(w => !w.IsHidden).ToList();
        for (int i = 0; i < count && visibleWords.Count > 0; i++)
        {
            var wordToHide = visibleWords[random.Next(visibleWords.Count)];
            wordToHide.Hide();
            visibleWords.Remove(wordToHide);
        }
    }

    public void RevealWords()
    {
        foreach (var word in Words)
        {
            word.Reveal();
        }
    }

    public bool IsCompletelyHidden()
    {
        return Words.All(w => w.IsHidden);
    }
}
