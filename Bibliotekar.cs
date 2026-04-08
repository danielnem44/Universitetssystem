using System;

public class Bibliotekar: User
{
    public string Avdeling { get; set; } // department they work in

    public Bibliotekar(int id, string navn, string epost, string username, string password, string roke)
        : base(id, navn, epost, username, password, roke)
    {
        Avdeling = "Bibliotek";
    }
    public override void DisplayInfo() // overriding the display info method to include avdeling
    {
        base.DisplayInfo(); // call the base class method to display common info
        Console.WriteLine("Type:\tBibliotekar");
        Console.WriteLine("Avdeling:\t"+ Avdeling); // display avdeling
    }
}