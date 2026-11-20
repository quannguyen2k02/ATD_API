using Application.DTOs.RequestDTOs.IO;
using Application.IRepositories.IO;
using Application.IServices.IO;
using AutoMapper;
using Domain.Enitties.IO;
using Infrastructure.Exceptions;

namespace Infrastructure.Services.IO
{
    public class IOVccdService : IIOVccdService
    {
        private readonly IIOVccdRepository _ioVccdRepository;
        private readonly IMapper _mapper;
        private readonly IIORepository _iORepository;
        public IOVccdService(IIOVccdRepository ioVccdRepository, IMapper mapper, IIORepository iORepository)
        {
            _ioVccdRepository = ioVccdRepository;
            _mapper = mapper;
            _iORepository = iORepository;
        }
        public async Task<List<IOVccdDTO>> UpdateVccd(IOVccdRequest vccdRequest)
        {
            var io = await _iORepository.GetByIDAsync(vccdRequest.IOId);
            if (io == null) throw new NotFoundException($"IO Id: {vccdRequest.IOId} was not found.");
            var plugItem = vccdRequest.Vccds.Select(x => new IOVccdDTO
            {
                IOId = vccdRequest.IOId,
                Module = x.Module,
                Camera = x.Camera,
                ExposureTime = x.ExposureTime,
                CreatedDate = DateTime.Now
            });
            var mappedItem = _mapper.Map<List<VccdItem>>(plugItem);
            await _ioVccdRepository.DeleteVccdByIOId(vccdRequest.IOId);
            var result = await _ioVccdRepository.AddRangeAsync(mappedItem);
            return _mapper.Map<List<IOVccdDTO>>(result);
        }
    }
}
