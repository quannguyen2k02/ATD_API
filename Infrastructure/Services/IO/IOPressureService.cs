using Application.DTOs.RequestDTOs.IO;
using Application.DTOs.ResponseDTOs.IO;
using Application.IRepositories.IO;
using Application.IServices.IO;
using AutoMapper;
using Domain.Enitties.IO;
using Infrastructure.Exceptions;
using Infrastructure.Repositories.IO;

namespace Infrastructure.Services.IO
{
    public class IOPressureService : IIOPressureService
    {
        private readonly IIORepository _iORepository;
        private readonly IIOModelRepository _iOModelRepository;
        private readonly IMapper _mapper;
        private readonly IIOPressureManagementRepository _iOPressureRepository;
        public IOPressureService(IIORepository iORepository, IIOModelRepository iOModelRepository, IMapper mapper, IIOPressureManagementRepository iOPressureRepository)
        {
            _iORepository = iORepository;
            _iOModelRepository = iOModelRepository;
            _mapper = mapper;
            _iOPressureRepository = iOPressureRepository;
        }
        public async Task<IOPressureResponse> AddNewPressures(IOPressureRequest iOPressureRequest)
        {
            var io = await _iORepository.GetByIDAsync(iOPressureRequest.IOId);
            if(io == null)
            {
                throw new NotFoundException($"IO Id: {iOPressureRequest.IOId} was not found.");
            }
            var model = await _iOModelRepository.GetIOModelByModelNameAndDeviceID(iOPressureRequest.ModelName, iOPressureRequest.IOId);
            if (model == null)
            {
                var m = new IOModel();
                m.ModelName = iOPressureRequest.ModelName;
                m.IOId = iOPressureRequest.IOId;
                m.CreateDate = DateTime.Now;
                var result = await _iOModelRepository.Add(m); // add new model.
                var pressure = _mapper.Map<PressureManagement>(iOPressureRequest);
                pressure.IOModelId = result.Id;
                pressure.CreateDate = DateTime.Now;
                return _mapper.Map<IOPressureResponse>(await _iOPressureRepository.Add(pressure));
            }
            var mappedPressure = _mapper.Map<PressureManagement>(iOPressureRequest);
            mappedPressure.IOModelId = model.Id;
            mappedPressure.CreateDate = DateTime.Now;
            return _mapper.Map<IOPressureResponse>(await _iOPressureRepository.Add(mappedPressure));

        }
    }
}