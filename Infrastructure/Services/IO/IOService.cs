using Application.DTOs.ResponseDTOs.IO;
using Application.IRepositories.IO;
using Application.IServices.IO;
using AutoMapper;

namespace Infrastructure.Services.IO
{
    public class IOService : IIOService
    {
        private readonly IMapper _mapper;
        private readonly IIORepository _ioRepository;
        public IOService(IMapper mapper, IIORepository ioRepository)
        {
            _mapper = mapper;
            _ioRepository = ioRepository;
        }
        public async Task<List<IOResponse>> GetIOsByLineId(int lineId)
        {
            var ios = await _ioRepository.Find(x=>x.LineId == lineId);
            return _mapper.Map<List<IOResponse>>(ios);
        }
    }
}
