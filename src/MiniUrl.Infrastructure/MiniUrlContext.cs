using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniUrl.ApplicationCore;
using MiniUrl.ApplicationCore.Entities;

namespace MiniUrl.Infrastructure
{
    public class MiniUrlContext : IdentityDbContext
    {
        DbSet<MiniUrlItem> MiniUrlItems { get; set; }
        DbSet<MiniUrlOfUser> MiniUrlOfUsers { get; set; }

        public MiniUrlContext(DbContextOptions<MiniUrlContext> options) : base(options) { }
    }
}