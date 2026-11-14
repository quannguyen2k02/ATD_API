using Application.DTOs.RequestDTOs.IO;
using Application.IRepositories.IO;
using Application.IServices.IO;
using AutoMapper;
using Domain.Enitties.IO;
using Infrastructure.Exceptions;

namespace Infrastructure.Services.IO
{
    public class IOStatusService : IIOStatusService
    {
        private readonly IIOStatusRepository _ioStatusRepository;
        private IMapper _mapper;
        private readonly IIORepository _ioRepository;
        public IOStatusService(IIOStatusRepository ioStatusRepository, IMapper mapper, IIORepository ioRepository)
        {
            _ioStatusRepository = ioStatusRepository;
            _mapper = mapper;
            _ioRepository = ioRepository;
        }
        public async Task<IOStatusRequest> AddIOStatusAsync(IOStatusRequest request)
        {
            var io = await _ioRepository.GetByIDAsync(request.IOId);
            if (io == null)
            {
                throw new NotFoundException($"IO with ID {request.IOId} not found.");
            }
            var result = await _ioStatusRepository.Add(_mapper.Map<IOStatus>(request));
            return _mapper.Map<IOStatusRequest>(result);
        }
    }
}
