using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.PipesAndFilters
{
    public interface IFilter
    {
        bool IsComplete { get; }

        void SetInput(MemoryStream inputStream);

        void SetOutput(MemoryStream OutputStream);

        void Filter();
    }
}
