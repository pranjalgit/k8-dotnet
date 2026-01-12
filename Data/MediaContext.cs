using Media_MS.Models;
using Microsoft.EntityFrameworkCore;

namespace Media_MS.Data
{
    public partial class MediaContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MediaContext> _logger;

        // 1. Accept IConfiguration in the constructor
        //public MediaContext(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public MediaContext(DbContextOptions<MediaContext> options, IConfiguration configuration, ILogger<MediaContext> logger)
            : base(options)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string env = _configuration["ASPNETCORE_ENVIRONMENT"];
            _logger.LogInformation(env);

            string connectionString = _configuration["MySqlConnection"];
            _logger.LogInformation($"my sql connection string{connectionString}");
            optionsBuilder.UseMySQL(connectionString);

            // optionsBuilder.UseMySQL("server=localhost;port=3306;database=media;user=root;password=pranjal@123;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId).HasName("PRIMARY");

                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderId");

                entity.Property(e => e.OrderDescription)
                    .HasColumnType("text")
                    .HasColumnName("OrderDescription");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("text")
                    .HasColumnName("OrderDate");

                entity.Property(e => e.OrderStatus)
                    .HasMaxLength(100)
                    .HasColumnName("OrderStatus");

                entity.Property(e => e.Price)
                    .HasPrecision(10)
                    .HasColumnName("Price");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
