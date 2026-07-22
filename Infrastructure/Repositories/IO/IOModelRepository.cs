using Application.IRepositories.IO;
using Domain.Enitties.IO;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.IO
{
    public class IOModelRepository : GenericRepository<IOModel>, IIOModelRepository
    {
        public IOModelRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IOModel> GetIOModelByModelNameAndDeviceID(string modelName, int deviceID)
        {
            var model = await _context.IOModels.Where(x => x.IOId == deviceID && x.ModelName.ToUpper() == modelName.ToUpper()).FirstOrDefaultAsync();
            return model;
        }
    }
}
