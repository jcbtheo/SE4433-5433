using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.PipesAndFilters
{
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> item);
    }
}
