using System;

class Program
{
    //Display function
    static void DisplayMessage()
    {
        Console.Write("This is the welcome screen!");
    }
    //PromptUserName function
    static string GetName()
    {
        Console.Write("What is your last name?");
        string lrb_name = Console.ReadLine();

        return lrb_name;
    }
    //Prompt UserNumber function
    static int GetFavoriteNumber()
    {
        Console.WriteLine("What is your favorite number?");
        int lrb_number = int.Parse(Console.ReadLine());
        return lrb_number;
    }
    //PromptUserBirthYear
    static int GetBirthYear()
    {
        Console.WriteLine("What is your birth year?");
        int lrb_year = int.Parse(Console.ReadLine());
        return lrb_year;
    }
    static int calcAge(int lrb_birth_year)
    {
        int age = 2026 - lrb_birth_year;
        return age;
    }
    //SquareNumber
    static int calcSquare(int lrb_favNum)
    {
        int squared = (int)Math.Pow(lrb_favNum, 2); //https://learn.microsoft.com/en-us/dotnet/api/system.math.pow?view=net-10.0#system-math-pow(system-double-system-double)
        return squared;
    }

    static void DisplayResult()
    {
        string lrbName = GetName();
        int lrbNumber = GetFavoriteNumber();
        int lrbBirthYear = GetBirthYear();
        int lrbAge = calcAge(lrbBirthYear);
        int lrbSquare = calcSquare(lrbNumber);

        Console.WriteLine($"Bro/Sis {lrbName}, the square of your favorite number is {lrbSquare}\nBro/Sis {lrbName}, you will turn {lrbAge} this year.");


    } 
    //Display result
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep5 World!");
        DisplayMessage();
        DisplayResult();

    }
}