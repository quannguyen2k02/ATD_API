using Domain.Enitties.LED;

namespace Application.IRepositories.LED
{
    public interface ILedStatusRepository
    {
        public Task<LedDeviceStatus> AddNewStatus(LedDeviceStatus ledDeviceStatus);
    }
}
