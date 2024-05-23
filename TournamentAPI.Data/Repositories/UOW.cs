using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPI.Core.Repositories;
using TournamentAPI.Data.Data;

namespace TournamentAPI.Data.Repositories
{
    public class UOW: IUOW
    {
        private readonly TournamentApiContext _context;

        public ITournamentRepository TournamentRepository { get; private set; }
        public IGameRepository GameRepository { get; private set; }

        public UOW(TournamentApiContext context)
        {
            _context = context;
            TournamentRepository = new TournamentRepository(context);
            GameRepository = new GameRepository(context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
