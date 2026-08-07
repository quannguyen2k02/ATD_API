using Domain.Enitties.IO;

namespace Application.IRepositories.IO
{
    public interface IIOPlugRepository : IGenericRepository<PlugItem>
    {
        public Task DeletePortByIoId(int ioId);
    }
}
