using Application.DTOs.RequestDTOs.LCD;
using Domain.Enitties.LCD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices.LCD
{
    public interface ILCDResultService
    {
        Task<(int inserted, int skipped)> AddBatchAsync(IEnumerable<RequestLCDResult> batch);
    }
}
