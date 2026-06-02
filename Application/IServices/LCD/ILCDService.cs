using Application.DTOs.ResponseDTOs.LCD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices.LCD
{
    public interface ILCDService
    {
        public Task<LCDResponse> GetLCDByIdAsync(int id);
    }
}
