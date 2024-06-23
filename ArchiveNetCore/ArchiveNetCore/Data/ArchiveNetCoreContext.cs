using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ArchiveNetCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ArchiveNetCore.Data
{
    public class ArchiveNetCoreContext : IdentityDbContext<ApplicationUser>
    {
        public ArchiveNetCoreContext (DbContextOptions<ArchiveNetCoreContext> options)
            : base(options)
        {
        }

        public DbSet<ArchiveNetCore.Models.Entrepots> Entrepots { get; set; } = default!;

        public DbSet<ArchiveNetCore.Models.Articles> Articles { get; set; } = default!;
    }
}
