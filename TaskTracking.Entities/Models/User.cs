
namespace TaskTracking.Entities.Models;

public class User
{
    public int Id{get ; set;} 
    public string FirstName { get; set; } = string.Empty ;
    public string LastName {get; set; }= string.Empty;
    public string Gender {get; set; }=string.Empty;
    public DateTime BirthDate {get; set;}
    public string NationalId {get; set;}= string.Empty;
    public string UserName {get; set;}= string.Empty;
    public string Password {get; set;}= string.Empty;


}