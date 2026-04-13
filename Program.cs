using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    static UserManager userManager = new UserManager();
    static Bibliotek bibliotek = new Bibliotek();
    static List<Kurs> kurser = new List<Kurs>();

    // Main method to run the university system
    public static void Main()
    {
        Console.WriteLine("=== University System ===\n");

        LoadTestData();

        bool appRunning = true;
        while (appRunning)
        {
            User? loggedInUser = null;

            // Keep showing login/register until user is logged in or exits
            while (loggedInUser == null)
            {
                Console.WriteLine("\n1. Login");
                Console.WriteLine("2. Register ny bruker");
                Console.WriteLine("0. Avslutt");
                Console.Write("Velg: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        loggedInUser = HandleLogin();
                        break;
                    case "2":
                        HandleRegister();
                        break;
                    case "0":
                        appRunning = false;
                        break;
                    default:
                        Console.WriteLine("Ugyldig valg.\n");
                        break;
                }

                if (!appRunning) break;
            }

            if (!appRunning) break;

            // Show menu based on role
            if (loggedInUser!.Role == "Student" || loggedInUser.Role == "Exchange Student")
            {
                StudentMenu((Student)loggedInUser);
            }
            else if (loggedInUser.Role == "Lærer")
            {
                LærerMenu((Lærer)loggedInUser);
            }
            else if (loggedInUser.Role == "Bibliotekar")
            {
                BibliotekarMenu((Bibliotekar)loggedInUser);
            }
        }

        Console.WriteLine("Det var gødt å se deg!");
    }

    // ─── AUTH ────────────────────────────────────────────────────────────────

    private static User? HandleLogin()
    {
        Console.Write("Brukernavn: ");
        string username = Console.ReadLine() ?? "";
        Console.Write("Passord: ");
        string password = Console.ReadLine() ?? "";

        return userManager.Login(username, password);
    }

    private static void HandleRegister()
    {
        Console.Write("Brukernavn: ");
        string username = Console.ReadLine() ?? "";

        Console.Write("Passord (minst 6 tegn): ");
        string password = Console.ReadLine() ?? "";

        Console.Write("Navn: ");
        string navn = Console.ReadLine() ?? "";

        Console.Write("Epost: ");
        string epost = Console.ReadLine() ?? "";

        Console.WriteLine("Velg rolle:");
        Console.WriteLine("1. Student");
        Console.WriteLine("2. Exchange Student");
        Console.WriteLine("3. Lærer");
        Console.WriteLine("4. Bibliotekar");
        Console.Write("Velg: ");

        string roleChoice = Console.ReadLine() ?? "";
        string role = roleChoice switch
        {
            "1" => "Student",
            "2" => "Exchange Student",
            "3" => "Lærer",
            "4" => "Bibliotekar",
            _ => ""
        };

        if (string.IsNullOrEmpty(role))
        {
            Console.WriteLine("Ugyldig rollevalg.\n");
            return;
        }

        userManager.Register(username, password, navn, epost, role);
    }

    // STUDENT MENU 

    private static void StudentMenu(Student student)
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine($"\nStudent Menu — Hei {student.Navn}!");
            Console.WriteLine("1.  Søk på kurs");
            Console.WriteLine("2.  Meld meg på kurs");
            Console.WriteLine("3.  Meld meg av kurs");
            Console.WriteLine("4.  Se mine kurs");
            Console.WriteLine("5.  Se mine karakterer");
            Console.WriteLine("6.  Søk på bok");
            Console.WriteLine("7.  Lån bok");
            Console.WriteLine("8.  Returner bok");
            Console.WriteLine("9.  Se lånehistorikk");
            Console.WriteLine("0.  Logg ut");
            Console.Write("Velg: ");
// Input and execute action
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    SøkPåKurs(kurser);
                    break;
                case "2":
                    MeldStudentPåKurs(kurser, student);
                    break;
                case "3":
                    MeldStudentAvKurs(kurser, student);
                    break;
                case "4":
                    SeMinePåmeldinger(student);
                    break;
                case "5":
                    SeKarakterer(student);
                    break;
                case "6":
                    SøkPåBok(bibliotek);
                    break;
                case "7":
                    LånBok(bibliotek, student);
                    break;
                case "8":
                    ReturnerBok(bibliotek, student);
                    break;
                case "9":
                    VisLånhistorikk(bibliotek, student);
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Logged out. Ha det gødt!\n");
                    break;
                default:
                    Console.WriteLine("Ugyldig valg.\n");
                    break;
            }
        }
    }
// create a new course
        
    //LÆRER MENU

    private static void LærerMenu(Lærer lærer)
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine($"\nLærer Menu — Hallo {lærer.Navn}!");
            Console.WriteLine("1.  Opprett kurs");
            Console.WriteLine("2.  Søk på kurs");
            Console.WriteLine("3.  Se mine kurs");
            Console.WriteLine("4.  Sett karakter");
            Console.WriteLine("5.  Registrer pensum");
            Console.WriteLine("6.  Søk på bok");
            Console.WriteLine("7.  Lån bok");
            Console.WriteLine("8.  Returner bok");
            Console.WriteLine("0.  Logg ut");
            Console.Write("Velg: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    OpprettKurs(kurser, lærer);
                    break;
                case "2":
                    SøkPåKurs(kurser);
                    break;
                case "3":
                    VisLærersKurser(lærer);
                    break;
                case "4":
                    SettKarakter(lærer);
                    break;
                case "5":
                    RegistrerPensum(lærer);
                    break;
                case "6":
                    SøkPåBok(bibliotek);
                    break;
                case "7":
                    LånBok(bibliotek, lærer);
                    break;
                case "8":
                    ReturnerBok(bibliotek, lærer);
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Logged out. It was nice seeing you!\n");
                    break;
                default:
                    Console.WriteLine("Ugyldig valg.\n");
                    break;
            }
        }
    }

    //BIBLIOTEKAR MENU 

    private static void BibliotekarMenu(Bibliotekar ansatt)
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine($"\nBibliotek Menu — Hallo {ansatt.Navn}");
            Console.WriteLine("1.  Registrer bok");
            Console.WriteLine("2.  Søk på bok");
            Console.WriteLine("3.  Se aktive lån");
            Console.WriteLine("4.  Se lånhistorikk");
            Console.WriteLine("0.  Logg ut");
            Console.Write("Velg: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    RegistrerBok(bibliotek);
                    break;
                case "2":
                    SøkPåBok(bibliotek);
                    break;
                case "3":
                    SeAktiveLån(bibliotek);
                    break;
                case "4":
                    SeAlleLoån(bibliotek);
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Logged out. Fin dag videre!\n");
                    break;
                default:
                    Console.WriteLine("Ugyldig valg.\n");
                    break;
            }
        }
    }

    //TEST DATA 

    private static void LoadTestData()
    {
        kurser.Add(new Kurs("IS112", "Project Studying v1", 47, 3));
        kurser.Add(new Kurs("IS115", "Data Systems", 50, 2));

        bibliotek.LeggTilBook(new Book("Clean Code", "Robert Martin", "978-0287407987", 2008, 2));
        bibliotek.LeggTilBook(new Book("Design Patterns", "Gang of Four", "978-0201633610", 1994, 1));
    }



    private static void OpprettKurs(List<Kurs> kurser, Lærer lærer)
    {
        Console.Write("Kurs kode: ");
        string kode = (Console.ReadLine() ?? "").Trim();

        Console.Write("Kurs navn: ");
        string navn = (Console.ReadLine() ?? "").Trim();

        // Duplicate check — same kode OR same name is not allowed
        bool duplicate = kurser.Any(k => k.Kode == kode || k.Navn == navn);
        

        if (duplicate)
        {
            Console.WriteLine("Et kurs med samme kode eller navn finnes allerede!");
            return;
        }
        
        Console.Write("Studiepoeng:");
        int studiepoeng = int.Parse(Console.ReadLine());
        Console.Write("Maks kapasitet:");
        int maxCapacity = int.Parse(Console.ReadLine());


        Kurs newKurs = new Kurs(kode, navn, studiepoeng, maxCapacity);
        kurser.Add(newKurs);
        lærer.Fag.Add(navn);
        lærer.MineKurser.Add(newKurs);

        Console.WriteLine($"Kurs '{navn}' ({kode}) opprettet.");
    }

    private static void MeldStudentPåKurs(List<Kurs> kurser, Student student)
    {
        Console.Write("Kurs kode: ");
        string kursKode = (Console.ReadLine() ?? "").Trim();

        Kurs? kurs = kurser.FirstOrDefault(k => k.Kode.ToLower() == kursKode.ToLower());
        if (kurs == null)
        {
            Console.WriteLine("Kurs ikke funnet.");
            return;
        }

        kurs.PåmeldtStudent(student);
    }

    private static void MeldStudentAvKurs(List<Kurs> kurser, Student student)
    {
        // Show their enrolled courses first
        var studentKurser = kurser.Where(k => k.PåmeldtStudenter.Contains(student)).ToList();
        if (studentKurser.Count == 0)
        {
            Console.WriteLine("Du er ikke påmeldt noen kurs.");
            return;
        }

        Console.WriteLine("Dine kurs:");
        foreach (var k in studentKurser)
            Console.WriteLine($"  [{k.Kode}] {k.Navn}");

        Console.Write("Kurs kode å melde deg av: ");
        string kursKode = (Console.ReadLine() ?? "").Trim();

        Kurs? kurs = studentKurser.FirstOrDefault(k => k.Kode.ToLower() == kursKode.ToLower());
        if (kurs == null)
        {
            Console.WriteLine("Kurs ikke funnet i din påmeldingsliste.");
            return;
        }

        kurs.IkkePåmeldtStudent(student);
    }

    private static void SeMinePåmeldinger(Student student)
    {
        var studentKurser = kurser.Where(k => k.PåmeldtStudenter.Contains(student)).ToList();
        if (studentKurser.Count == 0)
        {
            Console.WriteLine("Du er ikke påmeldt noen kurs.");
            return;
        }

        Console.WriteLine("\nDine kurs");
        foreach (var kurs in studentKurser)
            Console.WriteLine($"  [{kurs.Kode}] {kurs.Navn} — {kurs.Studiepoeng} sp");
    }

    private static void SeKarakterer(Student student)
    {
        var påmeldteKurser = kurser.Where(k => k.PåmeldtStudenter.Contains(student)).ToList();
        if (påmeldteKurser.Count == 0)
        {
            Console.WriteLine("Du er ikke påmeldt noen kurs.");
            return;
        }

        Console.WriteLine("\nDine karakterer");
        bool found = false;
        foreach (var kurs in påmeldteKurser)
        {
            
            if (kurs.Karakterer.ContainsKey(student))
            {
                Console.WriteLine($"  {kurs.Navn} ({kurs.Kode}): {kurs.Karakterer[student]}");
                found = true;
            }
            else
            {
                Console.WriteLine($"  {kurs.Navn} ({kurs.Kode}): Ingen karakter satt ennå");
            }
        }

        if (!found)
            Console.WriteLine("Ingen karakterer er satt ennå.");
    }
// search for a course
    private static void SøkPåKurs(List<Kurs> kurser)
    {
        Console.Write("Søk etter kurs (kode eller navn): ");
        string searchTerm = (Console.ReadLine() ?? "").Trim();

        if (string.IsNullOrEmpty(searchTerm))
        {
            Console.WriteLine("Søkeord kan ikke være tomt.");
            return;
        }

        Kurs? foundKurs = Kurs.SøkeEtterKurs(kurser, searchTerm, searchTerm);
        if (foundKurs != null)
            foundKurs.DisplayKursInfo();
        else
            Console.WriteLine("Kurs ikke funnet.");
    }

    private static void VisLærersKurser(Lærer lærer)
    {
        if (lærer.MineKurser.Count == 0)
        {
            Console.WriteLine("Du har ikke opprettet noen kurs ennå.");
            return;
        }

        Console.WriteLine($"\n=== Mine kurs ({lærer.MineKurser.Count} stk) ===");
        foreach (var kurs in lærer.MineKurser)
        {
            Console.WriteLine();
            kurs.DisplayKursInfo();
        }
    }

    private static void SettKarakter(Lærer lærer)
    {
        if (lærer.MineKurser.Count == 0)
        {
            Console.WriteLine("Du har ingen kurs å sette karakterer i.");
            return;
        }

        Console.WriteLine("Velg kurs:");
        foreach (var k in lærer.MineKurser)
            Console.WriteLine($"  [{k.Kode}] {k.Navn} ({k.PåmeldtStudenter.Count} studenter)");

        Console.Write("Kurs kode: ");
        string kode = Console.ReadLine().Trim();

        Kurs? kurs = lærer.MineKurser.FirstOrDefault(k => k.Kode.ToLower() == kode.ToLower());
        if (kurs == null)
        {
            Console.WriteLine("Kurs ikke funnet .");
            return;
        }

        if (kurs.PåmeldtStudenter.Count == 0)
        {
            Console.WriteLine($"Ingen studenter er påmeldt '{kurs.Navn}'.");
            return;
        }

        Console.WriteLine("Påmeldte studenter:");
        foreach (var s in kurs.PåmeldtStudenter)
            Console.WriteLine($"  [ID: {s.ID}] {s.Navn}");

        Console.Write("Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int studentId))
        {
            Console.WriteLine("Ugyldig ID.");
            return;
        }

        Student? student = kurs.PåmeldtStudenter.FirstOrDefault(s => s.ID == studentId);
        if (student == null)
        {
            Console.WriteLine("Student ikke funnet.");
            return;
        }

        Console.Write($"Karakter for {student.Navn}(f.eks 1.0 -6.0): ");
        if (!double.TryParse(Console.ReadLine(), out double karakter))
        {
            Console.WriteLine("Ugyldig karakter.");
            return;
        }

        kurs.SettKarakter(student, karakter);
    }

    private static void RegistrerPensum(Lærer lærer)
    {
        if (lærer.MineKurser.Count == 0)
        {
            Console.WriteLine("Du har ingen kurs å registrere pensum for.");
            return;
        }

        Console.WriteLine("Velg kurs:");
        foreach (var k in lærer.MineKurser)
            Console.WriteLine($"  [{k.Kode}] {k.Navn}");

        Console.Write("Kurs kode: ");
        string kode = (Console.ReadLine() ?? "").Trim();

        Kurs? kurs = lærer.MineKurser.FirstOrDefault(k => k.Kode.ToLower() == kode.ToLower());
        if (kurs == null)
        {
            Console.WriteLine("Kurs ikke funnet.");
            return;
        }

        Console.Write("Bok tittel: ");
        string pensumTittel = (Console.ReadLine() ?? "").Trim();

        kurs.LeggTilPensum(pensumTittel);
    }

    // ─── BIBLIOTEK HELPERS ───────────────────────────────────────────────────

    private static void SøkPåBok(Bibliotek bibliotek)
    {
        Console.Write("Søk etter bok (tittel): ");
        string searchTerm = (Console.ReadLine() ?? "").Trim();

        if (string.IsNullOrEmpty(searchTerm))
        {
            Console.WriteLine("Søkeord kan ikke være tomt.");
            return;
        }

        Book? foundBook = bibliotek.SøkBook(searchTerm);
        if (foundBook != null)
            foundBook.DisplayInfo();
        else
            Console.WriteLine("Bok ikke funnet.");
    }

    private static void LånBok(Bibliotek bibliotek, User user)
    {
        Console.Write("Bok tittel: ");
        string bookTitle = (Console.ReadLine() ?? "").Trim();

        Book? book = bibliotek.SøkBook(bookTitle);
        if (book == null)
        {
            Console.WriteLine("Bok ikke funnet.");
            return;
        }

        bibliotek.LånBook(user, book);
    }

    private static void ReturnerBok(Bibliotek bibliotek, User user)
    {
        // Show user's active loans first so they know what they have
        var activeLoans = bibliotek.Loans
            .Where(l => l.Låntaker == user && l.Status == "Active")
            .ToList();

        if (activeLoans.Count == 0)
        {
            Console.WriteLine("Du har ingen aktive lån.");
            return;
        }

        Console.WriteLine("Dine aktive lån:");
        foreach (var l in activeLoans)
            Console.WriteLine($"  - {l.LåntBook.Tittel} (forfaller {l.Deadline.ToShortDateString()})");

        Console.Write("Bok tittel å returnere: ");
        string bookTitle = (Console.ReadLine() ?? "").Trim();

        Loan? loan = activeLoans.FirstOrDefault(l =>
            l.LåntBook.Tittel.ToLower().Contains(bookTitle.ToLower()));

        if (loan != null)
            bibliotek.ReturnerBook(loan);
        else
            Console.WriteLine("Aktiv lån med den tittelen ikke funnet.");
    }

    private static void RegistrerBok(Bibliotek bibliotek)
    {
        Console.Write("Bok tittel: ");
        string tittel = (Console.ReadLine() ?? "").Trim();

        Console.Write("Forfatter: ");
        string forfatter = (Console.ReadLine() ?? "").Trim();

        Console.Write("ISBN: ");
        string isbn = (Console.ReadLine() ?? "").Trim();

        Console.Write("Utgitt år: ");
        if (!int.TryParse(Console.ReadLine(), out int utgitt) || utgitt < 1000 || utgitt > DateTime.Now.Year)
        {
            Console.WriteLine("Ugyldig utgivelsesår.");
            return;
        }

        Console.Write("Antall kopier: ");
        if (!int.TryParse(Console.ReadLine(), out int copies) || copies <= 0)
        {
            Console.WriteLine("Ugyldig antall kopier. Må være et positivt tall.");
            return;
        }

        Book newBook = new Book(tittel, forfatter, isbn, utgitt, copies);
        bibliotek.LeggTilBook(newBook);
    }

    private static void SeAktiveLån(Bibliotek bibliotek)
    {
        var activeLoans = bibliotek.Loans.Where(l => l.Status == "Active").ToList();
        if (activeLoans.Count == 0)
        {
            Console.WriteLine("Ingen aktive lån.");
            return;
        }

        Console.WriteLine($"\n=== AKTIVE LÅN ({activeLoans.Count} stk) ===");
        foreach (var loan in activeLoans)
        {
            Console.WriteLine($"  Låntaker: {loan.Låntaker.Navn}");
            Console.WriteLine($"  Bok:      {loan.LåntBook.Tittel}");
            Console.WriteLine($"  Frist:    {loan.Deadline.ToShortDateString()}");
            Console.WriteLine();
        }
    }

    private static void SeAlleLoån(Bibliotek bibliotek)
    {
        if (bibliotek.Loans.Count == 0)
        {
            Console.WriteLine("Ingen lånehistorikk.");
            return;
        }

        Console.WriteLine($"\n=== LÅNHISTORIKK ({bibliotek.Loans.Count} oppføringer) ===");
        foreach (var loan in bibliotek.Loans)
        {
            Console.WriteLine($"  Låntaker: {loan.Låntaker.Navn}");
            Console.WriteLine($"  Bok:      {loan.LåntBook.Tittel}");
            Console.WriteLine($"  Lånt:     {loan.Lånedato.ToShortDateString()}");
            Console.WriteLine($"  Status:   {loan.Status}");
            if (loan.ReturDato.HasValue)
                Console.WriteLine($"  Returnert:{loan.ReturDato.Value.ToShortDateString()}");
            Console.WriteLine();
        }
    }

    private static void VisLånhistorikk(Bibliotek bibliotek, User user)
    {
        var userLoans = bibliotek.Loans.Where(l => l.Låntaker == user).ToList();
        if (userLoans.Count == 0)
        {
            Console.WriteLine("Du har ingen lånehistorikk.");
            return;
        }

        Console.WriteLine($"\n=== Din lånehistorikk ({userLoans.Count} lån) ===");
        foreach (var loan in userLoans)
        {
            Console.WriteLine($"  Bok:    {loan.LåntBook.Tittel}");
            Console.WriteLine($"  Lånt:   {loan.Lånedato.ToShortDateString()}");
            Console.WriteLine($"  Status: {loan.Status}");
            if (loan.ReturDato.HasValue)
                Console.WriteLine($"  Retur:  {loan.ReturDato.Value.ToShortDateString()}");
            Console.WriteLine();
        }
    }
}
