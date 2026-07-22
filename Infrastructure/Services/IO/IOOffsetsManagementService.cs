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
    public class IOOffsetsManagementService : IIOOffsetManagementService
    {
        private IIOOffsetManagementRepository _ioOffsetManagementRepository;
        private readonly IMapper _mapper;
        private readonly IIOModelRepository _modelRepository;
        private readonly IIORepository _ioRepository;
        public IOOffsetsManagementService(IIOOffsetManagementRepository ioOffsetManagementRepository, IMapper mapper, IIOModelRepository modelRepository, IIORepository ioRepository)
        {
            _ioOffsetManagementRepository = ioOffsetManagementRepository;
            _mapper = mapper;
            _modelRepository = modelRepository;
            _ioRepository = ioRepository;
        }

        public async Task<IOOffsetsResponse> AddNewOffset(IOOffsetsRequest offsetsRequest)
        {
            var IOModel = await _modelRepository.GetIOModelByModelNameAndDeviceID(offsetsRequest.ModelName, offsetsRequest.IOId);
            if (IOModel == null)//if not exist model, create new model. 
            {
                var io = await _ioRepository.GetByIDAsync(offsetsRequest.IOId);
                if (io == null) throw new NotFoundException($"IO Id: {offsetsRequest.IOId} was not found.");
                //throw new NotFoundException($"Model name: '{config.ModelName}' or IO Id: {config.IOId} was not found.");
                var model = new IOModel();
                model.ModelName = offsetsRequest.ModelName;
                model.IOId = io.Id;
                model.CreateDate = DateTime.Now;
                var result = await _modelRepository.Add(model); // add new model.
                var mappedOffset = _mapper.Map<OffsetManagement>(offsetsRequest);
                mappedOffset.IOModelId = result.Id; // set id of motion point
                mappedOffset.CreateDate = DateTime.Now;
                return _mapper.Map<IOOffsetsResponse>(await _ioOffsetManagementRepository.Add(mappedOffset));
            }
            var offsets = _mapper.Map<OffsetManagement>(offsetsRequest);
            offsets.IOModelId = IOModel.Id;
            offsets.CreateDate = DateTime.Now;
            return _mapper.Map<IOOffsetsResponse>(await _ioOffsetManagementRepository.Add(offsets));
        }
    }
}
