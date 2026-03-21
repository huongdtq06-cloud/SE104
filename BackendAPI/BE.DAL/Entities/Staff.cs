/*class Staff {
    - baseSalary: Double
    - role: String
}*/
namespace BackendAPI.BE.DAL.Entities;
public class Staff 
{
    public string UserId { get; set; } = string.Empty;
    public double BaseSalary { get; set; }
    public string Role { get; set; } = string.Empty;

    public User User { get; set; } = new User();
    public ICollection<Shift> Shifts { get; set; }
    public ICollection<Note> Notes { get; set; }
    public ICollection<InfractionTicket> InfractionTickets { get; set; }
}
