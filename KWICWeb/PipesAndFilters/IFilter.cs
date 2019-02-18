using System.IO;

namespace KWICWeb.PipesAndFilters
{
    public interface IFilter
    {
        void SetInput(MemoryStream inputStream);

        void SetOutput(MemoryStream OutputStream);

        void Filter();
    }
}
