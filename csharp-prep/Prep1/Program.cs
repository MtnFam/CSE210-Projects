using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your name? ");
        string lrb_firstName = Console.ReadLine();
        
        Console.Write("What is your last name? ");
        string lrb_lastName = Console.ReadLine();
        Console.WriteLine();

        Console.Write($"Your name is {lrb_lastName}, {lrb_firstName} {lrb_lastName}.");
    }
}