using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentAPI.Data.Data;
using TournamentAPI.Core.Entities;
using TournamentAPI.Core.Repositories;
using TournamentAPI.Core.Dto;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using AutoMapper;
using TournamentAPI.Core.Dto.TournamentDto;

namespace TournamentAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        //private readonly TournamentApiContext _context;

        //public TournamentsController(TournamentApiContext context)
        //{
        //    _context = context;
        //}

        private readonly IUOW _uOW;
        private readonly IMapper _mapper;

        public TournamentsController(IUOW uOW, IMapper mapper)
        {
            _uOW = uOW;
            _mapper = mapper;
        }




        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournaments()
        {
            var tournaments=await _uOW.TournamentRepository.GetAllAsync();
            var tournamentsDto=_mapper.Map<IEnumerable<TournamentDto>>(tournaments);
            return Ok(tournamentsDto);
        }

        // GET: api/Tournaments/1
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetTournament(int id)
        {
            var tournament = await _uOW.TournamentRepository.GetAsync(id);

            if (tournament == null)
            {
                return NotFound();
            }

            var tournamentDto=_mapper.Map<TournamentDto>(tournament);

            return Ok(tournament);
        }

        // PUT: api/Tournaments/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, TournamentDto tournamentDto)
        {
            if (id != tournamentDto.Id)
            {
                return BadRequest("Id doesnot match");
            }

            var tournamentToChange = await _uOW.TournamentRepository.GetAsync(id);
            if (tournamentToChange == null)
            {
                return NotFound($"Not found {id}");
            }

        
            _mapper.Map(tournamentDto, tournamentToChange);

            try
            {
                _uOW.TournamentRepository.Update(tournamentToChange);
                await _uOW.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uOW.TournamentRepository.AnyAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();





            //_context.Entry(tournament).State = EntityState.Modified;

            //try
            //{
            //    var tournamentToChange = await _uOW.TournamentRepository.GetAsync(id);
            //    if (tournamentToChange == null)
            //    {
            //        return NotFound();
            //    }

            //    tournamentToChange.Title= tournament.Title;
            //    tournamentToChange.StartDate=tournament.StartDate;
            //    _uOW.TournamentRepository.Update(tournamentToChange);
            //    await _uOW.CompleteAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!_uOW.TournamentRepository.AnyAsync(id).Result)
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentDto>> PostTournament(TournamentDto tournamentDto)
        {
            if (tournamentDto == null)
            {
                return BadRequest("check why does not work-one");
            }

            var tournament =_mapper.Map<Tournament>(tournamentDto);
            _uOW.TournamentRepository.Add(tournament);
            try
            {
                await _uOW.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(409, "check why does not work-two");
            }


            //_context.Tournaments.Add(tournament);
            //await _context.SaveChangesAsync();

            //var cTournamentDto = _mapper.Map<TournamentDto>(tournament);
            //return CreatedAtAction("GetTournament", new { id = tournament.Id }, cTournamentDto);



            var createdTournamentDto = _mapper.Map<TournamentDto>(tournament);
            return CreatedAtAction("GetTournament", new { id = tournament.Id }, createdTournamentDto);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            //var tournament = await _context.Tournaments.FindAsync(id);
            var tournament = await _uOW.TournamentRepository.GetAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

          _uOW.TournamentRepository.Remove(tournament);
            await _uOW.CompleteAsync();

            return NoContent();

        }

        //private async Task<bool> TournamentExists(int id)
        //{
        //    return await _uOW.TournamentRepository.AnyAsync(e => e.Id == id);
        //}
    }
}
