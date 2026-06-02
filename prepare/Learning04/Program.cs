using System;
using System.ComponentModel.Design;

class Program
{
    static void Main(string[] args)
    {
       Console.WriteLine("Hello Learning04 World!");

    Assignment uno = new Assignment("Samuel Jackson", "Mathematics");
    Console.WriteLine(uno.GetSummary());

    MathAssignment math = new MathAssignment("Samuel Jackson", "Mathematics", "7.3", "12-25");
    Console.WriteLine(math.GetSummary());
    Console.WriteLine(math.GetHomeWorkList());
    
    WritingAssignment writing = new WritingAssignment("Samuel Jackson", "English", "Civil War Essay");
    Console.WriteLine(writing.GetSummary());
    Console.WriteLine(writing.GetHomeWorkList());
    }
}
public class Assignment
{
    protected string lrb_studentName;
    protected string lrb_topic;
    public Assignment(string name, string topic)
    {
        lrb_studentName = name;
        lrb_topic = topic;
    }
    public string GetSummary()
    {
        string summary = lrb_studentName + " - " + lrb_topic;
        return summary;
    }
}
public class MathAssignment : Assignment
{
    private string lrb_textbookSection;
    private string lrb_problems;
    public MathAssignment (string name, string topic, string selection, string problems) : base(name, topic)
    {
        lrb_textbookSection = selection;
        lrb_problems = problems;
    }
    public string GetHomeWorkList()
    {
        string homework = $"{lrb_textbookSection} Problems {lrb_problems}";
        return homework;
    }

}
public class WritingAssignment : Assignment
{
    private string lrb_title;
    public WritingAssignment(string name, string topic, string title) : base(name, topic)
    {
        lrb_title = title;
    }
   public string GetHomeWorkList()
    {
        string homework = $"{lrb_title}";
        return homework;
    }
}