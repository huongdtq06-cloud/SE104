namespace BackendAPI.BE.DAL.Entities;

public class ReceiptItem
{
    public string ReceiptItemId { get; set; }
    public string NoteID { get; set; } // FK
    public string ProductId { get; set; } // FK
    public int Quantity { get; set; }

    // navigation
    public GoodsReceipt GoodReceipt { get; set; }

    public Product Product { get; set; }
}