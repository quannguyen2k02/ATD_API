using Application.Common;
using Application.DTOs.RequestDTOs.LED;
using Application.DTOs.ResponseDTOs.LED;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices.LED
{
    public interface ILedConfigService
    {
        public Task<LedConfigDTO> AddLedConfigAsync(LedConfigDTO ledConfigDTO);
        // Keyset pagination: client supplies lastId (id of last item from previous page). If null, return first page.
        public Task<PagedResult<LedConfigResponse>> GetLedConfigByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20);
    }
}
