using Microsoft.EntityFrameworkCore;

namespace ContainerPlanPrediction.Data
{
    public class ShipPlanningContext : DbContext
    {
        public ShipPlanningContext() : base()
        {
            this.Database.EnsureCreated();
        }

        public ShipPlanningContext(DbContextOptions<ShipPlanningContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContainerPositionEntity>().ToTable("ContainerPositionEntity");
            modelBuilder.Entity<ContainerPositionEntity>().HasKey(x => new { x.Scenario, x.Bay, x.Row });
            modelBuilder.Entity<ContainerPositionEntity>().Property(x => x.RouteNumber).HasMaxLength(2).IsRequired();
            modelBuilder.Entity<ContainerPositionEntity>().Property(x => x.ContainerContentType).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<ContainerPositionEntity>().Property(x => x.ContainerSize).HasMaxLength(10).IsRequired();
            modelBuilder.Entity<ContainerPositionEntity>().Property(x => x.ContainerType).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<ContainerPositionEntity>().Property(x => x.Bay).HasMaxLength(2).IsRequired();
            modelBuilder.Entity<ContainerPositionEntity>().Property(x => x.Row).HasMaxLength(2).IsRequired();
            modelBuilder.Entity<ContainerPositionEntity>().Property(x => x.Scenario);
        }


        public DbSet<ContainerPositionEntity> ContainerPositions { get; set; }
    }
}
