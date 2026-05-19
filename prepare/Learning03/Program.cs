using System;
using System.Reflection.Metadata.Ecma335;

class Program
{
    static void Main(string[] args)
    {
        Fraction fraction = new Fraction();
        for (int i = 0; i<=20; i++)
        {
            Random randomGenerator = new Random();
            int lrb_top = randomGenerator.Next(1, 100);
            int lrb_bottom = randomGenerator.Next(1, 100);
            fraction.SetBottom(lrb_bottom);
            fraction.SetTop(lrb_top);
            Console.WriteLine($"Fraction {i} string: {fraction.GetFractionString()} Number : {fraction.GetDecimalValue()}");

        }
    }
}

public class Fraction
{
    private int lrb_top;
    private int lrb_bottom;
    public Fraction()
    {
        lrb_top = 1;
        lrb_bottom = 1;
    }
    public Fraction(int top)
    {
        lrb_top = top;
        lrb_bottom = 1;
    }
    public Fraction(int top, int bottom)
    {
        lrb_top = top;
        lrb_bottom = bottom;
    }
    public int GetTop()
    {
        return lrb_top;
    }
    public void SetTop(int top)
    {
        lrb_top = top;
    }
    public int GetBottom()
    {
        return lrb_bottom;
    }
    public void SetBottom(int bottom)
    {
        lrb_bottom = bottom;
    }
    public string GetFractionString()
    {
        return lrb_top + "/" + lrb_bottom;
    }
    public double GetDecimalValue()
    {
        double top = lrb_top;
        double bottom = lrb_bottom;
        double answer = top / bottom;
        return answer;
    }
}