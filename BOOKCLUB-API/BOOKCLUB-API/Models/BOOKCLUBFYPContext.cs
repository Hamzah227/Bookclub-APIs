using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BOOKCLUB_API.Models
{
    public partial class BOOKCLUBFYPContext : DbContext
    {
        public BOOKCLUBFYPContext()
        {
        }

        public BOOKCLUBFYPContext(DbContextOptions<BOOKCLUBFYPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Bid> Bids { get; set; }
        public virtual DbSet<BidAttribute> BidAttributes { get; set; }
        public virtual DbSet<BidAttributeBook> BidAttributeBooks { get; set; }
        public virtual DbSet<BookDatum> BookData { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<ProfileAttribute> ProfileAttributes { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestAttribute> RequestAttributes { get; set; }
        public virtual DbSet<RequestDelivery> RequestDeliveries { get; set; }
        public virtual DbSet<RequestDeliveryAttribute> RequestDeliveryAttributes { get; set; }
        public virtual DbSet<Rider> Riders { get; set; }
        public virtual DbSet<RiderDeliveryAttribute> RiderDeliveryAttributes { get; set; }
        public virtual DbSet<RiderMaster> RiderMasters { get; set; }
        public virtual DbSet<SourceAndDestination> SourceAndDestinations { get; set; }
        public virtual DbSet<WorkOrder> WorkOrders { get; set; }
        public virtual DbSet<WorkOrderAttribute> WorkOrderAttributes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Password=123;Persist Security Info=True;User ID=sa;Initial Catalog=BOOKCLUB-FYP;Data Source=LENOVO-IDEAPAD-");
                optionsBuilder.UseSqlServer("Password=sheheryar$$032;Persist Security Info=True;User ID=sheheryar321;Initial Catalog=bookClub;Data Source=66.165.248.146;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK_User");

                entity.ToTable("AppUser");

                entity.Property(e => e.Uid)
                    .HasMaxLength(50)
                    .HasColumnName("UID");

                entity.Property(e => e.AuthUserId)
                    .HasMaxLength(50)
                    .HasColumnName("auth_user_id");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(20);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PaymentProviderCustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("payment_provider_customer_id");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.Property(e => e.ProfileImageUrl)
                    .HasMaxLength(100)
                    .HasColumnName("profileImageUrl");

                entity.Property(e => e.Provider)
                    .HasMaxLength(50)
                    .HasColumnName("provider");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(50)
                    .HasColumnName("_roleId");
            });

            modelBuilder.Entity<Bid>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Uid });

                entity.ToTable("bid");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("_id");

                entity.Property(e => e.Uid)
                    .HasMaxLength(50)
                    .HasColumnName("UID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(50)
                    .HasColumnName("comments");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(50)
                    .HasColumnName("requestID");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.Property(e => e.StatusProgress).HasColumnName("statusProgress");
            });

            modelBuilder.Entity<BidAttribute>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("bidAttributes");

                entity.Property(e => e.BidId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("_bidId");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name_");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("_userId");

                entity.Property(e => e.Value)
                    .HasMaxLength(50)
                    .HasColumnName("value_");
            });

            modelBuilder.Entity<BidAttributeBook>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Author).HasMaxLength(50);

                entity.Property(e => e.BidId).HasColumnName("BidID");

                entity.Property(e => e.BookTitle).HasMaxLength(100);

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.Isbn)
                    .HasMaxLength(25)
                    .HasColumnName("ISBN");
            });

            modelBuilder.Entity<BookDatum>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Author)
                    .HasMaxLength(50)
                    .HasColumnName("author");

                entity.Property(e => e.BookTitle).HasColumnName("book_title");

                entity.Property(e => e.Categories)
                    .HasMaxLength(50)
                    .HasColumnName("categories");

                entity.Property(e => e.ImageUrl).HasColumnName("image_url");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(50)
                    .HasColumnName("isbn");

                entity.Property(e => e.ReqId)
                    .HasMaxLength(50)
                    .HasColumnName("req_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.Property(e => e.TypeId).HasColumnName("type_id");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Destination>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Destination1).HasColumnName("Destination");

                entity.Property(e => e.HasReached).HasColumnName("hasReached");
            });

            modelBuilder.Entity<ProfileAttribute>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.UserId })
                    .HasName("PK_ProfileAttributes_1");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("UserID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name_");

                entity.Property(e => e.Value)
                    .HasMaxLength(100)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => new { e.ReqId, e.Uid });

                entity.ToTable("Request");

                entity.Property(e => e.ReqId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ReqID");

                entity.Property(e => e.Uid)
                    .HasMaxLength(50)
                    .HasColumnName("UID");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("createdOn");

                entity.Property(e => e.ExpiresOn).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsArchived).HasColumnName("isArchived");

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.Property(e => e.TypeId)
                    .HasMaxLength(10)
                    .HasColumnName("typeId");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updateAt");
            });

            modelBuilder.Entity<RequestAttribute>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name_");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(50)
                    .HasColumnName("_requestId");

                entity.Property(e => e.Value).HasColumnName("value_");
            });

            modelBuilder.Entity<RequestDelivery>(entity =>
            {
                entity.ToTable("RequestDelivery");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("timestamp");
            });

            modelBuilder.Entity<RequestDeliveryAttribute>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BidId).HasColumnName("BidID");

                entity.Property(e => e.DropOffAddress).HasMaxLength(100);

                entity.Property(e => e.PickupAddress).HasMaxLength(100);

                entity.Property(e => e.RequestDeliveryId).HasColumnName("RequestDeliveryID");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Rider>(entity =>
            {
                entity.ToTable("Rider");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(20);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PaymentProviderCustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("payment_provider_customer_id");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.Property(e => e.ProfileImageUrl)
                    .HasMaxLength(100)
                    .HasColumnName("profileImageUrl");

                entity.Property(e => e.Provider)
                    .HasMaxLength(50)
                    .HasColumnName("provider");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(50)
                    .HasColumnName("_roleId");

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<RiderDeliveryAttribute>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Latitude).HasColumnType("decimal(12, 9)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(12, 9)");

                entity.Property(e => e.RiderMasterId).HasColumnName("RiderMasterID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(10);
            });

            modelBuilder.Entity<RiderMaster>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.RequestDeliveryId });

                entity.ToTable("RiderMaster");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.RiderLatitude).HasColumnType("decimal(12, 9)");

                entity.Property(e => e.RiderLongitude).HasColumnType("decimal(12, 9)");
            });

            modelBuilder.Entity<SourceAndDestination>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<WorkOrder>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.BidId });

                entity.ToTable("WorkOrder");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("timestamp");
            });

            modelBuilder.Entity<WorkOrderAttribute>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.WorkOrderId });

                entity.ToTable("WorkOrder_Attributes");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.WorkOrderId).HasColumnName("WorkOrder_Id");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("Name_");

                entity.Property(e => e.Value).HasColumnName("Value_");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
