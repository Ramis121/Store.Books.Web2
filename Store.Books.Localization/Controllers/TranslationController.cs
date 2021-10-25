using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Books.Localization.Model;
using Store.Books.Localization.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Books.Localization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationController : ControllerBase
    {
        private readonly ITranslationRepository _repository;
        private readonly ILocaleRepository _localeRepository;
        private readonly ILogger<TranslationController> _logger;
        public TranslationController(ILocaleRepository localeRepository,
            ITranslationRepository repository,
            ILogger<TranslationController> logger)
        {
            _repository = repository;
            _localeRepository = localeRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Translation>> Get()
        {
            return await _repository.Get(includeProperties: "Locale");
        }

        [HttpGet("byCode")]
        public async Task<IEnumerable<Translation>> Get(string lang)
        {
            return await _repository.Get(p => p.Lang == lang, includeProperties: "Locale");
        }
    }
}
