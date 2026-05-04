using Application.Common;
using Application.DTOs.RequestDTOs.LED;
using Application.DTOs.ResponseDTOs.LED;
using Application.IRepositories.LED;
using Application.IRepository.LED;
using Application.IServices.LED;
using AutoMapper;
using Domain.Entities.LED;
using Infrastructure.Exceptions;

namespace Infrastructure.Services.LED
{
    public class LedConfigService : ILedConfigService
    {
        private readonly ILedConfigRepository _ledConfigRepository;
        private readonly IMapper _mapper;
        private readonly ILedRepository _ledRepository;
        public LedConfigService(ILedConfigRepository ledConfigRepository, IMapper mapper, ILedRepository ledRepository)
        {
            _ledConfigRepository = ledConfigRepository;
            _mapper = mapper;
            _ledRepository = ledRepository;
        }
        public async Task<LedConfigDTO> AddLedConfigAsync(LedConfigDTO ledConfigDTO)
        {
            var led = await _ledRepository.GetDeviceNameByIdAsync(ledConfigDTO.LedId);
            if (led == null)
            {
                throw new NotFoundException($"Led id: '{ledConfigDTO.ID}' was not found.");
            }
            var ledConfig = _mapper.Map<LedConfig>(ledConfigDTO);
            return _mapper.Map<LedConfigDTO>(await _ledConfigRepository.AddLedModelAsync(ledConfig));
        }

        public async Task<CursorPagedResult<LedConfigResponse>> GetLedConfigByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20)
        {
            var led = await _ledRepository.GetDeviceNameByIdAsync(deviceId);
            if (led == null)
            {
                throw new NotFoundException($"Led id: '{deviceId}' was not found.");
            }

            var repoPage = await _ledConfigRepository.GetLedConfigByDeviceIdAsync(deviceId, lastId, pageSize);

            var mappedItems = _mapper.Map<List<LedConfigResponse>>(repoPage.Items);

            return new CursorPagedResult<LedConfigResponse>
            {
                Items = mappedItems,
                NextCursor = repoPage.NextCursor,
                HasNextPage = repoPage.HasNextPage,
                PageSize = repoPage.PageSize
            };
        }

    }
}
