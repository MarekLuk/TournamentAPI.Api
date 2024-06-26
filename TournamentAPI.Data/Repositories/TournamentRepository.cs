﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPI.Core.Entities;
using TournamentAPI.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using TournamentAPI.Data.Data;



namespace TournamentAPI.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentApiContext _context;

        public TournamentRepository(TournamentApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync()
        {
            return await _context.Set<Tournament>().ToListAsync();
        }

        public async Task<Tournament> GetAsync(int id)
        {
            var tournament = await _context.Set<Tournament>().FindAsync(id);
            if (tournament == null)
            {
                
                throw new KeyNotFoundException($"Id {id} was not found.");
            }
            return tournament;
        }

        public async Task<IEnumerable<Tournament>> GetAsyncExtraParameter(int id, bool extraParameter)
        {
            if (extraParameter)
            {              
                return await _context.Tournaments.Where(t => t.Title.Length > 10).ToListAsync();
            }
            else
            {                
                var tournament = await _context.Tournaments.FindAsync(id);
                return tournament != null ? new List<Tournament> { tournament } : new List<Tournament>();
            }
        }



        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Set<Tournament>().AnyAsync(t => t.Id == id);
        }

        public void Add(Tournament tournament)
        {
            _context.Set<Tournament>().Add(tournament);
        }

        public void Update(Tournament tournament)
        {
            _context.Set<Tournament>().Update(tournament);
        }

        public void Remove(Tournament tournament)
        {
            _context.Set<Tournament>().Remove(tournament);
        }
    }
}