using Application.DTOs.RequestDTOs;
using Application.DTOs.ResponseDTOs;
using Application.IRepositories;

namespace Application.IServices
{
    public interface IModelGroupService
    {
        public Task<ModelGroupResponse> AddModelGroupAsync(ModelGroupRequest request);
    }
}
