using Application.IRepositories;
using Domain.Enitties;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ModelGroupRepository : GenericRepository<ModelGroup>, IModelGroupRepository
    {
        public ModelGroupRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
