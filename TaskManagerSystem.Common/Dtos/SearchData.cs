using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerSystem.Common.Dtos
{
    public class SearchData<T>
        where T : class
    {
        public IReadOnlyList<T> Data { get; set; }
        public int Count { get; set; }

        public SearchData(IReadOnlyList<T> data, int count)
        {
            Data = data;
            Count = count;
        }
    }
}
