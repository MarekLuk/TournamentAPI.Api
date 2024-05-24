﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPI.Core.Dto.TournamentDto;
using TournamentAPI.Core.Dto.GameDto;
using TournamentAPI.Core.Entities;


namespace TournamentAPI.Data.Data
{
    public class TournamentMappings:Profile
    {
        public TournamentMappings() {
         
            CreateMap<Tournament, TournamentDto>().ReverseMap();
            CreateMap<Game, GameDto>().ReverseMap();


        }


    }
}
