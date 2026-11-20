using Application.DTOs.RequestDTOs.IO;
using Application.IRepositories.IO;
using Application.IServices.IO;
using AutoMapper;
using Domain.Enitties.IO;
using Infrastructure.Exceptions;

namespace Infrastructure.Services.IO
{
    public class IOTestDistanceService : IIOTestDistanceService
    {
        private readonly IIOTestDistanceRepository _ioTestDistanceRepository;
        private readonly IMapper _mapper;
        private readonly IIORepository _iORepository;
        public IOTestDistanceService(IIOTestDistanceRepository ioTestDistanceRepository, IMapper mapper, IIORepository iORepository)
        {
            _ioTestDistanceRepository = ioTestDistanceRepository;
            _mapper = mapper;
            _iORepository = iORepository;
        }
        public async Task<List<IOTestDistanceDTO>> UpdateTestDistance(IOTestDistanceRequest request)
        {
            var io = await _iORepository.GetByIDAsync(request.IOId);
            if (io == null) throw new NotFoundException($"IO Id: {request.IOId} was not found.");
            var items = request.TestDistances.Select(x => new IOTestDistanceDTO
            {
                IOId = request.IOId,
                Module = x.Module,
                Distance = x.Distance
            });
            var mappedItem = _mapper.Map<List<TestDistance>>(items);
            await _ioTestDistanceRepository.DeleteTestDistanceByIOId(request.IOId);
            var result = await _ioTestDistanceRepository.AddRangeAsync(mappedItem);
            return _mapper.Map<List<IOTestDistanceDTO>>(result);
        }
    }
}
