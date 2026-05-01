using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Input your grade: ");
        string lrb_grade = Console.ReadLine();
        int lrb_numGrade = int.Parse(lrb_grade);
        string lrb_letterGrade = "";

        if (lrb_numGrade >= 90)
        {
            lrb_letterGrade = "A";
        }
        else if (lrb_numGrade >= 80)
        {
            lrb_letterGrade = "B";
        }
        else if (lrb_numGrade >= 70)
        {
            lrb_letterGrade = "C";
        }
        else if (lrb_numGrade >= 60)
        {
            lrb_letterGrade = "D";
        }
        else
        {
            lrb_letterGrade = "F";
        }

        Console.WriteLine($"Your grade was {lrb_grade} which is: {lrb_letterGrade}");
    }
}