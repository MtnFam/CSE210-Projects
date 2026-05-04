using System;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job();
        Job job2 = new Job();
        Resume resume1 = new Resume();

        job1._jobTitle = "Software Engineer";
        job1._companyName = "Micron";
        job1._endYear = "2026";
        job1._startYear = "2010";
        job1.Display();

        job2._jobTitle = "Manager";
        job2._companyName = "Apple";
        job2._endYear = "2023";
        job2._startYear = "2022";
        job2.Display();

        resume1._fullName = "Liam Barber";
        resume1._jobs.Add(job1);
        resume1._jobs.Add(job2);
        resume1.Display();
    }
}

public class Job
{
    public string _companyName;
    public string _jobTitle;
    public string _startYear;
    public string _endYear;

    public void Display()
    {
        Console.WriteLine($"{_jobTitle} ({_companyName}) {_startYear}-{_endYear}");
    }
}

public class Resume
{
    public string _fullName;
    public List<Job> _jobs = new List<Job>();

    public void Display()
    {
        Console.WriteLine($"Name: {_fullName}");
        Console.WriteLine("Jobs: ");

        foreach (Job job in _jobs)
        {
            job.Display();
        }
    }
}