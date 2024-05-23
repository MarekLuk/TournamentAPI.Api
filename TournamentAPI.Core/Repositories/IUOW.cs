using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentAPI.Core.Repositories
{
    public interface IUOW
    {
        ITournamentRepository TournamentRepository { get; }
        IGameRepository GameRepository { get; }
        public Task CompleteAsync();

    }
}
