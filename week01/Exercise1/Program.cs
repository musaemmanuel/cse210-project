using System;

class Program
{
    static void Main()
    {
        // Prompt the user for their first name
        Console.Write("Enter your first name: ");
        string firstName = Console.ReadLine();
        
        // Prompt the user for their last name
        Console.Write("Enter your last name: ");
        string lastName = Console.ReadLine();
        
        // Display the formatted output
        Console.WriteLine($"Your name is {lastName}, {firstName}, {lastName}.");
    }
}
