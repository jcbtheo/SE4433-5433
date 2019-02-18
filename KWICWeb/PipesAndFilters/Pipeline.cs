using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.PipesAndFilters
{
    public class Pipeline
    {
        private List<IFilter> filters = new List<IFilter>();

        public Pipeline Register(IFilter newFilter)
        {
            filters.Add(newFilter);
            if(filters.Count > 1)
            {
                MemoryStream streamPipe = new MemoryStream();
                filters[filters.Count - 2].SetOutput(streamPipe);
                filters[filters.Count - 1].SetInput(streamPipe);
            }
            return this;
        }

        public void Run()
        {
            foreach(IFilter filter in filters)
            {
                while(filter.IsComplete == false)
                {
                    filter.Filter();
                }
            }
        }
    }
}
