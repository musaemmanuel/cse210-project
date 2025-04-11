using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }

    public Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
    }

    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetStatus();
    public abstract string Serialize();
}

class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points, bool isComplete = false)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return Points;
        }
        return 0;
    }

    public override bool IsComplete() => _isComplete;
    public override string GetStatus() => $"[ {(IsComplete() ? "X" : " ")} ] {Name} ({Description})";
    public override string Serialize() => $"Simple:{Name},{Description},{Points},{_isComplete}";
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent() => Points;
    public override bool IsComplete() => false;
    public override string GetStatus() => $"[ ] {Name} ({Description})";
    public override string Serialize() => $"Eternal:{Name},{Description},{Points}";
}

class ChecklistGoal : Goal
{
    private int _timesCompleted;
    private int _target;
    private int _bonus;
    private bool _isComplete;

    public ChecklistGoal(string name, string description, int points, int target, int bonus, int completed = 0)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _timesCompleted = completed;
        _isComplete = _timesCompleted >= _target;
    }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _timesCompleted++;
            if (_timesCompleted >= _target)
            {
                _isComplete = true;
                return Points + _bonus;
            }
            return Points;
        }
        return 0;
    }

    public override bool IsComplete() => _isComplete;
    public override string GetStatus() =>
        $"[ {(IsComplete() ? "X" : " ")} ] {Name} ({Description}) -- Completed {_timesCompleted}/{_target} times";

    public override string Serialize() =>
        $"Checklist:{Name},{Description},{Points},{_target},{_timesCompleted},{_bonus},{_isComplete}";
}

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int score = 0;
    static int simpleGoalsCompleted = 0;
    static int eternalGoalHits = 0;
    static int checklistGoalsCompleted = 0;
    static string filename = "goals.txt";

    static int level => score / 1000;

    static void Main()
    {
        while (true)
        {
            Console.WriteLine($"\nYour score: {score}");
            Console.WriteLine($"🎉 Level: {level}");
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Goal Event");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("6. Quit");

            Console.Write("Choose an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1": CreateGoal(); break;
                case "2": DisplayGoals(); break;
                case "3": RecordEvent(); break;
                case "4": SaveGoals(); break;
                case "5": LoadGoals(); break;
                case "6": return;
                default: Console.WriteLine("Invalid choice!"); break;
            }
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine("Select goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        string type = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Description: ");
        string desc = Console.ReadLine();
        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        switch (type)
        {
            case "1":
                goals.Add(new SimpleGoal(name, desc, points));
                break;
            case "2":
                goals.Add(new EternalGoal(name, desc, points));
                break;
            case "3":
                Console.Write("Target count: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Bonus points: ");
                int bonus = int.Parse(Console.ReadLine());
                goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
                break;
        }
    }

    static void DisplayGoals()
    {
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetStatus()}");
        }
    }

    static void RecordEvent()
    {
        DisplayGoals();
        Console.Write("Which goal did you accomplish? ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index >= 0 && index < goals.Count)
        {
            int points = goals[index].RecordEvent();
            score += points;

            Goal g = goals[index];
            Console.WriteLine($"You earned {points} points!");
            Console.WriteLine(GetMotivationMessage());

            if (g is SimpleGoal && g.IsComplete())
                simpleGoalsCompleted++;
            if (g is EternalGoal)
                eternalGoalHits++;
            if (g is ChecklistGoal && g.IsComplete())
                checklistGoalsCompleted++;

            // Level up message
            Console.WriteLine($"🎉 Level: {level}");

            // Achievements
            if (simpleGoalsCompleted == 5)
                Console.WriteLine("🏅 Achievement unlocked: Goal Crusher!");
            if (eternalGoalHits == 10)
                Console.WriteLine("🏅 Achievement unlocked: Consistency Champ!");
            if (checklistGoalsCompleted == 3)
                Console.WriteLine("🏅 Achievement unlocked: Checklist Hero!");
        }
        else
        {
            Console.WriteLine("Invalid goal index.");
        }
    }

    static void SaveGoals()
    {
        using (StreamWriter output = new StreamWriter(filename))
        {
            output.WriteLine(score);
            foreach (Goal goal in goals)
            {
                output.WriteLine(goal.Serialize());
            }
        }
        Console.WriteLine("Goals saved.");
    }

    static void LoadGoals()
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("Save file not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        goals.Clear();
        score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(':');
            string type = parts[0];
            string[] data = parts[1].Split(',');

            switch (type)
            {
                case "Simple":
                    goals.Add(new SimpleGoal(data[0], data[1], int.Parse(data[2]), bool.Parse(data[3])));
                    break;
                case "Eternal":
                    goals.Add(new EternalGoal(data[0], data[1], int.Parse(data[2])));
                    break;
                case "Checklist":
                    goals.Add(new ChecklistGoal(data[0], data[1], int.Parse(data[2]), int.Parse(data[3]), int.Parse(data[5]), int.Parse(data[4])));
                    break;
            }
        }

        Console.WriteLine("Goals loaded.");
    }

    static string GetMotivationMessage()
    {
        string[] messages = new[]
        {
            "🔥 You’re on fire!",
            "🎯 Bullseye! Goal achieved!",
            "🚀 Progress unlocked!",
            "💪 Keep it up, warrior!",
            "🌟 You just got better!",
            "🎉 That’s a win!",
            "🙌 Another step forward!",
        };
        Random rand = new Random();
        return messages[rand.Next(messages.Length)];
    }
}
