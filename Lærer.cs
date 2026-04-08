using System;
using System.Collections.Generic;

public class Lærer : User
{
    public List<string> Fag { get; set; }           // subject names (kept for display)
    public List<Kurs> MineKurser { get; set; }       // actual Kurs objects they created/teach

    public Lærer(int id, string navn, string epost, string username, string password, string role)
        : base(id, navn, epost, username, password, role)
    {
        Fag = new List<string>();
        MineKurser = new List<Kurs>();
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Type:\tLærer");
        Console.WriteLine("Fag:\t" + string.Join(", ", Fag));
    }
}
