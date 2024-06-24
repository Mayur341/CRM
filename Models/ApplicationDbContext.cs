using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRM.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly CustomSaveChangesInterceptor _saveChangesInterceptor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, CustomSaveChangesInterceptor saveChangesInterceptor)
            : base(options)
        {
            _saveChangesInterceptor = saveChangesInterceptor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_saveChangesInterceptor);
        }
    }
}
