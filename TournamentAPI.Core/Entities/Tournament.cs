using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentAPI.Core.Entities
{
    public class Tournament
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Title required")]
        [StringLength(100, ErrorMessage ="Max 100 characters")]
        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public ICollection<Game>? Games { get; set; } 
    }
}
