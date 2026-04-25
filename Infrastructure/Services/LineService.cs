using Application.DTOs.ResponseDTOs;
using Application.IRepositories;
using Application.IServices.LED;
using AutoMapper;

namespace Infrastructure.Services
{
    public class LineService:ILineService
    {
        private readonly ILineRepository _lineRepository;
        private readonly IMapper _mapper;
        public LineService(ILineRepository lineRepository, IMapper mapper)
        {
            _lineRepository = lineRepository;
            _mapper = mapper;
        }

        public async Task<List<LineModelResponse>> GetAllLinesAsync()
        {
            var lines = await _lineRepository.GetAllLinesAsync();
            return _mapper.Map<List<LineModelResponse>>(lines);
        }

        public Task<int?> GetIdByLineName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetLineNameById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
