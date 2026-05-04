using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class CursorPagedResult<T>
    {
        public List<T> Items { get; set; }
        public int? NextCursor { get; set; }
        public bool HasNextPage { get; set; }
        public int PageSize { get; set; }
    }
}
