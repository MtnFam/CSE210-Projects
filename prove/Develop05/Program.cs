using System;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using System.IO;

public class Program
{
    static void Main(string[] args)
    {
        User user = new User();
        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("\n=== Goal Tracker ===");
            Console.WriteLine("1. Add Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    AddGoalMenu(user);
                    Pause();
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine(user.listGoals());
                    Pause();
                    break;

                case "3":
                    Console.Clear();
                    user.Save();
                    Console.WriteLine("Goals saved.");
                    Pause();
                    break;

                case "4":
                    Console.Clear();
                    user.Load();
                    Console.WriteLine("Goals loaded.");
                    Pause();
                    break;

                case "5":
                    Console.Clear();
                    RecordEvent(user);
                    Pause();
                    break;

                case "6":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void AddGoalMenu(User user)
    {
        Console.WriteLine("\nChoose Goal Type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Type: ");

        string type = Console.ReadLine();

        Console.Write("Goal Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string desc = Console.ReadLine();

        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        bool status = false;

        if (type == "1")
        {
            user.AddGoal(new SimpleGoal(name, desc, points, status));
        }
        else if (type == "2")
        {
            Console.Write("Times Completed: ");
            int times = int.Parse(Console.ReadLine());

            user.AddGoal(new EternalGoal(name, desc, points, status, times));
        }
        else if (type == "3")
        {
            Console.Write("Times Required: ");
            int times = int.Parse(Console.ReadLine());

            Console.Write("Reward: ");
            int reward = int.Parse(Console.ReadLine());

            user.AddGoal(new ChecklistGoal(name, desc, points, status, reward, times));
        }
        else
        {
            Console.WriteLine("Invalid goal type.");
        }
    }
    static void RecordEvent(User user)
    {
        Console.WriteLine("\nWhich goal did you complete?");

        bool hasIncomplete = false;

        for (int i = 0; i < user.GoalCount(); i++)
        {
            if (!user.GetGoal(i).IsComplete())
            {
                hasIncomplete = true;
                Console.WriteLine($"{i + 1}. {user.GetGoal(i).toString()}");
            }
        }

        if (!hasIncomplete)
        {
            Console.WriteLine("There are no goals available to record.");
            return;
        }

        Console.Write("Enter number: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        Goal g = user.GetGoal(index);

        int pointsEarned = g.CompleteGoal();
        user.AddScore(pointsEarned);

        Console.WriteLine($"You earned {pointsEarned} points!");
    }

    static void Pause()
        {
            Console.WriteLine("\nPress ENTER to continue...");
            Console.ReadLine();
        }

}

public class User
{
    private List<Goal> lrb_goals = new List<Goal>();
    private int lrb_score;
    public string listGoals()
    {
        string format = "";
        var simpleGoals = lrb_goals.OfType<SimpleGoal>();
        var eternalGoals = lrb_goals.OfType<EternalGoal>();
        var checklistGoals = lrb_goals.OfType<ChecklistGoal>();
        format += "=== Simple Goals ===\n";
        foreach (var g in simpleGoals)
        {
            format += g.toString() + "\n";
        }

        format += "\n=== Eternal Goals ===\n";
        foreach (var g in eternalGoals)
        {
            format += g.toString() + "\n";
        }

        format += "\n=== Checklist Goals ===\n";
        foreach (var g in checklistGoals)
        {
            format += g.toString() + "\n";
        }
        format += $"\nTotal Score: {lrb_score}\n";

        return format;
    }
    public void AddGoal(Goal g)
    {
        lrb_goals.Add(g);
    }
    public void Save()
    {
        string format = "";

        format += $"SCORE:{lrb_score}\n";
        var simpleGoals = lrb_goals.OfType<SimpleGoal>();
        var eternalGoals = lrb_goals.OfType<EternalGoal>();
        var checklistGoals = lrb_goals.OfType<ChecklistGoal>();
        format += "=== Simple Goals ===\n";
        foreach (var g in simpleGoals)
        {
            format += g.toFile() + "\n";
        }

        format += "\n=== Eternal Goals ===\n";
        foreach (var g in eternalGoals)
        {
            format += g.toFile() + "\n";
        }

        format += "\n=== Checklist Goals ===\n";
        foreach (var g in checklistGoals)
        {
            format += g.toFile() + "\n";
        }
        File.WriteAllText("goals.txt", format);
    }
    public void Load()
    {
        lrb_goals.Clear();

        if (!File.Exists("goals.txt"))
        {
            Console.WriteLine("No save file found.");
            return;
        }

        string[] lines = File.ReadAllLines("goals.txt");

        foreach (string line in lines)
        {
            // Skip headers and blank lines
            if (string.IsNullOrWhiteSpace(line)) continue;
            if (line.StartsWith("SCORE:"))
            {
                lrb_score = int.Parse(line.Substring(6));
                continue;
            }
            if (line.StartsWith("===")) continue;

            string[] parts = line.Split('|');

            // Basic sanity check
            if (parts.Length < 4)
                continue;

            string name = parts[0];
            string desc = parts[1];
            int points = int.Parse(parts[2]);
            bool status = bool.Parse(parts[3]);

            // SimpleGoal format: 4 fields
            if (parts.Length == 4)
            {
                lrb_goals.Add(new SimpleGoal(name, desc, points, status));
            }
            // EternalGoal format: 5 fields
            else if (parts.Length == 5)
            {
                int times = int.Parse(parts[4]);
                lrb_goals.Add(new EternalGoal(name, desc, points, status, times));
            }
            // ChecklistGoal format: 7 fields
            else if (parts.Length == 7)
            {
                int times = int.Parse(parts[4]);
                int reward = int.Parse(parts[5]);

                string[] comp = parts[6].Split('/');
                int completed = int.Parse(comp[0]);

                lrb_goals.Add(new ChecklistGoal(name, desc, points, status, reward, times, completed));
            }
        }
    }

    public Goal GetGoal(int index)
    {
        return lrb_goals[index];
    }

    public int GoalCount()
    {
        return lrb_goals.Count;
    }

    public void AddScore(int points)
    {
        lrb_score += points;
        Console.WriteLine($"Total Score: {lrb_score}");
    }
 
}
public class Goal
{
    // Attributes
    protected string lrb_goalName;
    protected string lrb_description;
    protected int lrb_points;
    
    protected bool lrb_status;
    
    // Methods
    public virtual string toString()
    {
        return $"{lrb_goalName} - {lrb_description} ({lrb_points} pts)";
    }
    
    public virtual string toFile()
    {
        return $"{lrb_goalName}|{lrb_description}|{lrb_points}|{lrb_status}";
    }
    // Constructor
    public Goal(string name, string description, int points, bool status)
    {
        lrb_goalName = name;
        lrb_description = description;
        lrb_points = points;
        lrb_status = status;
    }
    public virtual int CompleteGoal()
    {
        lrb_status = true;
        return lrb_points;
    }
    public bool IsComplete()
    {
        return lrb_status;
    }
}
public class SimpleGoal : Goal
{
public SimpleGoal(string name, string description, int points, bool status) : base(name, description, points, status)
    {  
    }
}


class EternalGoal : Goal
{
    private int lrb_times;
    public EternalGoal(
        string name, 
        string description, 
        int points, 
        bool status, 
 
        int times
        ) : base(name, description, points, status)
    {
        lrb_times = times;
    }
    public override string toFile()
    {
        return $"{lrb_goalName}|{lrb_description}|{lrb_points}|{lrb_status}|{lrb_times}";
    }
    public override string toString()
    {
        return base.toString();
    }
    public override int CompleteGoal()
    {
        lrb_times++;
        return lrb_points;
    }

}
class ChecklistGoal : Goal
{
    private int lrb_times;
    private int lrb_reward;
    private int lrb_completed;
    public ChecklistGoal(
        string name, 
        string description, 
        int points, 
        bool status, 
        int rewards, 
        int times,
        int completed=0
        ) : base(name, description, points, status)
    {
        lrb_times = times;
        lrb_reward = rewards;
        lrb_completed = completed;
    }
    public override string toFile()
    {
        return $"{lrb_goalName}|{lrb_description}|{lrb_points}|{lrb_status}|{lrb_times}|{lrb_reward}|{lrb_completed}/{lrb_times}";
    }
    public override string toString()
    {
        string statusText = lrb_status ? "Completed!" : $"{lrb_completed}/{lrb_times}";
        return $"{lrb_goalName} - {lrb_description} ({lrb_points} pts) [{statusText}]";
    }
    public override int CompleteGoal()
    {   
    
        if (lrb_status)
        {
            return 0; // no more points
        }

        lrb_completed++;

        if (lrb_completed >= lrb_times)
        {
            lrb_status = true;
            return lrb_points + lrb_reward;
        }

        return lrb_points;
    }

}