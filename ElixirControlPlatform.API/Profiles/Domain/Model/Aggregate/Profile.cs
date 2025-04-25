using ElixirControlPlatform.API.IAM.Domain.Model.Aggregates;
using ElixirControlPlatform.API.Profiles.Domain.Model.Commands;
using ElixirControlPlatform.API.Profiles.Domain.Model.ValueObjects;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Aggregate;

namespace ElixirControlPlatform.API.Profiles.Domain.Model.Aggregate;

public partial class Profile
{

    public Guid Id { get; }
    
    public ICollection<Batch> Batches { get; set; } // Relación uno a muchos con Batch
    
    public int UserId { get; set; }
    public User User { get; set; } // Relación uno a uno con User
    
    
    public PersonName Name { get; private set; }
    public EmailAddress Email { get; private set; }
    public StreetAddress Address { get; private set; }
    
    //=============== Information By Profiles ===============//
    public string FullName => Name.FullName;
    public string EmailAddress => Email.Address;
    public string StreetAddress => Address.FullAddress;
    
    public string CompanyName { get; private set; }
    public string PhoneNumber { get; private set; }
    public string RUC { get; private set; }
    
    
    public Profile()
    {
        Name = new PersonName();
        Email = new EmailAddress();
        CompanyName = string.Empty;
        PhoneNumber = string.Empty;
        RUC = string.Empty;
        Address = new StreetAddress();
    }
    
    public Profile(string firstName, string lastName, string email, string companyName, string phoneNumber, string ruc, string street, string number, string city, string country)
    {

        Name = new PersonName(firstName, lastName);
        Email = new EmailAddress(email);
        CompanyName = companyName;
        PhoneNumber = phoneNumber;
        RUC = ruc;
        Address = new StreetAddress(street, number, city, country);
    }

    public Profile(CreateProfileCommand command, int userId)
    {
        Name = new PersonName(command.FirstName, command.LastName);
        Email = new EmailAddress(command.Email);
        CompanyName = command.CompanyName;
        PhoneNumber = command.PhoneNumber;
        RUC = command.RUC;
        Address = new StreetAddress(command.Street, command.Number, command.City, command.Country);
        
        UserId = userId;
    }
    
    
    

    
}