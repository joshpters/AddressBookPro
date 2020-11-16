using System;
using System.Collections.Generic;
using System.Text;
using AddressBookPro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AddressBookPro.Data
{
    public class ApplicationDbContext : IdentityDbContext<ABPUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Label> Labels { get; set; }
    }
}
