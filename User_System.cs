using System;
using System.Diagnostics.Metrics;
using System.Linq;

// Create base User class
/*
Create Student, ExchangeStudent, Ansatt subclasses
bruker = super class/parent class, child class/subclass for student, exchange student and ansatt
constructor for each class to initialize properties, and a method to display info about the user
polymorphism: create a list of User objects and add instances of Student, ExchangeStudent, and Ansatt to it. Iterate through the list and call the display method for each user to demonstrate polymorphism.
*/
public class User // super class for Brukere/ parent class
{
   public int ID { get; set; }
    public string Navn { get; set; }
    public string Epost { get; set; }

    public User(int id, string navn, string epost) // constructor 
    {
        ID = id;
        Navn = navn;
        Epost = epost;
    }

    public virtual void DisplayInfo() // virtual method to display user info
    {
        Console.WriteLine("ID:\t"+ ID);
        Console.WriteLine("Navn:\t"+ Navn);
        Console.WriteLine("Epost:\t"+ Epost);
    }


}

public class Student: User // subclass for student child for  user (parent)
{
    public List<string> Kurser { get; set; } 
    public Student(int id, string navn, string epost, List<string> kurser) 
        : base(id, navn, epost) // calling parent class which is user class
    {
        Kurser = kurser;
    }
    public override void DisplayInfo() // overriding the display info method to include courses
    {
        base.DisplayInfo(); // call the base class method to display common info
        Console.WriteLine("Kurser:\t"+ string.Join(",", Kurser)); // display courses
    }

    // 

}

public class ExchangeStudent: Student // exhange student child for (student class)
{

    public string Hjemland { get; set; }
    public string Land { get; set; }
    public string Periode { get; set; }

    public ExchangeStudent(int id, string navn, string epost,List<string> kurser,string hjemland, string land, string periode)
        : base(id, navn, epost,kurser) // same here calling parent class/ user/student
    {
        Hjemland = hjemland;
        Land = land;
        Periode = periode;
    }
    public override void DisplayInfo() // overriding the display info method to include exchange student specific info
    {
        base.DisplayInfo(); // call the base class method to display common info
        Console.WriteLine("Hjemland:\t"+ Hjemland); // display hjemland
        Console.WriteLine("Land:\t"+ Land); // display land
        Console.WriteLine("Periode:\t"+ Periode); // display periode fra og til
    }

}

public class Ansatt: User // ansatt child for user class/parent of ansatt
{
    public string Stilling { get; set; }
    public string Avdeling { get; set; }

    public Ansatt(int id, string navn, string epost, string stilling, string avdeling)
        :base(id, navn, epost)
    {
        Stilling = stilling;
        Avdeling = avdeling;
    }
    public override void DisplayInfo() // overriding the display info method to include employee specific info
    {
        base.DisplayInfo(); // call the base class method to display common info
        Console.WriteLine("Stilling:\t"+ Stilling); // display stilling
        Console.WriteLine("Avdeling:\t"+ Avdeling); // display avdeling
    }
}

