using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MessageBoard.Models
{
    public class MessageBoardContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<EndUser> EndUsers { get; set; }
        public DbSet<Group> Groups { get; set; }

        public MessageBoardContext(DbContextOptions options) : base(options){ }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}