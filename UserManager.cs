using System;
using System.Collections.Generic;
using System.Linq;

public class UserManager
{
    private List<User> allUsers=new List<User>();
     // list to hold all users
     public UserManager()
    {
        // preload some users for testing
        allUsers.Add(new Student(14689, "Daniel Nemeye","daniel@uia.no", "daniel123", "180100dan", "Student", new List<string>()));
        allUsers.Add(new ExchangeStudent(12345, "Maria Garcia", "maria@uia.no", new List<string>(), "Spania", "Norge", "2023-2024", "maria123", "180100mar", "ExchangeStudent"));
        allUsers.Add(new Lærer (27903,"Paulo De Lacrus", "paulo@uia.no","paulo234","7856pu", "Lærer" ) );
        allUsers.Add(new Bibliotekar(50001, "Lisa Anderson","lisa@uia.no", "lisa567", "7867lis", "Bibliotekar" ));
    }

    //chech if user exists
    public bool UserExists(string username)
    {
        return allUsers.FirstOrDefault(u => u.Username == username) != null; // check if user with given username exists in the list
    }

    // login return use if user exists and password matches
    public User Login(string username, string password)
    {
        try
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Brukernavn og passord kan ikke være tomme.");
                return null;
            }
            User user =allUsers.FirstOrDefault(u => u.Username == username && u.Password == password); // find user by username and password
            if (user != null)
            {
                Console.WriteLine($"\n Velkommen, {user.Navn}!Du er logget inn som {user.Role}.");
                return user;
            }
            else
            {
                Console.WriteLine("\n Feil brukernavn eller passord!\n");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Feil under login: {ex.Message}\n");
            return null;
        }
    }

    // method to add user to the list
    public bool Register(string username, string password, string navn, string epost, string role)
    {
        try
        {
            //validate input
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(navn) || string.IsNullOrEmpty(epost))
            {
                Console.WriteLine("Alle felt må fylles ut.");
                return false;
            }

            //check if username already exists
            if(UserExists(username))
            {
                Console.WriteLine("Brukernavn finnes allerede!\n.");
                return false;
            }

            // validate password strength
            if(password.Length < 6)
            {
                Console.WriteLine("Passord må være minst 6 tegn!\n");
                return false;
            }

            // validate email 
            if(!epost.Contains("@"))
            {
                Console.WriteLine("Ugyldig epost format!\n");
                return false;
            }

            // generate new user ID
            int newID = allUsers.Count>0 ? allUsers.Max(u => u.ID) + 1 : 1; 
            // create new user based on role
            User newUser = null;

            switch (role.ToLower())
            {
                case "student":
                    newUser = new Student(newID, navn, epost, username, password, role, new List<string>());
                    break;
                case "exchange student":
                    newUser = new ExchangeStudent(newID, navn, epost, new List<string>(), "Hjemland", "Land", "Periode", username, password, role);
                    break;
                case "lærer":
                    newUser = new Lærer(newID, navn, epost, username, password, role);
                    break;
                case "bibliotekar":
                    newUser = new Bibliotekar(newID, navn, epost, username, password, role);    
                    break;
                default:
                    Console.WriteLine("Ugyldig rolle! Velg mellom Student, Exchange Student, Lærer eller Bibliotekar.\n");
                    return false;
            }
            allUsers.Add(newUser); // add new user to the list
            Console.WriteLine($"\n Registrering vellykket! Velkommen, {navn}.\n");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Feil under registrering: {ex.Message}\n");
            return false;
        }
    }

    //get all users
    public List<User> GetAllUsers()
    {
        return allUsers;
    }

    // get user by ID
    public User GetUserByID(int id)
    {
        return allUsers.FirstOrDefault(u => u.ID == id);
    }

} 