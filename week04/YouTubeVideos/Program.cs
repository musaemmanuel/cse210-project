using System;
using System.Collections.Generic;

public class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }

    public override string ToString()
    {
        return $"{CommenterName}: {Text}";
    }
}

public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    private List<Comment> Comments { get; set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }

    public void DisplayInfo()
    {
        TimeSpan duration = TimeSpan.FromSeconds(Length);
        Console.WriteLine($"Title: {Title}\nAuthor: {Author}\nLength: {duration.Minutes}m {duration.Seconds}s\nComments ({GetCommentCount()}):");
        foreach (var comment in Comments)
        {
            Console.WriteLine(comment);
        }
        Console.WriteLine();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        List<Video> videos = new List<Video>
        {
            new Video("Understanding C# Classes", "John Doe", 540),
            new Video("Advanced OOP in C#", "Jane Smith", 720),
            new Video("C# Tips and Tricks", "Alex Brown", 300)
        };

        videos[0].AddComment(new Comment("Alice", "Great explanation!"));
        videos[0].AddComment(new Comment("Bob", "Very helpful, thanks!"));
        videos[0].AddComment(new Comment("Charlie", "Loved the examples."));

        videos[1].AddComment(new Comment("Dave", "This is gold!"));
        videos[1].AddComment(new Comment("Eve", "Learned a lot!"));
        videos[1].AddComment(new Comment("Frank", "Could use more examples."));

        videos[2].AddComment(new Comment("Grace", "Nice tips!"));
        videos[2].AddComment(new Comment("Heidi", "Really useful content."));
        videos[2].AddComment(new Comment("Ivan", "Short and sweet."));

        foreach (var video in videos)
        {
            video.DisplayInfo();
        }
    }
}
