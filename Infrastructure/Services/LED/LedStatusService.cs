using Application.DTOs.RequestDTOs.LED;
using Application.DTOs.ResponseDTOs.LED;
using Application.IRepositories.LED;
using Application.IRepository.LED;
using Application.IServices.LED;
using AutoMapper;
using Domain.Enitties.LED;

namespace Infrastructure.Services.LED
{

    public class LedStatusService : ILedStatusService
    {
        private readonly ILedStatusRepository _ledStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILedRepository _ledRepository;
        public LedStatusService(ILedStatusRepository ledStatusRepository, IMapper mapper, ILedRepository ledRepository)
        {
            _ledStatusRepository = ledStatusRepository;
            _mapper = mapper;
            _ledRepository = ledRepository;
        }

        public async Task<LedDeviceStatusResponse> AddNewStatus(LedDeviceStatusRequest ledDeviceStatusRequest)
        {
            var ledDevice = await _ledRepository.GetDeviceNameByIdAsync(ledDeviceStatusRequest.LedDeviceId);

            if (ledDevice == null)
            {
                throw new Exception($"LED device with ID {ledDeviceStatusRequest.LedDeviceId} not found.");
            }
            ledDeviceStatusRequest.CreateDate = DateTime.Now;
            var ledStatus = _mapper.Map<LedDeviceStatus>(ledDeviceStatusRequest);
            return _mapper.Map<LedDeviceStatusResponse>(await _ledStatusRepository.Add(ledStatus));
        }
    }
}
