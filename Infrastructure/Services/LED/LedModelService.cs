
using Application.Common;
using Application.DTOs.RequestDTOs.LED;
using Application.IRepositories;
using Application.IRepository.LED;
using Application.IServices.LED;
using AutoMapper;
using Domain.Enitties.ChangeLog;
using Domain.Entities.LED;
using Infrastructure.Exceptions;
using Infrastructure.ExternalServices.Mapper;

namespace Infrastructure.Services.LED
{
    public class LedModelService : ILedModelService
    {
        private readonly ILedModelRepository _LedModelRepository;
        private readonly ILedRepository _LedRepository;
        private readonly ILineRepository _LineRepository;
        private readonly IMapper _mapper;
        public LedModelService(ILedModelRepository ledModelRepository, IMapper mapper, ILedRepository ledRepository, ILineRepository lineRepository)
        {
            _LedModelRepository = ledModelRepository;
            _mapper = mapper;
            _LedRepository = ledRepository;
            _LineRepository = lineRepository;
        }
        //public async Task<LedModelDTO> AddLedModelAsync(LedModelDTO ledModelDTO)
        //{
        //    try
        //    {
        //        // 1. Xử lý Line và Device (Giữ nguyên logic của bạn)
        //        int? lineId = await _LineRepository.GetIdByLineName(ledModelDTO.LineName);
        //        if (!lineId.HasValue)
        //            throw new NotFoundException($"Line with name '{ledModelDTO.LineName}' was not found.");

        //        int? deviceId = await _LedRepository.GetDeviceIdByDeviceNameAndLineNameAsync(ledModelDTO.DeviceName, (int)lineId);
        //        if (!deviceId.HasValue)
        //            throw new NotFoundException($"Device with name '{ledModelDTO.DeviceName}' was not found.");

        //        ledModelDTO.LedId = (int)deviceId;

        //        // 2. Lấy bản ghi gần nhất từ DB (Entity)
        //        var oldModelEntity = await _LedModelRepository.GetLatestLedModel(ledModelDTO.LedId, ledModelDTO.Name!);

        //        // TRƯỜNG HỢP 1: Chưa có bản ghi nào trong DB -> Lưu luôn
        //        if (oldModelEntity == null)
        //        {
        //            var newEntity = _mapper.Map<LedModel>(ledModelDTO);
        //            var savedEntity = await _LedModelRepository.Add(newEntity);
        //            return _mapper.Map<LedModelDTO>(savedEntity);
        //        }

        //        // 3. Chuyển Entity cũ sang DTO để có cùng cấu trúc so sánh
        //        var oldModelDTO = _mapper.Map<LedModelDTO>(oldModelEntity);

        //        // 4. So sánh bằng hàm DynamicComparer bạn đã viết
        //        bool isDifferent = !DynamicComparer.AreMappedModelsEqual(oldModelDTO, ledModelDTO);

        //        // TRƯỜNG HỢP 2: Dữ liệu không có gì thay đổi -> Trả về bản ghi cũ, không lưu
        //        if (!isDifferent)
        //        {
        //            return oldModelDTO;
        //        }

        //        // TRƯỜNG HỢP 3: Có sự khác biệt -> Thêm mới
        //        var newModel = _mapper.Map<LedModel>(ledModelDTO);
        //        var result = await _LedModelRepository.Add(newModel);

        //        return _mapper.Map<LedModelDTO>(result);
        //    }
        //    catch (NotFoundException)
        //    {
        //        throw; // Nên throw lại để Controller bắt được lỗi 404 (NotFound)
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"An error occurred while adding the LED model: {ex.Message}", ex);
        //    }
        //}
        public async Task<LedModelDTO> AddLedModelAsync(LedModelDTO ledModelDTO)
        {
            int? lineId = await _LineRepository.GetIdByLineName(ledModelDTO.LineName);
            if (!lineId.HasValue)
                throw new NotFoundException($"Line with name '{ledModelDTO.LineName}' was not found.");

            int? deviceId = await _LedRepository.GetDeviceIdByDeviceNameAndLineNameAsync(ledModelDTO.DeviceName, (int)lineId);
            if (!deviceId.HasValue)
            {
                throw new NotFoundException($"Device with name '{ledModelDTO.DeviceName}' was not found.");
            }
            ledModelDTO.LedId = (int)deviceId;
            var ledModel = _mapper.Map<LedModel>(ledModelDTO);

            return _mapper.Map<LedModelDTO>(await _LedModelRepository.Add(ledModel));
        }

        public async Task<CursorPagedResult<dynamic>> GetLedModelAsync(string line, string devicename, string model, string kb, string fp, int? lastId = null, int pageSize = 20)
        {
            // 1. Lấy lineId và deviceId (giữ nguyên)
            int? lineId = await _LineRepository.GetIdByLineName(line);
            if (!lineId.HasValue)
                throw new NotFoundException($"Line with name '{line}' was not found.");

            int? deviceId = await _LedRepository.GetDeviceIdByDeviceNameAndLineNameAsync(devicename, (int)lineId);
            if (!deviceId.HasValue)
                throw new NotFoundException($"Device with name '{devicename}' in line '{line}' was not found.");
            var repoPage = await _LedModelRepository.GetLedModelAsync((int)deviceId, model, kb, fp, lastId, pageSize);
            var mappedItems = _mapper.Map<List<LedModelDTO>>(repoPage.Items);
            var resultItems = new List<dynamic>();
            foreach (var item in mappedItems)
            {
                resultItems.Add(LED_MapToDynamic.MapToDynamic(item));
            }
            return new CursorPagedResult<dynamic>
            {
                Items = resultItems,
                NextCursor = repoPage.NextCursor,    
                HasNextPage = repoPage.HasNextPage,
                PageSize = repoPage.PageSize
            };
        }

        public async Task<CursorPagedResult<dynamic>> GetLedModelsByDevice(string line, string devicename, int? lastId = null, int pageSize = 20)
        {
            int? lineId = await _LineRepository.GetIdByLineName(line);
            if (!lineId.HasValue)
                throw new NotFoundException($"Line with name '{line}' was not found.");

            int? deviceId = await _LedRepository.GetDeviceIdByDeviceNameAndLineNameAsync(devicename, (int)lineId);
            if (!deviceId.HasValue)
                throw new NotFoundException($"Device with name '{devicename}' in line '{line}' was not found.");
            var repoResult = await _LedModelRepository.GetLedModelsByDeviceIdAsync((int)deviceId, lastId, pageSize);
            var mappedItems = _mapper.Map<List<LedModelDTO>>(repoResult.Items);
            var dynamicItems = mappedItems
                .Select(item => LED_MapToDynamic.MapToDynamic(item))
                .ToList();
            return new CursorPagedResult<dynamic>
            {
                Items = dynamicItems,
                NextCursor = repoResult.NextCursor,
                HasNextPage = repoResult.HasNextPage,
                PageSize = repoResult.PageSize
            };
        }

        public async Task<dynamic> GetLedModelById(int id)
        {
            var model = await _LedModelRepository.GetByIDAsync(id);
            if (model == null)
                throw new NotFoundException($"Model with ID {id} was not found!");
            var mapped = _mapper.Map<LedModelDTO>(model);
            return LED_MapToDynamic.MapToDynamic(mapped);
        }
    }
}
