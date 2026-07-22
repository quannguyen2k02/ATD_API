using Application.DTOs.RequestDTOs.IO;
using Application.DTOs.ResponseDTOs.IO;
using Application.IRepositories.IO;
using Application.IServices.IO;
using AutoMapper;
using Domain.Enitties.IO;
using Infrastructure.Exceptions;

namespace Infrastructure.Services.IO
{
    public class IOConfigManagementService : IIOConfigManagementService
    {
        private readonly IIOConfigManagementRepository _configRepository;
        private readonly IIOModelRepository _modelRepository;
        private readonly IMapper _mapper;
        private readonly IIORepository _ioRepository;
        public IOConfigManagementService(IIOConfigManagementRepository configRepository, IIOModelRepository modelRepository, IMapper mapper, IIORepository ioRepository)
        {
            _configRepository = configRepository;
            _modelRepository = modelRepository;
            _mapper = mapper;
            _ioRepository = ioRepository;
        }

        public async Task<IOConfigResponse> AddNewConfig(IOConfigRequest config)
        {
            var IOModel = await _modelRepository.GetIOModelByModelNameAndDeviceID(config.ModelName, config.IOId);
            if(IOModel == null)//if not exist model, create new model. 
            {
                var io = await _ioRepository.GetByIDAsync(config.IOId);
                if(io == null) throw new NotFoundException($"IO Id: {config.IOId} was not found.");
                //throw new NotFoundException($"Model name: '{config.ModelName}' or IO Id: {config.IOId} was not found.");
                var model = new IOModel();
                model.ModelName = config.ModelName;
                model.IOId = config.IOId;
                model.CreateDate = DateTime.Now;
                var result = await _modelRepository.Add(model); // add new model.
                var mappedConfig = _mapper.Map<IOConfigManagement>(config);
                mappedConfig.IOModelId = result.Id; // set id of config
                mappedConfig.CreateDate = DateTime.Now;
                return _mapper.Map<IOConfigResponse>(await _configRepository.Add(mappedConfig));
            }
            var IOConfig = _mapper.Map<IOConfigManagement>(config);
            IOConfig.IOModelId = IOModel.Id;
            IOConfig.CreateDate = DateTime.Now;
            return _mapper.Map<IOConfigResponse>( await _configRepository.Add(IOConfig));
        }
    }
}