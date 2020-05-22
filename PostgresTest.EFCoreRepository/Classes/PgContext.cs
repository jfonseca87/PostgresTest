using Microsoft.EntityFrameworkCore;
using PostgresTest.Domain.Models;

namespace PostgresTest.EFCoreRepository
{
    public class PgContext: DbContext
    {
        public PgContext(DbContextOptions<PgContext> options) : base(options)
        {}

        public DbSet<Posts> Posts { get; set; }
        public DbSet<Comments> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasOne(x => x.Post)
                    .WithMany(x => x.Comments)
                    .HasForeignKey(x => x.IdPost);
            });
        }
    }
}
