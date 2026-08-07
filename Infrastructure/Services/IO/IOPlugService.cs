using Application.DTOs.RequestDTOs.IO;
using Application.IRepositories.IO;
using Application.IServices.IO;
using AutoMapper;
using Domain.Enitties.IO;
using Infrastructure.Exceptions;

namespace Infrastructure.Services.IO
{
    public class IOPlugService : IIOPlugService
    {
        private readonly IIOPlugRepository _ioPlugRepository;
        private readonly IMapper _mapper;
        private readonly IIORepository _iORepository;
        public IOPlugService(IIOPlugRepository ioPlugRepository, IMapper mapper, IIORepository iORepository)
        {
            _ioPlugRepository = ioPlugRepository;
            _mapper = mapper;
            _iORepository = iORepository;
        }

        public async Task<List<PlugItemDTO>> UpdatePort(PortRequest portRequest)
        {
            var io = await _iORepository.GetByIDAsync(portRequest.IOId);
            if(io == null)  throw new NotFoundException($"IO Id: {portRequest.IOId} was not found.");
            var plugItem = portRequest.PlugItems.Select(x => new PlugItemDTO
            {
                IOId = portRequest.IOId,
                Module = x.Module,
                Port = x.Port,
                Limited = x.Limited,
                Actual = x.Actual,
                Warning = x.Warning,
                CreateDate = DateTime.Now
            });
            var mappedItem = _mapper.Map<List<PlugItem>>(plugItem);
            await _ioPlugRepository.DeletePortByIoId(portRequest.IOId);
            var result = await _ioPlugRepository.AddRangeAsync(mappedItem);
            return _mapper.Map<List<PlugItemDTO>>(result);
        }
    }
}
