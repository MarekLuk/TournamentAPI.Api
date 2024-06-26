﻿using System;
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
using Microsoft.AspNetCore.JsonPatch;
using TournamentAPI.Data.Repositories;

namespace TournamentAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
       

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
        public async Task<ActionResult<TournamentDto>> GetTournament(int id, bool extraParameter=false)
        {
            var tournament = await _uOW.TournamentRepository.GetAsyncExtraParameter(id, extraParameter);

            if (tournament == null)
            {
                return NotFound();
            }

            if (!extraParameter)
            {
                var tournamentDto = _mapper.Map<TournamentDto>(tournament.SingleOrDefault());
                return Ok(tournamentDto);
            }
            else
            {
                var tournamentDto2 = _mapper.Map<IEnumerable<TournamentDto>>(tournament);
                return Ok(tournamentDto2);
            }
        }

        // PUT: api/Tournaments/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, TournamentDto tournamentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tournamentToChange = await _uOW.TournamentRepository.GetAsync(id);
            if (tournamentToChange == null)
            {
                return NotFound();
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
                    return StatusCode(500, "Failed");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // PATCH: api/Tournaments/1
        [HttpPatch("{tournamentId}")]

        public async Task<ActionResult<TournamentDto>> PatchTournament
            (int tournamentId, JsonPatchDocument<TournamentDto> patchDocument)
        {
            if (patchDocument==null) 
            {
                return NoContent();
            }

            var tournament= await _uOW.TournamentRepository.GetAsync(tournamentId);

            if (patchDocument == null)
            {
                return NotFound();
            }


            var tournamentDto=_mapper.Map<TournamentDto>(tournament);


            patchDocument.ApplyTo(tournamentDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(tournamentDto, tournament);

            _uOW.TournamentRepository.Update(tournament);

            try
            {
                await _uOW.CompleteAsync();
            }
            catch
            {
                return StatusCode(500);
            }

            return Ok(_mapper.Map<TournamentDto>(tournament));

        }


        // POST: api/Tournaments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentDto>> PostTournament(TournamentDto tournamentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tournament =_mapper.Map<Tournament>(tournamentDto);
            _uOW.TournamentRepository.Add(tournament);
            try
            {
                await _uOW.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException )
            {
                return StatusCode(500, "Failed");
            }

            var createdTournamentDto = _mapper.Map<TournamentDto>(tournament);
            return CreatedAtAction("GetTournament", new { id = tournament.Id }, createdTournamentDto);
        }

        // DELETE: api/Tournaments/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            
            var tournament = await _uOW.TournamentRepository.GetAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

          _uOW.TournamentRepository.Remove(tournament);
            await _uOW.CompleteAsync();

            return NoContent();

        }

     
    }
}
