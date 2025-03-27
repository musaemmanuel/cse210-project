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
}

public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    private List<Comment> Comments { get; set; } = new List<Comment>();

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetCommentCount() => Comments.Count;

    public void DisplayVideoDetails()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Comments ({GetCommentCount()}):");
        foreach (var comment in Comments)
        {
            Console.WriteLine($"- {comment.CommenterName}: {comment.Text}");
        }
        Console.WriteLine();
    }
}

public class Program
{
    public static void Main()
    {
        var videos = new List<Video>
        {
            new Video("Study Book of Mormon", "Student Home work", 200),
            new Video("Unboxing Experience", "Gadget Guy", 250),
            new Video("How-To Guide", "DIY Master", 400)
        };

        videos[0].AddComment(new Comment("Koweh", "Great Study!"));
        videos[0].AddComment(new Comment("Bob", "Very informative."));
        videos[0].AddComment(new Comment("Charlie", "Loved it!"));

        videos[1].AddComment(new Comment("Diana", "So cool!"));
        videos[1].AddComment(new Comment("Eve", "Can’t wait to try this."));
        videos[1].AddComment(new Comment("Frank", "Looks awesome!"));

        videos[2].AddComment(new Comment("George", "This helped a lot."));
        videos[2].AddComment(new Comment("Naomi", "Clear and concise."));
        videos[2].AddComment(new Comment("Emma", "Perfect guide!"));

        foreach (var video in videos)
        {
            video.DisplayVideoDetails();
        }
    }
}
