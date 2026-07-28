using Application.DTOs.RequestDTOs.IO;
using Application.DTOs.ResponseDTOs.IO;
using Application.IRepositories.IO;
using Application.IServices.IO;
using AutoMapper;
using Domain.Enitties.IO;

namespace Infrastructure.Services.IO
{
    public class IOModelService : IIOModelService
    {
        private readonly IIOModelRepository _modelRepository;
        private readonly IMapper _mapper;
        public IOModelService(IIOModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<IOModelResponse> AddNewModel(IOModelRequest model)
        {
            var IoModel = _mapper.Map<IOModel>(model);
            var result = await _modelRepository.Add(IoModel);
            return _mapper.Map<IOModelResponse>(result);
        }

        public async Task<List<IOModelResponse>> GetModelsByIOId(int ioId)
        {
            var models = await _modelRepository.Find(x=>x.IOId == ioId);
            return _mapper.Map<List<IOModelResponse>>(models);
        }
    }
}
