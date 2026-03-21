namespace BackendAPI.BE.DAL.Entities;
// Product
// -------------------------
// productId (PK)
// productName
// category
// description
// stockQuantity
// defectiveQuantity
// damagedQuantity
// sellPrice

public class Product
{
    public string ProductId { get; set; }     

    public string Name { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public decimal SellPrice { get; set; }
    public decimal CostPrice { get; set; }
    public int StockQuantity { get; set; }
    public int DefectiveQuantity { get; set; }
    public int DamagedQuantity { get; set; }

    public int SupplierId { get; set; }     


    // Navigation properties
    public ICollection<DeliveryItem> DeliveryItems { get; set; }
    public ICollection<ReceiptItem> ReceiptItems { get; set; }
    public ICollection<DamageItem> DamageItems { get; set; }
    public ICollection<ProductSupplier> ProductSuppliers { get; set; }
}