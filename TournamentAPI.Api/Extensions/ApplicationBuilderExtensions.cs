using Bogus;
using Microsoft.EntityFrameworkCore;
using TournamentAPI.Core.Entities;
using TournamentAPI.Data.Data;

namespace TournamentAPI.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
    
        public static async Task SeedDataAsync(this WebApplication myApp)
        {
            var scope = myApp.Services.CreateScope();
            var services = scope.ServiceProvider;

     
            var myDbContext = services.GetRequiredService<TournamentApiContext>();

            
            if (!await myDbContext.Tournaments.AnyAsync())
            {
                var fakerForTournament = new Faker<Tournament>();
                fakerForTournament.RuleFor(t => t.Title, f => f.Address.City());
                fakerForTournament.RuleFor(t => t.StartDate, f => f.Date.Future());

                var listOfTournaments = new List<Tournament>();

                for (int i = 0; i < 6; i++)
                {
                    var tournament = fakerForTournament.Generate();

                    var gamesForTournament = new Faker<Game>()
                        .RuleFor(g => g.Title, f => f.Lorem.Sentence(2))
                        .RuleFor(g => g.Time, f => f.Date.Future())
                        .Generate(5); 

                    tournament.Games = gamesForTournament;

                    listOfTournaments.Add(tournament);
                }

        
                await myDbContext.Tournaments.AddRangeAsync(listOfTournaments);
                await myDbContext.SaveChangesAsync();
            }
        }

        public static async Task<IEnumerable<Tournament>> GetAllAsync(this TournamentApiContext myDbContext)
        {
          
            var allTournaments = await myDbContext.Tournaments.Include(t => t.Games).ToListAsync();
            return allTournaments;
        }
    }
}
