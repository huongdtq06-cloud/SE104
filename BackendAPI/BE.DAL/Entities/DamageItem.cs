namespace BackendAPI.BE.DAL.Entities;

// class DamageItem {
//     - damageItemId: String
//     - productId: String
//     - quantity: Integer
//     - reason: String
// }

public class DamageItem
{
    public string DamageItemId { get; set; }
    public string ProductId { get; set; }
    public string NoteID { get; set; } // FK
    public int Quantity { get; set; }   
    public string Reason { get; set; }

    // Navigation property
    public DamageNote DamageNote { get; set; }
    public Product Product { get; set; }
}