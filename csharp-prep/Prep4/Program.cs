using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("You will be creating a list as prompted");

        List<int> lrb_numberList = new List<int>();
        int lrb_entry = 1;

        // Prompt User for number inputs unless 0 is entered
        while (lrb_entry != 0)
        {
            Console.WriteLine("Enter a number and press enter to add to a list. Type 0 to move to calculations");
            string lrb_entrySTR = Console.ReadLine();
            lrb_entry = int.Parse(lrb_entrySTR);
            lrb_numberList.Add(lrb_entry);
        }

        //Get the sum and find the round
        int lrb_sum = 0;
        int lrb_max = 0;
        foreach (int lrb_number in lrb_numberList)
        {
            
            lrb_sum = lrb_number + lrb_sum;
            
            if (lrb_number > lrb_max)
            {
                lrb_max = lrb_number;
            }

        }


        //Return
        Console.WriteLine($"The sum is {lrb_sum}");
        Console.WriteLine($"The average is {lrb_sum / 2}");
        Console.WriteLine($"The highest number is {lrb_max}");
    }
}