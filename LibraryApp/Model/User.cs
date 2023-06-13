using System;
using System.Collections.ObjectModel;


namespace LibraryApp.Model;

public class User
{
    public string FirstName { get;  set; }
    public string LastName { get;  set; }
    public string Address { get;  set; }
    public Guid UserID { get; set; }
    public string FullName => $"{LastName}, {FirstName}";
    public string LoanCard { get; set; }

    public User()
    {
        
    }
    public User(string firstName, string lastName, string address)
    {
        UserID = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        LoanCard = "Ingen";
    }

    

    public void IssueLoanCard()
    {
        LoanCard = $"Gyldig til {App.GetTodaysDate(1)}";
    }

    public void RevokeLoanCard()
    {
        LoanCard = "Ingen";
    }
    
}