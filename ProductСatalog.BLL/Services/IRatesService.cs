using ProductCatalog.BLL.Models;

namespace ProductCatalog.BLL.Services
{
    public interface IRatesService
    {
        Task<RateModel?> GetRateForTodayAsync(int innerCurrencyCode);
    }
}
