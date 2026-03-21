namespace BackendAPI.BE.DAL.Entities;
public class Note
{
    public string NoteId { get; set; }
    public string StaffId { get; set; } // FK
    public DateTime Date { get; set; }
    public string type { get; set; }

    // navigation
    public Staff Staff { get; set; }
}