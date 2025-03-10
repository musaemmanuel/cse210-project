using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int>();
        int number;

        Console.WriteLine("Enter a series of numbers (enter 0 to stop):");

        do
        {
            Console.Write("Enter a number: ");
            number = int.Parse(Console.ReadLine());
            
            if (number != 0)
            {
                numbers.Add(number);
            }
        } while (number != 0);

        if (numbers.Count > 0)
        {
            int sum = numbers.Sum();
            double average = numbers.Average();
            int max = numbers.Max();
            int? smallestPositive = numbers.Where(n => n > 0).DefaultIfEmpty().Min();
            numbers.Sort();

            Console.WriteLine($"Total sum: {sum}");
            Console.WriteLine($"Average: {average}");
            Console.WriteLine($"Largest number: {max}");
            Console.WriteLine($"Smallest positive number: {(smallestPositive == 0 ? "None" : smallestPositive.ToString())}");
            Console.WriteLine("Sorted numbers: " + string.Join(", ", numbers));
        }
        else
        {
            Console.WriteLine("No numbers entered.");
        }
    }
}
