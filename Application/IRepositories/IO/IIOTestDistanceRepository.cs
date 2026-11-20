using Domain.Enitties.IO;

namespace Application.IRepositories.IO
{
    public interface IIOTestDistanceRepository : IGenericRepository<TestDistance>
    {
        public Task DeleteTestDistanceByIOId(int id);
    }
}
