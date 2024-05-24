using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPI.Core.Entities;

namespace TournamentAPI.Core.Dto.TournamentDto
{
    public class TournamentDto
    {
        public int Id { get; set; }

      
        public string? Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate => StartDate.AddMonths(33);
        public ICollection<Game>? Games { get; set; }
    }
}
