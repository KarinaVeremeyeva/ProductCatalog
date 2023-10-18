using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.DTOs;
using ProductCatalog.BLL.Services;

namespace ProductCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IRatesService _ratesService;
        private readonly IMapper _mapper;

        public RatesController(IRatesService ratesService, IMapper mapper)
        {
            _ratesService = ratesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRateForToday()
        {
            var dollarCurrencyId = 431;

            var rate = await _ratesService.GetRateForTodayAsync(dollarCurrencyId);
            var rateDto = _mapper.Map<RateDto>(rate);

            return Ok(rateDto);
        }
    }
}
