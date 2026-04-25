using Application.Common;
using Domain.Enitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface ILineRepository
    {

        public Task<int?> GetIdByLineName(string name);
        public Task<string?> GetLineNameById(int id);
        public Task<List<Line>> GetAllLinesAsync();
    }
}
