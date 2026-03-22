namespace BackendAPI.BE.DAL.Entities;

// class DamageItem {
//     - damageItemId: String
//     - productId: String
//     - quantity: Integer
//     - reason: String
// }

public class DamageItem
{
    public int DamageItemId { get; set; }
    public int ProductId { get; set; }
    public int NoteId { get; set; } // FK
    public int Quantity { get; set; }   
    public string Reason { get; set; }

    // Navigation property
    public DamageNote DamageNote { get; set; }
    public Product Product { get; set; }
}