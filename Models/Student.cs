namespace StudentHub.Models;

public class Student
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public List<string> Courses { get; set; }
    public double Score { get; set; }

    public Student()
    {
    }
}