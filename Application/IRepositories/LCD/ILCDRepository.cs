
namespace Application.IRepositories.LCD
{
    public interface ILCDRepository
    {
        public Task<Domain.Enitties.LCD.LCD> GetLCDById(int id);

    }
}
