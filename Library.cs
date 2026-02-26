using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;



// class for bibliotek system, include, user management, loan management and book management
public class Bibliotek
{
    // properties for bibliotel system
    public List<Book> Books { get; set; }
    public List<User> Users { get; set; }
    public List<Loan> Loans { get; set; }
    public List<VenteListe> VenteLister { get; set; }
// constructor for bibliotek class to initialize lists
    public Bibliotek()
    {
        Books = new List<Book>();
        Users = new List<User>();
        Loans = new List<Loan>();
        VenteLister = new List<VenteListe>();
    }

    public void LeggTilBook(Book book)
    {
        Books.Add(book);
        Console.WriteLine($"Boken '{book.Tittel}' har blitt lagt til i biblioteket.");
    }
    // method to add a user to the library

    public void LeggTilUser(User user)
    {
        Users.Add(user);
        Console.WriteLine($"Brukeren '{user.Navn}' har blitt lagt til i biblioteket.");
    }
// method to search for a book by title
    public Book SøkBook(string tittel)
    {
       return Books.FirstOrDefault(b => b.Tittel.Contains(tittel));
    }
    
    public void LånBook(User user, Book book)
    {
         // Check if user already has active loan for this book
      Loan existingLoan = Loans.FirstOrDefault(l => l.Låntaker == user && l.LåntBook == book && l.Status == "Active");
        if (existingLoan != null)        {
            Console.WriteLine($"{user.Navn} har allerede lånt '{book.Tittel}'.");
            return;
        }
        // Check if book is available
        if (book.AvailableCopies > 0)
        {
            DateTime lånedato = DateTime.Now;
            DateTime deadline = lånedato.AddDays(30); // 30 day loan period
            Loan newLoan = new Loan(user, book, lånedato, deadline, null, "Active");
            Loans.Add(newLoan);
            book.AvailableCopies--;

            
            Console.WriteLine($"{user.Navn} har lånt '{book.Tittel}'. Forfallsdato: {deadline.ToShortDateString()}.");
        }
        else
        // If book is not available, add user to waitlist
        {
            Console.WriteLine($"Ingen kopier av '{book.Tittel}' er tilgjengelige. Legger {user.Navn} til i ventelisten.");

            // Check if a waitlist already exists for this book
            VenteListe existingWaitlist = VenteLister.FirstOrDefault(v => v.Ventebok == book);
            if (existingWaitlist != null) // If waitlist exists, add user to it
            {
                existingWaitlist =  new VenteListe(book);
                VenteLister.Add(existingWaitlist);
            }
            
            existingWaitlist.LeggTilBruker(user);

        }
        
     
       
    }
    // method to return a book
    public void ReturnerBook(Loan loan)
    {
        if (loan.Status == "Active")
        {
            loan.Status = "Returned";
            loan.ReturDato = DateTime.Now;
            loan.LåntBook.AvailableCopies++;
            Console.WriteLine($"{loan.Låntaker.Navn} har returnert {loan.LåntBook.Tittel}.");

        }
       
    }
    // method to display loan history for a user
    public void VisLånhistorikk(User user)
    {
        var userLoans = Loans.Where(l => l.Låntaker == user).ToList();
        if (userLoans.Count == 0)
        {
            Console.WriteLine($"{user.Navn} ikke lånhistorikk.");
            return;
        }
// display loan history

        Console.WriteLine($"Lånehistorikk for {user.Navn}:");
        foreach (var loan in userLoans)
        {
            Console.WriteLine($"\n Book: {loan.LåntBook.Tittel}");
            Console.WriteLine($" Lånt: {loan.Lånedato.ToShortDateString()}");
            Console.WriteLine($" Forfall: {loan.Deadline.ToShortDateString()}");
            Console.WriteLine($" Status: {loan.Status}");
            if (loan.ReturDato.HasValue)
            {
                Console.WriteLine($" Returnert: {loan.ReturDato.Value.ToShortDateString()}");
            }
        }
    }
}
public class Book 
{
    // properties for book 
    public string Tittel { get; set; }
    public string Forfatter { get; set; }
    public string ISBN { get; set; }
    public int Utgitt { get; set; }
    public int AvailableCopies { get; set; }
 // constructor for book class
    public Book(string tittel, string forfatter, string isbn, int utgitt, int availablecopies)
    {
        Tittel = tittel;
        Forfatter = forfatter;
        ISBN = isbn;
        Utgitt = utgitt;
        AvailableCopies = availablecopies;
    }
// method to display book info
    public void DisplayInfo()
    {
        Console.WriteLine("Tittle:\n"+Tittel);
        Console.WriteLine("Forfatter:\n"+ Forfatter);
        Console.WriteLine("ISBN:\n"+ ISBN);
        Console.WriteLine("Utgitt:\n"+ Utgitt);
        Console.WriteLine("Available copies:\n"+ AvailableCopies);
    }
}
// class for loan and waitlist
public class Loan
{
    public User Låntaker { get; set; }
    public Book LåntBook { get; set; }
    public DateTime Lånedato { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime? ReturDato { get; set; }
    public string Status { get; set; }
    // constructor for loan class
    public Loan(User låntaker, Book låntBook, DateTime lånedato, DateTime deadline, DateTime? returDato, string status)
    {
        Låntaker = låntaker;
        LåntBook = låntBook;
        Lånedato = lånedato;
        Deadline = deadline;
        ReturDato = null;
        Status = "Active";
    }
// method to display loan info
    public void DisplayInfo()
    {
        Console.WriteLine("Låntaker:\n"+ Låntaker.Navn);
        Console.WriteLine("Book:\n"+ LåntBook.Tittel);
        Console.WriteLine("Lånedato:\n"+ Lånedato.ToShortDateString());
        Console.WriteLine("Forfallsdato:\n"+ Deadline.ToShortDateString());
        Console.WriteLine("Retur dato:\n"+ (ReturDato.HasValue ? ReturDato.Value.ToShortDateString() : "Ikke returnert"));
        Console.WriteLine("Status:\n"+ Status);
    }
}
public class VenteListe
{
    public Book Ventebok { get; set; }
    public Queue<User> Brukere { get; set; }

    public VenteListe(Book ventebok)
    {
        Ventebok = ventebok;
        Brukere = new Queue<User>();
    }

    public void LeggTilBruker(User bruker)
    {
        // Check if user is already in the waitlist
        if (!Brukere.Contains(bruker))
        {
            Brukere.Enqueue(bruker);
            Console.WriteLine($"{bruker.Navn} har blitt lagt til i ventelisten for {Ventebok.Tittel}.");
        }
        // If user is already in the waitlist, do not add again
        else
        {
            Console.WriteLine($"{bruker.Navn} er allerede i ventelisten for {Ventebok.Tittel}.");
        }
    }

    public void FjernBruker()
    {
        // Remove the first user in the waitlist and display a message
        if (Brukere.Count > 0)
        {
            User fjernetBruker = Brukere.Dequeue();
            Console.WriteLine($"{fjernetBruker.Navn} har blitt fjernet fra ventelisten for {Ventebok.Tittel}.");
        }
        // If the waitlist is empty, display a message
        else
        {
            Console.WriteLine($"Ventelisten for {Ventebok.Tittel} er tom.");
        }
    }
}
