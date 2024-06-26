namespace CRM.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using CRM.Models;
    using Microsoft.EntityFrameworkCore;

    public class UserSynchronizationServiceTop
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly CRMContext _crmContext;
        private readonly object _lock = new object(); // For thread safety
        public bool IsSynchronizing { get; private set; }

        public UserSynchronizationServiceTop(ApplicationDbContext appDbContext, CRMContext crmContext)
        {
            _appDbContext = appDbContext;
            _crmContext = crmContext;
            IsSynchronizing = false;
        }

        public async Task SynchronizeUsersAsync()
        {
            lock (_lock) // Ensure thread safety
            {
                if (IsSynchronizing)
                {
                    return;
                }
                IsSynchronizing = true;
            }

            try
            {
                var aspNetUsers = await _appDbContext.Users.ToListAsync();
                var crmUsers = await _crmContext.Users.ToListAsync();

                // Add or update users in CRMContext
                foreach (var aspNetUser in aspNetUsers)
                {
                    var crmUser = crmUsers.FirstOrDefault(u => u.Id == aspNetUser.Id);
                    if (crmUser == null)
                    {
                        // User does not exist in CRMContext, add it
                        _crmContext.Users.Add(new User
                        {
                            Id = aspNetUser.Id,
                            FirstName = aspNetUser.FirstName,
                            LastName = aspNetUser.LastName,
                            UserName = aspNetUser.UserName,
                            Email = aspNetUser.Email,
                            // Map other fields as necessary
                        });
                    }
                    else
                    {
                        // User exists, update it
                        crmUser.UserName = aspNetUser.UserName;
                        crmUser.Email = aspNetUser.Email;
                        // Update other fields as necessary
                    }
                }

                // Remove users from CRMContext that are not in ASPNetUsers
                foreach (var crmUser in crmUsers)
                {
                    if (!aspNetUsers.Any(u => u.Id == crmUser.Id))
                    {
                        _crmContext.Users.Remove(crmUser);
                    }
                }

                await _crmContext.SaveChangesAsync();
            }
            finally
            {
                lock (_lock)
                {
                    IsSynchronizing = false;
                }
            }
        }
    }
}
