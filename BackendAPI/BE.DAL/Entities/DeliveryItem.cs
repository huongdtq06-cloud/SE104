namespace BackendAPI.BE.DAL.Entities;

public class DeliveryItem
{
    public int DeliveryItemId { get; set; }
    public int NoteId { get; set; } // FK
    public int ProductId { get; set; } // FK
    public int Quantity { get; set; }

    // navigation
    public DeliveryNote DeliveryNote { get; set; }

    public Product Product { get; set; }
}