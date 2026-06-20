using Application.Common;
using Application.DTOs.RequestDTOs.LCD;
using Application.DTOs.ResponseDTOs.LCD;
using Application.IRepositories.LCD;
using Application.IServices.LCD;
using AutoMapper;
using Domain.Enitties.LCD;
using Domain.Entities.LED;
using Infrastructure.Exceptions;

namespace Infrastructure.Services.LCD
{
    public class LCDConfigService : ILCDConfigService
    {
        private readonly ILCDConfigRepository _lcdConfigRepository;
        private readonly IMapper _mapper;
        private readonly ILCDRepository _lcdRepository;
        public LCDConfigService(ILCDConfigRepository lcdConfigRepository, IMapper mapper, ILCDRepository lCDRepository)
        {
            _lcdConfigRepository = lcdConfigRepository;
            _mapper = mapper;
            _lcdRepository = lCDRepository;
        }

        public async Task<ResponseLCDConfig> AddLCDConfigAsync(RequestLCDConfig lcdConfig)
        {
            lcdConfig.CreateDate = DateTime.Now;
            var lcd = await _lcdRepository.GetByIDAsync(lcdConfig.LCDId);
            if(lcd == null)
            {
                throw new NotFoundException($"LCD with id '{lcdConfig.LCDId}' was not found.");
            }
            var lcdConfigEntity = _mapper.Map<LCDConfig>(lcdConfig);
            var addedConfig = await _lcdConfigRepository.Add(lcdConfigEntity);
            return _mapper.Map<ResponseLCDConfig>(addedConfig);
        }

        public Task<ResponseLCDConfig> GetLCDConfigByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CursorPagedResult<ResponseLCDConfig>> GetLCDConfigsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20)
        {
            var lcd = await _lcdRepository.GetByIDAsync(deviceId);
            if (lcd == null)
            {
                throw new NotFoundException($"LCD with id '{deviceId}' was not found.");
            }
            var result = await _lcdConfigRepository.GetLCDConfigsByDeviceIdAsync(deviceId, lastId, pageSize);
            return new CursorPagedResult<ResponseLCDConfig>
            {
                Items = result.Items.Select(x => _mapper.Map<ResponseLCDConfig>(x)).ToList(),
                NextCursor = result.NextCursor,
                HasNextPage = result.HasNextPage,
                PageSize = result.PageSize
            };
        }
    }
}
