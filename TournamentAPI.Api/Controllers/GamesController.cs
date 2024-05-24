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

namespace TournamentAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        

        private readonly IUOW _uOW;

        public GamesController(IUOW uOW)
        {
            _uOW = uOW;
        }



        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {

            var games = await _uOW.GameRepository.GetAllAsync();
            return Ok(games);

            
        }

        // GET: api/Games/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
           
            var game=await _uOW.GameRepository.GetAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        // PUT: api/Games/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

           

            try
            {
                var gameToChange = await _uOW.GameRepository.GetAsync(id);
             
                if (gameToChange==null)
                {
                    return NotFound();
                }

                gameToChange.Title = game.Title;
                gameToChange.Time=game.Time;
                gameToChange.TournamentId = game.TournamentId;
                _uOW.GameRepository.Update(gameToChange);
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

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
          
            _uOW.GameRepository.Add(game);
            await _uOW.CompleteAsync();
            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            
            var game=await _uOW.GameRepository.GetAsync(id);

            if (game == null)
            {
                return NotFound();
            }


            _uOW.GameRepository.Remove(game);
            await _uOW.CompleteAsync();

            return NoContent();
        }

      
    }
}
