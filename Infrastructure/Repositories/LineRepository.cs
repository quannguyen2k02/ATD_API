using Application.Common;
using Application.IRepositories;
using Domain.Enitties;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LineRepository : ILineRepository
    {
        private readonly ApplicationDbContext _context;
        public LineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Line>> GetAllLinesAsync()
        {
            var lines = await _context.Lines.ToListAsync();
            return lines;
        }

        public async Task<int?> GetIdByLineName(string name)
        {
            var lineId = await _context.Lines
        .Where(l => l.Name.ToLower() == name.ToLower())
        .Select(l => (int?)l.Id)
        .FirstOrDefaultAsync();
            return lineId;
        }


        public async Task<string?> GetLineNameById(int id)
        {
            var lineName = await _context.Lines
                .Where(l => l.Id == id)
                .Select(l => (string?)l.Name).FirstOrDefaultAsync();
            return lineName;
        }
    }
}
