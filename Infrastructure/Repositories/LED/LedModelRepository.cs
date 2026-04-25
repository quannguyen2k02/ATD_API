using Application.Common;
using Application.IRepository.LED;
using Domain.Entities.LED;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.LED
{
    public class LedModelRepository : ILedModelRepository
    {
        private readonly ApplicationDbContext _context;
        public LedModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<LedModelConfig> AddLedModelConfig(LedModelConfig ledModelConfig)
        {
            throw new NotImplementedException();
        }

        public async Task<LedModel> AddLedModelAsync(LedModel ledModel)
        {
            ledModel.CreateDate = DateTime.Now;
            ledModel.ModifiedDate = DateTime.Now;
            await _context.LEDModels.AddAsync(ledModel);
            await _context.SaveChangesAsync();
            return ledModel;
        }

        public async Task<PagedResult<LedModel>> GetLedModelAsync(int deviceId, string model, string kb, string fp, int? lastId = null, int pageSize = 20)
        {
            // S? d?ng .Include() ?? n?p các b?ng liên quan, tránh b? null d? li?u
            var baseQuery = _context.LEDModels
                .Include(x => x.LEDModelConfigs)
                .Include(x => x.Cameras)
                    .ThenInclude(c => c.LedStatuses)
                        .ThenInclude(c => c.Jobs)
                .Where(x => x.KB == kb && x.FP == fp && x.LedId == deviceId && x.Name.ToLower() == model.ToLower());

            // For keyset pagination, filter by Id > lastId (assuming ascending order)
            if (lastId.HasValue)
            {
                baseQuery = baseQuery.Where(x => x.Id > lastId.Value);
            }

            var totalCount = await _context.LEDModels
                .Where(x => x.KB == kb && x.FP == fp && x.LedId == deviceId && x.Name.ToLower() == model.ToLower())
                .CountAsync();

            var items = await baseQuery
                .OrderBy(x => x.Id)
                .Take(Math.Max(pageSize, 1))
                .ToListAsync();

            return new PagedResult<LedModel>
            {
                Items = items,
                PageNumber = lastId ?? 0, // reuse PageNumber field to carry lastId for client; not ideal but keeps signature
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<PagedResult<LedModel>> GetLedModelsByDeviceIdAsync(int id, int? lastId = null, int pageSize = 20)
        {
            // Ensure valid pageSize
            pageSize = Math.Max(pageSize, 1);

            // Step 1: Get the latest Id per group
            var latestIdsQuery = _context.LEDModels
                .Where(x => x.LedId == id)
                .GroupBy(x => new { x.Name, x.KB, x.FP })
                .Select(g => g.Max(x => x.Id)); // Latest Id per group

            // If keyset pagination, only get ids greater than lastId
            if (lastId.HasValue)
            {
                latestIdsQuery = latestIdsQuery.Where(x => x > lastId.Value);
            }

            var totalCount = await _context.LEDModels
                .Where(x => x.LedId == id)
                .GroupBy(x => new { x.Name, x.KB, x.FP })
                .Select(g => g.Key)
                .CountAsync();

            var pagedIds = await latestIdsQuery
                .OrderBy(x => x)
                .Take(pageSize)
                .ToListAsync();

            var items = await _context.LEDModels
                .Where(x => pagedIds.Contains(x.Id))
                .OrderBy(x => x.Id)
                .ToListAsync();

            return new PagedResult<LedModel>
            {
                Items = items,
                PageNumber = lastId ?? 0, // use PageNumber to convey lastId back
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<LedModel> GetLedModelById(int id)
        {
            var ledModel = await _context.LEDModels
                .FindAsync(id);
            return ledModel;
        }
    }
}
