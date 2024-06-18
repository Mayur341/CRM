using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CRM.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Assuming a simple string role representation
        public string? Role { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}

