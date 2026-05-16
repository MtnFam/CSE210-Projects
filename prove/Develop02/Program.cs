using System;
using System.IO;
using System.IO.Enumeration;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal(); //intiate a new journal object
        string state = "0"; //give a state value for a loop later
        Console.WriteLine("Welcome to the journal program!");
        while (state != "5")
        {
            menuDisplay();
            state = Console.ReadLine();
            Logic(state, journal);
        }
    }
    static void menuDisplay() // Display the entire menu options
    {
        Console.Write("\nThis program is designed to help you journal better\nIt will give you a random prompt every time\nWhat would you like to do?\n1. Display Journal\n2. Write a new entry\n3. Save Journal\n4. Load Journal\n5. Quit\nWhat would you like to do? ");
    }
    static void Logic(string choice, Journal journal) //logic for the choices
    {
        if (choice == "1")
        {
            journal.Display();
        }
        else if (choice == "2")
        {
            journal.Write();
        }
        else if (choice == "3")
        {
            journal.Save();
        }
        else if (choice == "4")
        {
            journal.Load();
        }
        else if (choice == "5")
        {
            Console.WriteLine("Thank you for using the software!");
        }
        else
        {
            Console.WriteLine("That value is not accepted");
        }
    }
}

public class Entry
{
    public string lrb_date;
    public string lrb_prompt;
    public string lrb_response;

    public string getPrompt() //method to get a prompt for journaling
    {
        List<string> lrb_prompts = new List<string>()
        {"If you could make one change today, what would it be?", "Who did you see today?", "What made today good?", "What did you learn about Christ today?", "Who were you able to serve today?"};

        Random lrb_randomIndex = new Random();
        int lrb_index = lrb_randomIndex.Next(0,lrb_prompts.Count);

        return lrb_prompts[lrb_index];
    }
    public string getDate() //method to get a date
    {
        DateTime lrb_theCurrentTime = DateTime.Now;
        return lrb_theCurrentTime.ToShortDateString();
    }
    public string format() //method to format the information for display
    {
        return lrb_date + " - " + lrb_prompt + "\n" + lrb_response;
    }
}

public class Journal
{
    public List<Entry> lrb_entriesList = new List<Entry>(); // Create a list for the entry objects
    public void Display() // method to display the entries in the list
    {
        foreach (Entry lrb_entry in lrb_entriesList)
        {
            Console.WriteLine("\n" + lrb_entry.format());
        }
        Console.WriteLine("\nPress ENTER to return");
        Console.ReadLine();
    }
    public void Write() // method to create a new entry
    {
        Entry lrb_entry = new Entry();
        lrb_entry.lrb_date = lrb_entry.getDate();
        lrb_entry.lrb_prompt = lrb_entry.getPrompt();
        Console.WriteLine($"{lrb_entry.lrb_date} - Prompt: {lrb_entry.lrb_prompt}\n");
        lrb_entry.lrb_response = Console.ReadLine();
        lrb_entriesList.Add(lrb_entry);
    }
    public void Save() //method to save to a journal
    {

        Console.WriteLine("\nList of already made files"); // make it a little prettier
        string lrb_directory = AppDomain.CurrentDomain.BaseDirectory; // https://stackoverflow.com/questions/674857/should-i-use-appdomain-currentdomain-basedirectory-or-system-environment-current
        foreach (var file in Directory.EnumerateFiles(lrb_directory, searchPattern: "*.txt*"))
        {
            Console.WriteLine(Path.GetFileName(file)); // https://stackoverflow.com/questions/13003555/c-sharp-how-to-extract-the-file-name-and-extension-from-a-path
        }
        try
        {
            Console.WriteLine("/nWhat would you like to name the file? (.txt) ");
            string lrb_filename = Console.ReadLine();

            using (StreamWriter outputFile = new StreamWriter(lrb_filename))
            {

            foreach (Entry lrb_entry in lrb_entriesList)
            {
                outputFile.WriteLine($"{lrb_entry.lrb_date}|{lrb_entry.lrb_prompt}|{lrb_entry.lrb_response}"); // Saves in a CSV style format
            }
            }
        }
        catch
        {
            Console.WriteLine("Insufficient Format");
        }
    }
    public void Load() // method to load from journal
    {
        lrb_entriesList.Clear();

        Console.WriteLine("\nList of available files"); // make it a little prettier
        string lrb_directory = AppDomain.CurrentDomain.BaseDirectory; // https://stackoverflow.com/questions/674857/should-i-use-appdomain-currentdomain-basedirectory-or-system-environment-current
        foreach (var file in Directory.EnumerateFiles(lrb_directory, searchPattern: "*.txt*"))
        {
            Console.WriteLine(Path.GetFileName(file)); // https://stackoverflow.com/questions/13003555/c-sharp-how-to-extract-the-file-name-and-extension-from-a-path
        }
        try 
        {
            Console.WriteLine("\nWhich file would you like to open? ");
            string lrb_filename = Console.ReadLine();
            string[] lrb_lines = System.IO.File.ReadAllLines(lrb_filename);
            
            foreach (string line in lrb_lines)
            {
                string[] col = line.Split('|'); //https://stackoverflow.com/questions/43807152/read-individual-values-separated-by-commas-per-line-c-sharp
                
                Entry lrb_entry = new Entry();
                lrb_entry.lrb_date = col[0];
                lrb_entry.lrb_prompt = col[1];
                lrb_entry.lrb_response = col[2];
                
                lrb_entriesList.Add(lrb_entry);
            }
        }
        catch
        {
            Console.WriteLine("File does not exist");
        }
    }
}









/*
 string filename = "a_new_file.txt";
        //using (StreamWriter outputFile = new StreamWriter(filename))
        //{
            //outputFile.WriteLine("Why are there fences around graveyards?\nIts because people are dying to get in!");
        //}
        ReadJoke(filename);
    }
    static void ReadJoke(string filename)
    {
        string[] lines = System.IO.File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            Console.WriteLine(line);
        }*/