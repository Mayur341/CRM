using Microsoft.EntityFrameworkCore;

namespace CRM.Models
{
    public class CRMContext : DbContext
    {
        private readonly CustomSaveChangesInterceptor _saveChangesInterceptor;

        public CRMContext(DbContextOptions<CRMContext> options, CustomSaveChangesInterceptor saveChangesInterceptor)
            : base(options)
        {
            _saveChangesInterceptor = saveChangesInterceptor;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientStage> ClientStages { get; set; }
        public DbSet<Communication> Communications { get; set; }
        public DbSet<FinancialDetails> FinancialDetails { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<LeadStage> LeadStages { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<ClientActivity> ClientActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lead>()
                .HasOne(l => l.Client)
                .WithMany(c => c.Lead)
                .HasForeignKey(l => l.ClientID);

            modelBuilder.Entity<Communication>()
                .HasOne(c => c.Lead)
                .WithMany(l => l.Communications)
                .HasForeignKey(c => c.LeadID);

            modelBuilder.Entity<Deal>()
                .HasOne(d => d.Client)
                .WithOne(c => c.Deal)
                .HasForeignKey<Deal>(d => d.ClientID);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Deal)
                .WithMany(d => d.Transactions)
                .HasForeignKey(t => t.deal_id);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.ClientActivity)
                .WithOne(a => a.Client)
                .HasForeignKey(a => a.ClientID);

            modelBuilder.Entity<Client>()
                .HasOne(c => c.User)
                .WithMany(u => u.Clients)
                .HasForeignKey(c => c.UserId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_saveChangesInterceptor);
        }
    }
}
