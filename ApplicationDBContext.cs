using Preveld.Models;
using System.Data.Entity;

namespace Preveld
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() : base("DefaultConnection")
        { }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Wrap> Wraps { get; set; }

        public DbSet<Valve> Valves { get; set; }

        public DbSet<Membership> Memberships { get; set; }
    }
}