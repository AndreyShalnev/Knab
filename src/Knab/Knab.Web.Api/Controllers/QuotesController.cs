using System.Collections.Generic;
using Knab.Data;
using Knab.Data.Provider;
using Microsoft.AspNetCore.Mvc;

namespace Knab.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        
        public QuotesController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Quote>> GetLatestQuotes(string cryptoCode)
        {
            if (string.IsNullOrWhiteSpace(cryptoCode))
                return BadRequest("Crypto currency code can not be null.");

            return Ok(_quoteService.GetQuotes(cryptoCode));
        }
    }
}
