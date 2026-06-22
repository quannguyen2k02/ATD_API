using Application.DTOs.ResponseDTOs.LED;
using Application.IRepository.LED;
using Application.IServices.LED;
using AutoMapper;

namespace Infrastructure.Services.LED
{
    public class LedService : ILedService
    {
        private readonly ILedRepository _ledRepository;
        private readonly IMapper _mapper;
        public LedService(ILedRepository ledRepository, IMapper mapper)
        {
            _ledRepository = ledRepository;
            _mapper = mapper;
        }
        public async Task<List<LedResponse>> GetLedsByLineId(int lineId)
        {
            var leds = await _ledRepository.GetLedsByLineId(lineId);
            return _mapper.Map<List<LedResponse>>(leds);
        }
    }
}
