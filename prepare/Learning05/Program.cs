using System;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        Shape s1 = new Square(5, "Red");
        Shape s2 = new Rectangle(4, 6, "Blue");
        Shape s3 = new Circle(3, "Green");

        Console.WriteLine(s1.GetArea()); // 25
        Console.WriteLine(s2.GetArea()); // 24
        Console.WriteLine(s3.GetArea()); // 28.274
    }
}
public abstract class Shape
{
    // Attributes
    protected string lrb_color;

    // Methods
    public string GetColor()
    {
        return lrb_color;
    }
    public abstract double GetArea();

}
public class Square : Shape
{
    private double lrb_side;
    public Square(double side, string color)
    {
        lrb_side = side;
        lrb_color = color;
    }
    public override double GetArea()
    {
        double lrb_area = lrb_side * lrb_side;
        return lrb_area;
    }
}
public class Rectangle : Shape
{
    private double lrb_length;
    private double lrb_width;
    public Rectangle(double length, double width, string color)
    {
        lrb_length = length;
        lrb_width = width;
        lrb_color = color;
    }
    public override double GetArea()
    {
        double lrb_area = lrb_length * lrb_width;
        return lrb_area;
    }
}
public class Circle : Shape
{
    private double lrb_radius;
    public Circle(double radius, string color)
    {
        lrb_radius = radius;
        lrb_color = color;
    }
    public override double GetArea()
    {
        double pi = Math.PI;
        double lrb_area = lrb_radius * lrb_radius * pi;
        return lrb_area;
    }
}