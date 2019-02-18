using System.IO;
using System.Collections.Generic;

namespace KWICWeb.PipesAndFilters
{
    public class Pipeline
    {
        private List<IFilter> filters = new List<IFilter>();

        public Pipeline Register(IFilter newFilter)
        {
            filters.Add(newFilter);
            if (filters.Count > 1)
            {
                // Set MemoryStream pipe between filters
                MemoryStream streamPipe = new MemoryStream();
                filters[filters.Count - 2].SetOutput(streamPipe);
                filters[filters.Count - 1].SetInput(streamPipe);
            }
            return this;
        }

        public void Run()
        {
            foreach (IFilter filter in filters)
            {
                filter.Filter();
            }
        }
    }
}
