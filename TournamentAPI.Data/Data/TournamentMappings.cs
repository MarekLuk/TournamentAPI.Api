using AutoMapper;
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
            //  //CreateMap<Foo, FooDto>();m
            //  CreateMap<Tournament, TournamentDto>()
            //  .ForMember(dest => dest.Id, opt => opt.Ignore())
            //  .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            //  .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            //  .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.StartDate.AddMonths(33)));


            //  CreateMap<Game, GameDto>()
            //.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            //.ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Time));

            CreateMap<Tournament, TournamentDto>().ReverseMap();
            CreateMap<Game, GameDto>().ReverseMap();


        }


    }
}
