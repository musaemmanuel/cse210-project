using System;
using System.Collections.Generic;

class Word
{
    public string Text { get; private set; }
    public bool IsVisible { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsVisible = true; // Default visibility is true
    }

    public void Hide() => IsVisible = false;
    public void Show() => IsVisible = true;
}

class Reference
{
    public string Book { get; private set; }
    public int Chapter { get; private set; }
    public int StartVerse { get; private set; }
    public int? EndVerse { get; private set; } // Nullable for single-verse references

    // Single-verse constructor
    public Reference(string book, int chapter, int verse)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = verse;
        EndVerse = null;
    }

    // Multi-verse constructor
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        return EndVerse.HasValue ? $"{Book} {Chapter}:{StartVerse}-{EndVerse}" : $"{Book} {Chapter}:{StartVerse}";
    }
}

class Scripture
{
    public Reference ScriptureReference { get; private set; }
    private List<Word> Words { get; set; }

    public Scripture(string book, int chapter, int verse, string text)
    {
        ScriptureReference = new Reference(book, chapter, verse);
        Words = CreateWordList(text);
    }

    public Scripture(string book, int chapter, int startVerse, int endVerse, string text)
    {
        ScriptureReference = new Reference(book, chapter, startVerse, endVerse);
        Words = CreateWordList(text);
    }

    private List<Word> CreateWordList(string text)
    {
        var words = new List<Word>();
        foreach (var word in text.Split(' '))
        {
            words.Add(new Word(word));
        }
        return words;
    }

    public void Display()
    {
        Console.Write($"{ScriptureReference}: ");
        foreach (var word in Words)
        {
            Console.Write(word.IsVisible ? word.Text + " " : "_____ ");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        Scripture scripture = new Scripture("John", 3, 16, "For God so loved the world, That He give His only Begotten Son, that whosover believe in Him shall not perish , but have everlasting life");
        scripture.Display();
    }
}
