﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentAPI.Core.Entities
{
    public class Tournament
    {
        public int Id { get; set; } 

        public string? Title { get; set; }

        public DateTime StartDate { get; set; }

        //public ICollection<Game> Games { get; set; }=new List<Game>();
        public ICollection<Game>? Games { get; set; } 
    }
}
