using Domain.Enitties.IO;

namespace Application.IRepositories.IO
{
    public interface IIOVccdRepository : IGenericRepository<VccdItem>
    {
        public Task DeleteVccdByIOId(int id);
    }
}
