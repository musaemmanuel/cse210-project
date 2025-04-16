using System;
using System.Collections.Generic;

abstract class Activity
{
    private DateTime _date;
    private int _minutes;
    protected bool _useKilometers;

    public Activity(DateTime date, int minutes, bool useKilometers = false)
    {
        _date = date;
        _minutes = minutes;
        _useKilometers = useKilometers;
    }

    public DateTime Date => _date;
    public int Minutes => _minutes;

    public abstract double GetDistance(); // in miles or km
    public abstract double GetSpeed();    // mph or kph
    public abstract double GetPace();     // min per mile or min per km

    protected abstract string GetActivityName();
    protected abstract string GetActivityEmoji();

    public virtual string GetSummary()
    {
        string unit = _useKilometers ? "km" : "miles";
        string speedUnit = _useKilometers ? "kph" : "mph";
        string paceUnit = _useKilometers ? "min per km" : "min per mile";

        return $"{_date:dd MMM yyyy} {GetActivityEmoji()} {GetActivityName()} ({_minutes} min) - " +
               $"Distance: {GetDistance():0.00} {unit}, Speed: {GetSpeed():0.00} {speedUnit}, Pace: {GetPace():0.00} {paceUnit}";
    }
}

class Running : Activity
{
    private double _distanceMiles;

    public Running(DateTime date, int minutes, double distanceMiles, bool useKilometers = false)
        : base(date, minutes, useKilometers)
    {
        _distanceMiles = distanceMiles;
    }

    public override double GetDistance() => _useKilometers ? _distanceMiles * 1.60934 : _distanceMiles;

    public override double GetSpeed() => (GetDistance() / Minutes) * 60;

    public override double GetPace() => Minutes / GetDistance();

    protected override string GetActivityName() => "Running";

    protected override string GetActivityEmoji() => "🏃‍♂️";
}

class Cycling : Activity
{
    private double _speedMph;

    public Cycling(DateTime date, int minutes, double speedMph, bool useKilometers = false)
        : base(date, minutes, useKilometers)
    {
        _speedMph = speedMph;
    }

    public override double GetSpeed() => _useKilometers ? _speedMph * 1.60934 : _speedMph;

    public override double GetDistance() => (GetSpeed() * Minutes) / 60;

    public override double GetPace() => 60 / GetSpeed();

    protected override string GetActivityName() => "Cycling";

    protected override string GetActivityEmoji() => "🚴‍♀️";
}

class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int minutes, int laps, bool useKilometers = false)
        : base(date, minutes, useKilometers)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        double distanceKm = (_laps * 50) / 1000.0;
        return _useKilometers ? distanceKm : distanceKm * 0.621371;
    }

    public override double GetSpeed() => (GetDistance() / Minutes) * 60;

    public override double GetPace() => Minutes / GetDistance();

    protected override string GetActivityName() => "Swimming";

    protected override string GetActivityEmoji() => "🏊‍♂️";
}

class Program
{
    static void Main()
    {
        Console.WriteLine("🏋️‍♂️ Welcome to FitTrack! Choose unit (miles/km):");
        string unitChoice = Console.ReadLine()?.ToLower();
        bool useKm = unitChoice == "km";

        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0, useKm),
            new Cycling(new DateTime(2022, 11, 3), 30, 9.7, useKm),
            new Swimming(new DateTime(2022, 11, 3), 30, 20, useKm)
        };

        Console.WriteLine("\n📊 Activity Summaries:");
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
