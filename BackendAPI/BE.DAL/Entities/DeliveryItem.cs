namespace BackendAPI.BE.DAL.Entities;

public class DeliveryItem
{
    public string DeliveryItemId { get; set; }
    public string NoteID { get; set; } // FK
    public string ProductId { get; set; } // FK
    public int Quantity { get; set; }

    // navigation
    public DeliveryNote DeliveryNote { get; set; }

    public Product Product { get; set; }
}