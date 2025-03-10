using System;

class Program
{
    static void Main()
    {
        Random random = new Random();
        string playAgain;

        do
        {
            int magicNumber = random.Next(1, 101); // Generate random number between 1 and 100
            int guess;
            int attempts = 0;

            Console.WriteLine("I have selected a magic number between 1 and 100. Try to guess it!");

            do
            {
                Console.Write("Enter your guess: ");
                guess = int.Parse(Console.ReadLine());
                attempts++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher!");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower!");
                }
                else
                {
                    Console.WriteLine($"Congratulations! You guessed the magic number in {attempts} attempts.");
                }
            } while (guess != magicNumber);

            Console.Write("Do you want to play again? (yes/no): ");
            playAgain = Console.ReadLine().ToLower();
        } while (playAgain == "yes");

        Console.WriteLine("Thanks for playing! Goodbye.");
    }
}
