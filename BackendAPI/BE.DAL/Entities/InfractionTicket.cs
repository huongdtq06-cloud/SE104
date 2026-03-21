namespace BackendAPI.BE.DAL.Entities;
public class InfractionTicket
{
    public string InfractionTicketId { get; set; }

    public string StaffId { get; set; } // FK

    public DateTime Date { get; set; }
    public string Description { get; set; }
    public decimal Penalty { get; set; }

    // navigation
    public Staff Staff { get; set; }
}