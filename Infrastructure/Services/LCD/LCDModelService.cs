using Application.Common;
using Application.DTOs.RequestDTOs.LCD;
using Application.DTOs.ResponseDTOs.LCD;
using Application.IRepositories.LCD;
using Application.IServices.LCD;
using AutoMapper;
using Infrastructure.Exceptions;

namespace Infrastructure.Services.LCD
{
    public class LCDModelService : ILCDModelService
    {
        private readonly ILCDModelRepository _lcdModelRepository;
        private readonly ILCDRepository _lcdRepository;
        private readonly IMapper _mapper;
        public LCDModelService(ILCDModelRepository lcdModelRepository, ILCDRepository lcdRepository, IMapper mapper)
        {
            _lcdModelRepository = lcdModelRepository;
            _lcdRepository = lcdRepository;
            _mapper = mapper;
        }
        public async Task<ResponseLCDModel> AddNewLCDModelAsync(RequestLCDModel model)
        {
            var lcd = await _lcdRepository.GetLCDById(model.LCDId);
            if(lcd == null)
            {
                throw new NotFoundException($"LCD with id '{model.Id}' was not found.");
            }
            model.ModelName = model.ModelName.ToUpper();
            model.CreateDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            var lcdModel = _mapper.Map<Domain.Enitties.LCD.LCDModel>(model);
            return _mapper.Map<ResponseLCDModel>(await _lcdModelRepository.AddNewLCDModelAsync(lcdModel));
        }

        public async Task<CursorPagedResult<ResponseLCDModel>> GetLCDModelsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20)
        {
            var lcd = await _lcdRepository.GetLCDById(deviceId);
            if (lcd == null)
            {
                throw new NotFoundException($"LCD with id '{deviceId}' was not found.");
            }
            var pagedResult = await _lcdModelRepository.GetLCDModelsByDeviceIdAsync(deviceId, lastId, pageSize);
            return new CursorPagedResult<ResponseLCDModel>
            {
                Items = pagedResult.Items.Select(x => _mapper.Map<ResponseLCDModel>(x)).ToList(),
                NextCursor = pagedResult.NextCursor,
                HasNextPage = pagedResult.HasNextPage,
                PageSize = pagedResult.PageSize
            };
        }

        public async Task<CursorPagedResult<ResponseLCDModel>> GetLCDModelsByModelNameAsync(string modelName, int deviceId, int? lastId = null, int pageSize = 20)
        {
            var lcd = await _lcdRepository.GetLCDById(deviceId);
            if (lcd == null)
            {
                throw new NotFoundException($"LCD with id '{deviceId}' was not found.");
            }
            var pagedResult = await _lcdModelRepository.GetLCDModelsByModelNameAsync(modelName,deviceId, lastId, pageSize);
            return new CursorPagedResult<ResponseLCDModel>
            {
                Items = pagedResult.Items.Select(x => _mapper.Map<ResponseLCDModel>(x)).ToList(),
                NextCursor = pagedResult.NextCursor,
                HasNextPage = pagedResult.HasNextPage,
                PageSize = pagedResult.PageSize
            };
        }
    }
}
