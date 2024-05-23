using Microsoft.EntityFrameworkCore;
using TournamentAPI.Core.Entities;

namespace TournamentAPI.Data.Data
{
    public class TournamentApiContext:DbContext

    {
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Game> Games { get; set; }

        public TournamentApiContext(DbContextOptions<TournamentApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }

        }
}
