using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPI.Core.Entities;
using TournamentAPI.Core.Repositories;
using TournamentAPI.Data.Data;

namespace TournamentAPI.Data.Repositories
{
    public class GameRepository : IGameRepository

    {
        private readonly TournamentApiContext _context;

        public GameRepository(TournamentApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Set<Game>().ToListAsync();
        }

        public async Task<Game> GetAsync(int id)
        {          

            var game = await _context.Set<Game>().FindAsync(id);
            if (game == null)
            {

                throw new KeyNotFoundException($"Game id {id} was not found.");
            }
            return game;
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Set<Game>().AnyAsync(g => g.Id == id);
        }

        public void Add(Game game)
        {
            _context.Set<Game>().Add(game);
        }

        public void Update(Game game)
        {
            _context.Set<Game>().Update(game);
        }

        public void Remove(Game game)
        {
            _context.Set<Game>().Remove(game);
        }
    }
}

    

