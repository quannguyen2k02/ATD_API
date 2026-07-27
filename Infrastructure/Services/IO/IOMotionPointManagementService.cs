using Application.Common;
using Application.DTOs.RequestDTOs.IO;
using Application.DTOs.ResponseDTOs.IO;
using Application.IRepositories.IO;
using Application.IServices.IO;
using AutoMapper;
using Domain.Enitties.IO;
using Infrastructure.Exceptions;
using Infrastructure.ExternalServices.Mapper;

namespace Infrastructure.Services.IO
{
    public class IOMotionPointManagementService : IIOMotionPointManagementService
    {
        private readonly IIOMotionPointsManagementRepository _ioMotionPointsManagementRepository;
        private readonly IMapper _mapper;
        private readonly IIOModelRepository _modelRepository;
        private readonly IIORepository _ioRepository;
        public IOMotionPointManagementService(IIOMotionPointsManagementRepository ioMotionPointsManagementRepository, IMapper mapper, IIOModelRepository modelRepository, IIORepository iORepository)
        {
            _ioMotionPointsManagementRepository = ioMotionPointsManagementRepository;
            _mapper = mapper;
            _modelRepository = modelRepository;
            _ioRepository = iORepository;
        }
        public async Task<IOMotionPointsResponse> AddNewIOMotionPoints(IOMotionPointsRequest motionPointsRequest)
        {
            var IOModel = await _modelRepository.GetIOModelByModelNameAndDeviceID(motionPointsRequest.ModelName, motionPointsRequest.IOId);
            if (IOModel == null)//if not exist model, create new model. 
            {
                var io = await _ioRepository.GetByIDAsync(motionPointsRequest.IOId);
                if (io == null) throw new NotFoundException($"IO Id: {motionPointsRequest.IOId} was not found.");
                //throw new NotFoundException($"Model name: '{config.ModelName}' or IO Id: {config.IOId} was not found.");
                var model = new IOModel();
                model.ModelName = motionPointsRequest.ModelName;
                model.IOId = motionPointsRequest.IOId;
                model.CreateDate = DateTime.Now;
                var result = await _modelRepository.Add(model); // add new model.
                var mappedMotionPoint = _mapper.Map<MotionPointsManagement>(motionPointsRequest);
                mappedMotionPoint.IOModelId = result.Id; // set id of motion point
                mappedMotionPoint.CreateDate = DateTime.Now;
                return _mapper.Map<IOMotionPointsResponse>(await _ioMotionPointsManagementRepository.Add(mappedMotionPoint));
            }
            var motionPoint = _mapper.Map<MotionPointsManagement>(motionPointsRequest);
            motionPoint.IOModelId = IOModel.Id;
            motionPoint.CreateDate = DateTime.Now;
            return _mapper.Map<IOMotionPointsResponse>(await _ioMotionPointsManagementRepository.Add(motionPoint));
        }

        public async Task<CursorPagedResult<dynamic>> GetMotionPoints(int modelId, int? lastId = null, int pageSize = 20)
        {
            var io = await _modelRepository.GetByIDAsync(modelId);
            if (io == null) throw new NotFoundException($"Model Id: {modelId} was not found.");
            var repoPage = await _ioMotionPointsManagementRepository.GetIOMotionPoints(modelId, lastId, pageSize);
            var mappedItems = _mapper.Map<List<IOMotionPointsResponse>>(repoPage.Items);
            var resultItems = new List<dynamic>();
            foreach (var item in mappedItems)
            {
                resultItems.Add(IO_Mapper.Map_MotionPoint(item));
            }
            return new CursorPagedResult<dynamic>
            {
                Items = resultItems,
                NextCursor = repoPage.NextCursor,
                HasNextPage = repoPage.HasNextPage,
                PageSize = repoPage.PageSize
            };
        }
    }
}
