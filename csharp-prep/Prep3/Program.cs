using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("You will be guessing a number between 1-100");
        int lrb_guessNum = 0;

        Random lrb_magicNum = new Random();
        int lrb_num = lrb_magicNum.Next(1,100);

        while (lrb_guessNum != lrb_num)
        {
            Console.WriteLine("What is your guess? ");
            string lrb_guess = Console.ReadLine();
            lrb_guessNum = int.Parse(lrb_guess);

            if (lrb_num == lrb_guessNum)
            {
                Console.WriteLine("You guessed the right number!");
            }
            else if (lrb_num >= lrb_guessNum)
            {
                Console.WriteLine("The random number is higher");
            }
            else if (lrb_num <= lrb_guessNum)
            {
                Console.WriteLine("The random number is lower");
            }
        }

    }
}