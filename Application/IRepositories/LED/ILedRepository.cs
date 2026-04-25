using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository.LED
{
    public interface ILedRepository
    {
        public Task<int?> GetDeviceIdByDeviceNameAndLineNameAsync(string deviceName, int lineId);
        public Task<string?> GetDeviceNameByIdAsync(int id);
    }
}
