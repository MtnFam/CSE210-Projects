using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Activity controller = new Activity();
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("       Mindfulness & Activity App       ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Change Animation Style");
            Console.WriteLine("5. Exit");
            Console.WriteLine("========================================");
            Console.Write("Select an option (1-5): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    controller.run(0); // 0 = Breathing
                    break;
                case "2":
                    controller.run(1); // 1 = Reflection
                    break;
                case "3":
                    controller.run(2); // 2 = Listing
                    break;
                case "4":
                    Console.Clear();
                    controller.alterAnimation();
                    break;
                case "5":
                    running = false;
                    Console.WriteLine("\nGoodbye! Stay mindful.");
                    break;
                default:
                    Console.WriteLine("\nInvalid option. Press Enter to try again.");
                    Console.ReadLine();
                    break;
            }

            if (running && choice != "4")
            {
                Console.WriteLine("\nPress Enter to return to the main menu...");
                Console.ReadLine();
            }
        }
    }
}

/*
Name: Nathan Hutchings
Date: 6-17-26
*/
class Reflection
{
    private int lrb_duration;
    private List<string> lrb_prompts = new List<string>()
    {
        "Think of a time when you stood up for someone else",
        "Think of a time when you did something really difficult",
        "Think of a time when you helped someone in need",
        "Think of a time when you did something truly selfless"
    };
    private List<string> lrb_questions = new List<string>()
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };
    public Reflection(int duration)
    {
        lrb_duration = duration;
    }
    private string GetRandomPrompt()
    {
        Random rand = new Random();
        int index = rand.Next(lrb_prompts.Count);
        return lrb_prompts[index];
    }

    private string GetRandomQuestion()
    {
        Random rand = new Random();
        int index = rand.Next(lrb_questions.Count);
        return lrb_questions[index];
    }

    public void run(Animation sharedAnimation)
    {
        Console.Clear();
        Console.WriteLine("Get ready...");

        // Corrected: Called .Play() on the animation object instead of alterAnimation
        sharedAnimation.Play(3); 
        Console.WriteLine();

        Console.WriteLine(GetRandomPrompt());
        Console.WriteLine("\nPress enter when you're ready.");
        Console.ReadLine();

        Console.WriteLine("Here are some questions to ponder:");

        for (int i = 5; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b"); 
        }
        Console.Clear();

        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(lrb_duration);

        while (DateTime.Now < endTime)
        {
            string question = GetRandomQuestion();
            Console.Write($"> {question} ");

            // Corrected: Called .Play() to make the spinner move
            sharedAnimation.Play(5);
            Console.WriteLine();
        }
    }
}

/*
Name: Sean Gillman
Date: 6/9/2026
*/
class Breathe
{
    private int lrb_time = 0;
    
    public Breathe(int timeS)
    {
        lrb_time = timeS;
    }

    public void run()
    {
        int leftover = lrb_time % 8;
        int cycle = lrb_time / 8;
        string[] breath = { "Breathe in...", "Breathe out..." };

        for (int i = cycle; i > 0; i--)
        {
            foreach (var phase in breath)
            {
                Console.WriteLine(phase);
                Thread.Sleep(1000);

                for (int count = 1; count <= 3; count++)
                {
                    Console.WriteLine($"{count}...");
                    Thread.Sleep(1000);
                }
            }
        }

        if (leftover > 0)
        {
            foreach (var phase in breath)
            {
                Console.WriteLine(phase);
                Thread.Sleep(1000);
                Console.WriteLine("1...");
                Thread.Sleep(1000);
            }
        }
    }
}

/* Renae Rogers - 6/9/26 - 6/21/26
*/
class Animation
{
    private int _animationType = 0;

    public Animation() { }

    public void SetAnimationType(int choice)
    {
        if (choice >= 0 && choice <= 2)
        {
            _animationType = choice;
        }
        else
        {
            _animationType = 0; 
        }
    }

    public void Play(int seconds)
    {
        Console.CursorVisible = false;
        int totalFrames = (seconds * 1000) / 150;
        List<string> frames = GetAnimationFrames();
        int frameIndex = 0;

        for (int i = 0; i < totalFrames; i++)
        {
            string currentFrame = frames[frameIndex];
            
            Console.Write(currentFrame);
            Thread.Sleep(150);

            for (int j = 0; j < currentFrame.Length; j++)
            {
                Console.Write("\b \b");
            }

            frameIndex = (frameIndex + 1) % frames.Count;
        }

        Console.CursorVisible = true;
    }

    private List<string> GetAnimationFrames()
    {
        switch (_animationType)
        {
            case 1:
                return new List<string> { ".", "..", "...", "....", "...", ".." };
            case 2:
                return new List<string> { "[    ]", "[=   ]", "[==  ]", "[=== ]", "[====]", "[ ===]", "[  ==]", "[   =]" };
            case 0:
            default:
                return new List<string> { "|", "/", "-", "\\" };
        }
    }
}

/*
Name: Sean Gillman
Date: 6/11/2026
*/
class Activity
{
    private List<string> _intro = new List<string> {
        "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.",
        "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area."
    };
    private List<string> lrb_name = new List<string> { "Breathing Activity", "Reflection Activity", "Listing Activity" };

    protected Animation lrb_loading = new Animation(); // Made protected so derived classes can use it if needed

    public Activity() { }

    public void run(int actnum)
    {
        int time = intro(actnum);
        switch (actnum)
        {
            case 0:
                Breathe br = new Breathe(time);
                br.run();
                break;
            case 1:
                Reflection re = new Reflection(time);
                re.run(lrb_loading);
                break;
            case 2:
                Listing li = new Listing(time);
                li.run(); // Corrected lowercase match
                break;
        }
        outro(actnum);
    }

    private int intro(int actnum)
    {
        Console.WriteLine($"Welcome to the {lrb_name[actnum]}.\n\n{_intro[actnum]}\n\nFor how long, in seconds, would you like your session to last?");
        int time;
        while (!int.TryParse(Console.ReadLine(), out time))
        {
            Console.WriteLine("Please enter a valid whole number:");
        }
        return time;
    }

    private void outro(int actnum)
    {
        Console.WriteLine($"\n\nYou have just completed the {lrb_name[actnum]}, great job!");
        lrb_loading.Play(3); // Added running animation to closing buffer
    }
    
    public void alterAnimation()
    {
        Console.WriteLine("\nSelect a loading animation style:");
        Console.WriteLine("0. Classic Spinner ( | / - \\ )");
        Console.WriteLine("1. Growing Dots ( . .. ... )");
        Console.WriteLine("2. Progress Bar ( [==  ] )");
        Console.Write("\nYour choice (Press Enter for default): ");

        string input = Console.ReadLine();
        if (int.TryParse(input, out int choice))
        {
            lrb_loading.SetAnimationType(choice);
            Console.WriteLine("\nAnimation updated successfully!");
        }
        else
        {
            lrb_loading.SetAnimationType(0);
            Console.WriteLine("\nInvalid input. Defaulting to Classic Spinner.");
        }
        
        Thread.Sleep(1500);
    }
}

/*
Liam Barber
6/25/2026
*/
class Listing : Activity
{
    // Corrected typos in list string declarations
    private List<string> lrb_prompt = new List<string>() {
        "Who are the people that you appreciate?", 
        "What are personal strengths of yours?", 
        "Who are people that you have helped this week?", 
        "When have you felt inspired today?", 
        "Who are your personal heroes?"
    };
    private int _promptDisp;
    private List<string> lrb_response = new List<string>();
    private string lrb_promptChosen;
    private int lrb_time;

    public Listing(int times)
    {
        lrb_time = times; // Corrected: Kept unit matching seconds directly
    }

    public int DisplayPrompt(int? number = null)
    {
        if (number != null)
        {
            string _promptStr = lrb_prompt[number.Value]; // Corrected: Using .Value instead of int.Parse
            lrb_promptChosen = _promptStr;
            Console.WriteLine(_promptStr);
            return number.Value;
        }
        else
        {
            Random randomGenerator = new Random();
            int randomNumber = randomGenerator.Next(0, lrb_prompt.Count);
            string lrb_promptStr = lrb_prompt[randomNumber];
            lrb_promptChosen = lrb_promptStr;
            Console.WriteLine(lrb_promptStr);
            return randomNumber;
        }
    }

    public void GetInput()
    {
        string lrb_userResponse = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(lrb_userResponse))
        {
            lrb_response.Add(lrb_userResponse);
        }
    }

    public void DisplayAll()
    {
        Console.WriteLine($"\n--- {lrb_promptChosen} ---");
        Console.WriteLine($"You listed {lrb_response.Count} items:");
        foreach (string i in lrb_response)
        {
            Console.WriteLine($"- {i}");
        }
    }

    public void run() // Corrected: Renamed to lowercase 'run' to match class Activity switch-case block
    {
        Console.Clear();
        _promptDisp = DisplayPrompt();
        Console.WriteLine("Get ready to start listing things. Starting countdown...");
        
        for (int i = 5; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
        Console.WriteLine("\nGo!");

        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(lrb_time);

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            GetInput();
        }
        DisplayAll();
    }
}