﻿using Microsoft.AspNetCore.Identity;

namespace CRM.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = " ";


        // New navigation property
        public virtual ICollection<Client> Clients { get; set; }


    }

}
