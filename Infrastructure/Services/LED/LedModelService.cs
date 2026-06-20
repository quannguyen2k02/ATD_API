
using Application.Common;
using Application.DTOs.RequestDTOs.LED;
using Application.IRepositories;
using Application.IRepository.LED;
using Application.IServices.LED;
using AutoMapper;
using Domain.Entities.LED;
using Infrastructure.Exceptions;
using Infrastructure.ExternalServices.Mapper;

namespace Infrastructure.Services.LED
{
    public class LedModelService : ILedModelService
    {
        private readonly ILedModelRepository _LedModelRepository;
        private readonly ILedRepository _LedRepository;
        private readonly ILineRepository _LineRepository;
        private readonly IMapper _mapper;
        public LedModelService(ILedModelRepository ledModelRepository, IMapper mapper, ILedRepository ledRepository, ILineRepository lineRepository)
        {
            _LedModelRepository = ledModelRepository;
            _mapper = mapper;
            _LedRepository = ledRepository;
            _LineRepository = lineRepository;
        }
        public async Task<LedModelDTO> AddLedModelAsync(LedModelDTO ledModelDTO)
        {
            int? lineId = await _LineRepository.GetIdByLineName(ledModelDTO.LineName);
            if (!lineId.HasValue)
                throw new NotFoundException($"Line with name '{ledModelDTO.LineName}' was not found.");

            int? deviceId = await _LedRepository.GetDeviceIdByDeviceNameAndLineNameAsync(ledModelDTO.DeviceName, (int)lineId);
            if (!deviceId.HasValue)
            {
                throw new NotFoundException($"Device with name '{ledModelDTO.DeviceName}' was not found.");
            }
            ledModelDTO.LedId = (int)deviceId;
            var ledModel = _mapper.Map<LedModel>(ledModelDTO);
            return _mapper.Map<LedModelDTO>(await _LedModelRepository.Add(ledModel));
        }

        public async Task<CursorPagedResult<dynamic>> GetLedModelAsync(string line, string devicename, string model, string kb, string fp, int? lastId = null, int pageSize = 20)
        {
            // 1. Lấy lineId và deviceId (giữ nguyên)
            int? lineId = await _LineRepository.GetIdByLineName(line);
            if (!lineId.HasValue)
                throw new NotFoundException($"Line with name '{line}' was not found.");

            int? deviceId = await _LedRepository.GetDeviceIdByDeviceNameAndLineNameAsync(devicename, (int)lineId);
            if (!deviceId.HasValue)
                throw new NotFoundException($"Device with name '{devicename}' in line '{line}' was not found.");
            var repoPage = await _LedModelRepository.GetLedModelAsync((int)deviceId, model, kb, fp, lastId, pageSize);
            var mappedItems = _mapper.Map<List<LedModelDTO>>(repoPage.Items);
            var resultItems = new List<dynamic>();
            foreach (var item in mappedItems)
            {
                resultItems.Add(LED_MapToDynamic.MapToDynamic(item));
            }
            return new CursorPagedResult<dynamic>
            {
                Items = resultItems,
                NextCursor = repoPage.NextCursor,    
                HasNextPage = repoPage.HasNextPage,
                PageSize = repoPage.PageSize
            };
        }

        public async Task<CursorPagedResult<dynamic>> GetLedModelsByDevice(string line, string devicename, int? lastId = null, int pageSize = 20)
        {
            int? lineId = await _LineRepository.GetIdByLineName(line);
            if (!lineId.HasValue)
                throw new NotFoundException($"Line with name '{line}' was not found.");

            int? deviceId = await _LedRepository.GetDeviceIdByDeviceNameAndLineNameAsync(devicename, (int)lineId);
            if (!deviceId.HasValue)
                throw new NotFoundException($"Device with name '{devicename}' in line '{line}' was not found.");
            var repoResult = await _LedModelRepository.GetLedModelsByDeviceIdAsync((int)deviceId, lastId, pageSize);
            var mappedItems = _mapper.Map<List<LedModelDTO>>(repoResult.Items);
            var dynamicItems = mappedItems
                .Select(item => LED_MapToDynamic.MapToDynamic(item))
                .ToList();
            return new CursorPagedResult<dynamic>
            {
                Items = dynamicItems,
                NextCursor = repoResult.NextCursor,
                HasNextPage = repoResult.HasNextPage,
                PageSize = repoResult.PageSize
            };
        }

        public async Task<dynamic> GetLedModelById(int id)
        {
            var model = await _LedModelRepository.GetByIDAsync(id);
            if (model == null)
                throw new NotFoundException($"Model with ID {id} was not found!");
            var mapped = _mapper.Map<LedModelDTO>(model);
            return LED_MapToDynamic.MapToDynamic(mapped);
        }
    }
}
