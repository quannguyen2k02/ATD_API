using Application.DTOs.RequestDTOs.LED;
using Application.DTOs.ResponseDTOs.LED;
using Application.IRepositories.LED;
using Application.IRepository.LED;
using Application.IServices.LED;
using AutoMapper;
using Domain.Enitties.LED;
using Infrastructure.Data;
using Infrastructure.Exceptions;

namespace Infrastructure.Services.LED
{
    public class LedResultService : ILedResultService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILedResultRepository _ledResultRepository;
        private readonly ILedRepository _ledRepository;
        private readonly IMapper _mapper;
        public LedResultService(ApplicationDbContext context, ILedResultRepository ledResultRepository, ILedRepository ledRepository, IMapper mapper)
        {
            _context = context;
            _ledResultRepository = ledResultRepository;
            _ledRepository = ledRepository;
            _mapper = mapper;
        }
        public async Task<LedResultResponse> AddnewLedResult(LedResultRequest ledResultRequest)
        {
            var led = await _ledRepository.GetByIDAsync(ledResultRequest.LEDId);
            if(led == null)
            {
                throw new NotFoundException($"LED with ID {ledResultRequest.LEDId} not found.");
            }
            var ledResult = _mapper.Map<LedResult>(ledResultRequest);
            var result = await _ledResultRepository.Add(ledResult);
            return _mapper.Map<LedResultResponse>(result);
        }
    }
}
