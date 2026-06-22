using Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository.LED
{
    public interface ILedRepository:IGenericRepository<Domain.Entities.LED.LED>
    {
        public Task<int?> GetDeviceIdByDeviceNameAndLineNameAsync(string deviceName, int lineId);
        public Task<string?> GetDeviceNameByIdAsync(int id);
        public Task<List<Domain.Entities.LED.LED>> GetLedsByLineId(int lineId);
    }
}
