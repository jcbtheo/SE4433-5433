using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.PipesAndFilters
{
    public class Pipeline<T>
    {
        public IEnumerable<T> output { get; private set; }
        List<IFilter<T>> filters = new List<IFilter<T>>();

        public Pipeline<T> Add(IFilter<T> newFilter)
        {
            filters.Add(newFilter);
            return this;
        }

        public void Run()
        {
            output = new List<T>();

            foreach(IFilter<T> filter in filters)
            {
                output = filter.Filter(output);
            }
        }
    }
}
