namespace BackendAPI.BE.DAL.Entities;


public class Shift
{
    public string ShiftId { get; set; }      // PK

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public string StaffId { get; set; }   // FK

    public string Duty { get; set; }
    public string Note { get; set; }

    public Staff Staff { get; set; }      // navigation
}