using Application.Common;
using Application.DTOs.ResponseDTOs.LCD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories.LCD
{
    public interface ILCDResultDemoService
    {
        public Task<CursorPaged<ResponseLCDResult>> GetLCDResultDemoAsync(DateTime? fromDate, DateTime? toDate, string? modelName, int deviceId, int? lastId = null, int pageSize = 20);
    }
}
