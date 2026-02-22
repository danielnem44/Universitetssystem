using System;
using System.Collections.Generic;
using System.Linq;


public class Kurs
{
    public string Kode { get; set; }
    public string Navn { get; set; }
    public int Studiepoeng { get; set; }
    public int MaxCapacity { get; set; }
    public List<Student> PåmeldtStudenter { get; set; }

    public Kurs(string kode, string navn, int studiepoeng, int maxCapacity)
    {
        //properties 
        Kode = kode;
        Navn = navn;
        Studiepoeng = studiepoeng;
        MaxCapacity = maxCapacity;
        PåmeldtStudenter = new List<Student>();
    }
    // method to add a student to the course
    public void PåmeldtStudent(Student student)
    {
        if (PåmeldtStudenter.Contains(student))
        {
            Console.WriteLine($"{student.Navn} er allerede påmeldt i {Navn}.");
            return;
        }

        if (PåmeldtStudenter.Count < MaxCapacity)
        {
            PåmeldtStudenter.Add(student);
            Console.WriteLine($"{student.Navn} har blitt påmeldt {Navn}.");
        }
        else
        {
            Console.WriteLine($"Kan ikke melde {student.Navn} på {Navn}. Makskapasitet nådd.");
        }
    }
// method to remove a student from the course
    public void IkkePåmeldtStudent(Student student)
    {
        if (PåmeldtStudenter.Contains(student))
        {
            PåmeldtStudenter.Remove(student);
            Console.WriteLine($"{student.Navn} har blitt avmeldt fra {Navn}.");
        }
        else
        {
            Console.WriteLine($"{student.Navn} har ikke påmeldt i {Navn}.");
        }
    }

    public void DisplayKursInfo()
    {
        Console.WriteLine("Kode:\t" + Kode);
        Console.WriteLine("Navn:\t" + Navn);
        Console.WriteLine("Studiepoeng:\t" + Studiepoeng);
        Console.WriteLine("Maks Kapasitet:\t" + MaxCapacity);
        Console.WriteLine("Påmeldte Studenter:\t" + PåmeldtStudenter.Count);
        foreach (Student student in PåmeldtStudenter)
        {
            Console.WriteLine("\t" + student.Navn);
        }
    }
// search for a course with code or name
    public static Kurs? SøkeEtterKurs(List<Kurs> kurser, string kode, string navn)
    {
        return kurser.FirstOrDefault(c => c.Kode == kode || c.Navn.Contains(navn));
    }
}

