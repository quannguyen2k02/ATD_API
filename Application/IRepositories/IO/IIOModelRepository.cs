using Domain.Enitties.IO;

namespace Application.IRepositories.IO
{
    public interface IIOModelRepository : IGenericRepository<IOModel>
    {
        public Task<IOModel> GetIOModelByModelNameAndDeviceID(string modelName, int deviceID);
    }
}
