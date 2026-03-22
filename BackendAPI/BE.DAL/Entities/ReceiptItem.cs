namespace BackendAPI.BE.DAL.Entities;

public class ReceiptItem
{
    public int ReceiptItemId { get; set; }
    public int NoteId { get; set; } // FK
    public int ProductId { get; set; } // FK
    public int Quantity { get; set; }

    // navigation
    public GoodsReceipt GoodReceipt { get; set; }

    public Product Product { get; set; }
}