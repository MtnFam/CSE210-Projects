using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        string libname = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "library.txt");
        int c = 1;
        while (c != 0)
        {
            
            c=menu(libname);
        }
    }
    static private int menu(string libname)
    {
        Console.Clear();
        int c;
        c=-1;
        Console.WriteLine("Choose an option or type 'quit' to exit:\n1) Learn random scripture\n2) Learn chosen scripture (must be in library)\n3) Save new Scripture");
        string input = Console.ReadLine();
        while (input != "quit" && (!int.TryParse(input, out c) || c < 0 || c > 3))
        {
            Console.WriteLine("Enter 1-3 or 'quit' to exit.");
            input = Console.ReadLine();
        };
        if (input == "quit")
        {
            c=0;
        }
        switch (c)
        {
            case 1://Learn random
                Random num = new Random();
                var lines = File.ReadAllLines(libname);
                int scrindex = num.Next(1, lines.Length + 1);
                Scripture scr = GetScript(libname, scrindex);
                showscripture(scr);
                break;

            case 2: //learn chosen
                Console.WriteLine("Enter a reference in the form John 1:5 or Moroini 7:47-48\nEnsure there are capitals and spaces where needed");
                string sref = Console.ReadLine();
                scr = GetScriptFRef(libname,sref); 
                showscripture(scr);
                Console.WriteLine("Not implemented yet");
                break;

            case 3://Save
                Console.WriteLine("Enter a reference in the form John 1:5 or Moroini 7:47-48\nEnsure there are capitals and spaces where needed");
                string nref = Console.ReadLine();
                Console.WriteLine("Enter the complete passage text");
                string npas = Console.ReadLine();
                File.AppendAllText(libname, $"{nref}|{npas}" + Environment.NewLine);
                break;
        }
            return c;
    }
    private static Scripture GetScript(string library, int index)
    {
        string line = File.ReadLines(library).Skip(index - 1).First();
        List<string> parts = line.Split('|', 2).ToList();
        return new Scripture(parts[0], parts[1]);
    }
    private static Scripture GetScriptFRef(string library, string sref)
    {
        int lineNumber = 0;

        foreach (string line in File.ReadLines(library))
        {
            lineNumber++;
            string[] parts = line.Split('|', 2);
            if (parts.Length > 0 &&
                parts[0].Equals(sref, StringComparison.OrdinalIgnoreCase))
            {
                return GetScript(library,lineNumber);
            }
        }
       throw new ArgumentException($"Scripture reference '{sref}' was not found.");
    }
    private static void showscripture(Scripture script)
    {
        string input = "";

    while (input != "quit")
    {
        Console.Clear();
        Console.WriteLine(script.ToString()); 
        Console.WriteLine("\nPress enter to continue or 'quit' to finish");
        input = Console.ReadLine();
        Random num = new Random();
        int val = num.Next(1, 5);
        if (input != "quit")
        {
            script.HideWords(val);
        }
    }
    return;
    }
}
public class Passage
{
    private List<Word> lrb_words;
    
    public Passage(string text)
    {
        lrb_words = new List<Word>();
        string[] splitWords = text.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string wordText in splitWords)
        {
            lrb_words.Add(new Word(wordText));
        }
    }

    public void HideWords(int countToHide)
    {
        Random random = new Random();
        List<Word> visibleWords = lrb_words.Where(w => !w.IsHidden()).ToList();

        if (visibleWords.Count == 0)
        {
            return;
        }

        int HiddenCount = 0;
        while (HiddenCount < countToHide && visibleWords.Count > 0)
        {
            int randomIndex = random.Next(visibleWords.Count);
            visibleWords[randomIndex].Hide();
            visibleWords.RemoveAt(randomIndex);
            HiddenCount++;
        }
    }

    public bool IsAllHidden()
    {
        return lrb_words.All(w => w.IsHidden());
    }

    public override string ToString()
    {
        return string.Join(" ", lrb_words.Select(w => w.ToString()));
    }
}

public class Reference
{
    private string lrb_text;

    public Reference(string text)
    {
        lrb_text = text;
    }

    public override string ToString()
    {
        return lrb_text;
    }
}


// Handles the Scripture object, keeping track of both the Reference and the Passage.
public class Scripture
{
    private Reference lrb_reference;
    private Passage lrb_passage;
    
    // Creates a new instance of the Scripture class using the reference and passage parameters
    public Scripture(Reference reference, Passage passage)
    {
        lrb_reference = reference;
        lrb_passage = passage;
    }
    public Scripture(string reference, string passage)
    {
        lrb_reference = new Reference(reference);
        lrb_passage = new Passage(passage);
    }

    // Calls the hideWords function from Passage (not directly manipulating the words).
    public void HideWords(int countToHide)
    {
        lrb_passage.HideWords(countToHide);
    }

    // Makes sure all of the words have been hidden
    public bool IsAllHidden()
    {
        return lrb_passage.IsAllHidden();
    }

    /// Formats the complete scripture for display in the console view.
    public override string ToString()
    {
        return $"{lrb_reference}\n{lrb_passage.ToString()}";
    }
}

public class Word
{
    private string lrb_text;
    private bool lrb_isHidden;
    public Word(string text)
    {
        lrb_text = text;
        lrb_isHidden = false;
    }
    
    public void Hide()
    {
        lrb_isHidden = true;
    }

    public bool IsHidden()
    {
        return lrb_isHidden;
    }

    public override string ToString()
    {
        if (lrb_isHidden)
        {
            return new string('_', lrb_text.Length);
        }
        else
        {
            return lrb_text;
        }
    }
}
public class Library
{
    
    public static string FindGitRepoRoot()
    {
    string folder = AppDomain.CurrentDomain.BaseDirectory;

    while (folder != null)
    {
        if (Directory.Exists(Path.Combine(folder, ".git")))
        {
            return folder;
        }

        folder = Directory.GetParent(folder)?.FullName;
    }

    throw new Exception("Git repository root not found.");
    }
}
