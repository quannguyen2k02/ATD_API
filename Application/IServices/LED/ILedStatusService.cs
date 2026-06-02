using Application.DTOs.RequestDTOs.LED;
using Application.DTOs.ResponseDTOs.LED;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices.LED
{
    public interface ILedStatusService
    {
        public Task<LedDeviceStatusResponse> AddNewStatus(LedDeviceStatusRequest ledDeviceStatusRequest);
    }
}
