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
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

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

        public TournamentsController(IUOW uOW)
        {
            _uOW = uOW;
        }




        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournaments()
        {
            var tournaments=await _uOW.TournamentRepository.GetAllAsync();
            return Ok(tournaments);
        }

        // GET: api/Tournaments/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetTournament(int id)
        {
            var tournament = await _uOW.TournamentRepository.GetAsync(id);

            if (tournament == null)
            {
                return NotFound();
            }

            return Ok(tournament);
        }

        // PUT: api/Tournaments/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, Tournament tournament)
        {
            if (id != tournament.Id)
            {
                return BadRequest();
            }

            //_context.Entry(tournament).State = EntityState.Modified;

            try
            {
                var tournamentToChange = await _uOW.TournamentRepository.GetAsync(id);
                if (tournamentToChange == null)
                {
                    return NotFound();
                }

                tournamentToChange.Title= tournament.Title;
                tournamentToChange.StartDate=tournament.StartDate;
                _uOW.TournamentRepository.Update(tournamentToChange);
                await _uOW.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_uOW.TournamentRepository.AnyAsync(id).Result)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(Tournament tournament)
        {
            _uOW.TournamentRepository.Add(tournament);
            await _uOW.CompleteAsync();
            //_context.Tournaments.Add(tournament);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetTournament", new { id = tournament.Id }, tournament);
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
