using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class CursorPaged<T>
    {
        public List<T> Items { get; set; }
        public object? NextCursor { get; set; }
        public bool HasNextPage { get; set; }
        public int PageSize { get; set; }
    }
}
