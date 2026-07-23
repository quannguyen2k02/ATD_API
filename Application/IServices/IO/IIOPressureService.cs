using Application.DTOs.RequestDTOs.IO;
using Application.DTOs.ResponseDTOs.IO;

namespace Application.IServices.IO
{
    public interface IIOPressureService
    {
        public Task<IOPressureResponse> AddNewPressures(IOPressureRequest iOPressureRequest);
    }
}
