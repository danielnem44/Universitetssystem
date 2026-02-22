using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    // Main method to run the university system
    public static void Main()
    {
        Bibliotek bibliotek = new Bibliotek();
        List<Kurs> kurser = new List<Kurs>();
        List<User> users = new List<User>();

        users.Add(new Student(14689, "Daniel Nemeye", "daniel@uia.no", new List<string>()));
        users.Add(new Student(13648, "Mika Deo", "mika@uia.no", new List<string>()));
        users.Add(new Ansatt(27903, "Paulo De Lacrus", "paulo@uia.no", "Lecturer", "IT"));
        
        kurser.Add(new Kurs("IS112", "Programmering 1", 47, 3));
        kurser.Add(new Kurs("IS115", "Programmering 2", 50, 2));
        kurser.Add(new Kurs("TEO1218", "Practicing the Way", 30, 6));

        
        bibliotek.LeggTilBook(new Book("Clean Code", "Robert Martin", "978-0132350884", 2008, 2));
        bibliotek.LeggTilBook(new Book("Design Patterns", "Gang of Four", "978-0201633610", 1994, 1));
        bibliotek.LeggTilBook(new Book("Practicing the Way", "John Mark Comer", "978-0310113774", 2024,1));

         Console.WriteLine("Test data loaded\n");
        
        


        bool running = true;
        while (running)
        {
            Console.WriteLine("\nUniversity System");
            Console.WriteLine("[1] Opprett kurs");
            Console.WriteLine("[2] Meld student til kurs");
            Console.WriteLine("[3] Print kurs og deltakere");
            Console.WriteLine("[4] Søk på kurs");
            Console.WriteLine("[5] Søk på bok");
            Console.WriteLine("[6] Lån bok");
            Console.WriteLine("[7] Returner bok");
            Console.WriteLine("[8] Registrer bok");
            Console.WriteLine("[9] Vis lånhistorikk");
            Console.WriteLine("[0] Avslutt");
            Console.Write("Velg: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    OpprettKurs(kurser);
                    break;
                case "2":
                    MeldStudentTilKurs(kurser, users);
                    break;
                case "3":
                    PrintKursOgDeltakere(kurser);
                    break;
                case "4":
                    SøkPåKurs(kurser);
                    break;
                case "5":
                    SøkPåBok(bibliotek);
                    break;
                case "6":
                    LånBok(bibliotek, users);
                    break;
                case "7":
                    ReturnerBok(bibliotek);
                    break;
                case "8":
                    RegistrerBok(bibliotek);
                    break;
                case "9":
                    VisLånhistorikk(bibliotek, users);
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static void OpprettKurs(List<Kurs> kurser)
    {
        Console.Write("Kurs kode: ");
        string kode = Console.ReadLine();
        Console.Write("Kurs navn: ");
        string navn = Console.ReadLine();
        Console.Write("Studiepoeng: ");
        int studiepoeng = int.Parse(Console.ReadLine());
        Console.Write("Maks kapasitet: ");
        int maxCapacity = int.Parse(Console.ReadLine());

        Kurs newKurs = new Kurs(kode, navn, studiepoeng, maxCapacity);
        kurser.Add(newKurs);
        Console.WriteLine($"Kurs '{navn}' opprettet.");
    }

    private static void MeldStudentTilKurs(List<Kurs> kurser, List<User> users)
    {
        Console.Write("Student ID: ");
        int studentId = int.Parse(Console.ReadLine());
        Student student = users.OfType<Student>().FirstOrDefault(s => s.ID == studentId);
        if (student == null)
        {
            Console.WriteLine("Student ikke funnet.");
            return;
        }

        Console.Write("Kurs kode: ");
        string kursKode = Console.ReadLine();
        Kurs kurs = kurser.FirstOrDefault(k => k.Kode == kursKode);
        if (kurs == null)
        {
            Console.WriteLine("Kurs ikke funnet.");
            return;
        }

        kurs.PåmeldtStudent(student);
    }

    private static void PrintKursOgDeltakere(List<Kurs> kurser)
    {
        if (kurser.Count == 0)
        {
            Console.WriteLine("Ingen kurs registrert.");
            return;
        }
        
        foreach (Kurs kurs in kurser)
        {
            kurs.DisplayKursInfo();
            Console.WriteLine("\n");
        }
    }

    private static void SøkPåKurs(List<Kurs> kurser)
    {
        Console.Write("Søk etter kurs (kode eller navn): ");
        string searchTerm = Console.ReadLine();
        Kurs foundKurs = Kurs.SøkeEtterKurs(kurser, searchTerm, searchTerm);
        if (foundKurs != null)
        {
            foundKurs.DisplayKursInfo();
        }
        else
        {
            Console.WriteLine("Kurs ikke funnet.");
        }
    }

    private static void SøkPåBok(Bibliotek bibliotek)
    {
        Console.Write("Søk etter bok (tittel): ");
        string searchTerm = Console.ReadLine();
        Book foundBook = bibliotek.SøkBook(searchTerm);
        if (foundBook != null)
        {
            foundBook.DisplayInfo();
        }
        else
        {
            Console.WriteLine("Bok ikke funnet.");
        }
    }

    private static void LånBok(Bibliotek bibliotek, List<User> users)
    {
        Console.Write("User ID: ");
        int userId = int.Parse(Console.ReadLine());
        User user = users.FirstOrDefault(u => u.ID == userId);
        if (user == null)
        {
            Console.WriteLine("User ikke funnet.");
            return;
        }

        Console.Write("Bok tittel: ");
        string bookTitle = Console.ReadLine();
        Book book = bibliotek.SøkBook(bookTitle);
        if (book == null)
        {
            Console.WriteLine("Bok ikke funnet.");
            return;
        }
        
        bibliotek.LånBook(user, book);
    }

    private static void ReturnerBok(Bibliotek bibliotek)
    {
        // Find loan by user ID and book title
        Console.Write("User ID: ");
        int userId = int.Parse(Console.ReadLine());
        
        Console.Write("Bok tittel: ");
        string bookTitle = Console.ReadLine();
        
        Loan loan = bibliotek.Loans.FirstOrDefault(l => l.Låntaker.ID == userId && l.LåntBook.Tittel == bookTitle && l.Status == "Active");
        
        if (loan != null)
        {
            bibliotek.ReturnerBook(loan);
        }
        else
        {
            Console.WriteLine("Lån ikke funnet.");
        }
    }

    private static void RegistrerBok(Bibliotek bibliotek)
    {
        Console.Write("Bok tittel: ");
        string tittel = Console.ReadLine();
        Console.Write("Forfatter: ");
        string forfatter = Console.ReadLine();
        Console.Write("ISBN: ");
        string isbn = Console.ReadLine();
        Console.Write("Utgitt år: ");
        int utgitt = int.Parse(Console.ReadLine());
        Console.Write("Antall kopier: ");
        int copies = int.Parse(Console.ReadLine());

        Book newBook = new Book(tittel, forfatter, isbn, utgitt, copies);
        bibliotek.LeggTilBook(newBook);
    }
    // New method to display loan history
    private static void VisLånhistorikk(Bibliotek bibliotek, List<User> users)
{
    Console.Write("User ID: ");
    int userId = int.Parse(Console.ReadLine());
    User user = users.FirstOrDefault(u => u.ID == userId);
    
    if (user == null)
    {
        Console.WriteLine("User ikke funnet.");
        return;
    }
    
    // Get all loans for this user
    var userLoans = bibliotek.Loans.Where(l => l.Låntaker == user).ToList();
    
    if (userLoans.Count == 0)
    {
        Console.WriteLine($"{user.Navn} har ingen lånehistorikk.");
        return;
    }
    
    Console.WriteLine($"\nLånehistorikk for {user.Navn}");
    
    // Show active loans
    var activeLoans = userLoans.Where(l => l.Status == "Active").ToList();
    if (activeLoans.Count > 0)
    {
        Console.WriteLine("\nAKTIVE LÅN ");
        foreach (var loan in activeLoans)
        {
            Console.WriteLine($"\nBok: {loan.LåntBook.Tittel}");
            Console.WriteLine($"Lånt: {loan.Lånedato.ToShortDateString()}");
            Console.WriteLine($"Frist: {loan.Deadline.ToShortDateString()}");
            Console.WriteLine($"Status: {loan.Status}");
        }
    }
    else
    {
        Console.WriteLine("\nKeine aktive lån.");
    }
    
    // Show returned loans
    var returnedLoans = userLoans.Where(l => l.Status == "Returned").ToList();
    if (returnedLoans.Count > 0)
    {
        Console.WriteLine("\nRETURNERT HISTORIKK");
        foreach (var loan in returnedLoans)
        {
            Console.WriteLine($"\nBok: {loan.LåntBook.Tittel}");
            Console.WriteLine($"Lånt: {loan.Lånedato.ToShortDateString()}");
            Console.WriteLine($"Returnert: {loan.ReturDato?.ToShortDateString()}");
            Console.WriteLine($"Status: {loan.Status}");
        }
    }
}
}