namespace CRM.Services
{
    //LOGIC BELOW 
    using System.Linq;
    using System.Threading.Tasks;
    using CRM.Models;
    using Microsoft.EntityFrameworkCore;

    public class UserSynchronizationService
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly CRMContext _crmContext;

        public UserSynchronizationService(ApplicationDbContext appDbContext, CRMContext crmContext)
        {
            _appDbContext = appDbContext;
            _crmContext = crmContext;
        }

        public async Task SynchronizeUsersAsync()
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
    }









}
