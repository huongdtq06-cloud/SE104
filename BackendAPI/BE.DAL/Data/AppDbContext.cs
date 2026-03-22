using BackendAPI.BE.DAL.Entities; 
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.BE.DAL.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TestItem> TestItems { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<InfractionTicket> InfractionTickets { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductSupplier> ProductSuppliers { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<GoodsReceipt> GoodsReceipts { get; set; }
    public DbSet<DeliveryNote> deliveryNotes { get; set; }
    public DbSet<DamageNote> damageNotes { get; set; }
    public DbSet<DamageItem> damageItems { get; set; }
    public DbSet<ReceiptItem> receiptItems { get; set; }
    public DbSet<DeliveryItem> deliveryItems { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



    // =======================
    // ProductSupplier (N-N)
    // =======================
    modelBuilder.Entity<ProductSupplier>()
        .HasKey(ps => new { ps.ProductId, ps.SupplierId });

    modelBuilder.Entity<ProductSupplier>()
        .HasOne(ps => ps.Product)
        .WithMany(p => p.ProductSuppliers)
        .HasForeignKey(ps => ps.ProductId);

    modelBuilder.Entity<ProductSupplier>()
        .HasOne(ps => ps.Supplier)
        .WithMany(s => s.ProductSuppliers)
        .HasForeignKey(ps => ps.SupplierId);

    // =======================
    // Note inheritance (TPH)
    // =======================
    modelBuilder.Entity<Note>()
        .HasDiscriminator<string>("NoteType")
        .HasValue<GoodsReceipt>("GoodsReceipt")
        .HasValue<DeliveryNote>("DeliveryNote")
        .HasValue<DamageNote>("DamageNote");

    // =======================
    // Note - User (1-n)
    // =======================
    modelBuilder.Entity<Note>()
        .HasOne(n => n.User)
        .WithMany()
        .HasForeignKey(n => n.UserId)
        .OnDelete(DeleteBehavior.Restrict);

    // =======================
    // GoodsReceipt - ReceiptItem
    // =======================
    modelBuilder.Entity<ReceiptItem>()
        .HasOne(re => re.GoodReceipt)
        .WithMany(gr => gr.ReceiptItems)
        .HasForeignKey(re => re.NoteId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<ReceiptItem>()
        .HasOne(ri => ri.Product)
        .WithMany(p => p.ReceiptItems)
        .HasForeignKey(ri => ri.ProductId);

    // =======================
    // DeliveryNote - DeliveryItem
    // =======================
    modelBuilder.Entity<DeliveryItem>()
        .HasOne(de => de.DeliveryNote)
        .WithMany(dn => dn.DeliveryItems)
        .HasForeignKey(de => de.NoteId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<DeliveryItem>()
        .HasOne(dei=> dei.Product)
        .WithMany(p => p.DeliveryItems)
        .HasForeignKey(di => di.ProductId);

    // =======================
    // DamageNote - DamageItem
    // =======================
    modelBuilder.Entity<DamageItem>()
        .HasOne(da => da.DamageNote)
        .WithMany( dn => dn.DamageItems)
        .HasForeignKey(da => da.NoteId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<DamageItem>()
        .HasOne(di=> di.Product)
        .WithMany(p=> p.DamageItems)
        .HasForeignKey(di => di.ProductId);

    // =======================
    // Shift - User
    // =======================
    modelBuilder.Entity<Shift>()
        .HasOne(s=> s.User)
        .WithMany(u=> u.Shifts)
        .HasForeignKey(s => s.UserId);

    // =======================
    // InfractionTicket - User
    // =======================
    modelBuilder.Entity<InfractionTicket>()
    .HasOne(i => i.User)                   
    .WithMany(u => u.InfractionTickets)    
    .HasForeignKey(i => i.UserId);
    }
}
