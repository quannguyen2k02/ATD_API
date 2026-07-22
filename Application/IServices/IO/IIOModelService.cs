using Application.DTOs.RequestDTOs.IO;
using Application.DTOs.ResponseDTOs.IO;

namespace Application.IServices.IO
{
    public interface IIOModelService
    {
        public Task<IOModelResponse> AddNewModel(IOModelRequest model);
    }
}
