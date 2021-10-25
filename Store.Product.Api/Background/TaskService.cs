using ExcelDataReader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Store.Books.Infrastructure.Interfaces;
using Store.Common.Configs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Product.Api.Background
{
    public class TaskService : BackgroundService
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger _logger;
        private readonly ServiceConfig _config;
        private readonly IHttpClientFactory _factory;
        public TaskService(IServiceProvider provider,
            IHttpClientFactory factory,
            IOptions<ServiceConfig> config,
            ILogger<TaskService> logger)
        {
            _provider = provider;
            _logger = logger;
            _config = config?.Value;
            _factory = factory;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _provider.CreateScope();
            /*var books = scope.ServiceProvider.GetRequiredService<IBookRepository>();
            var prices = scope.ServiceProvider.GetRequiredService<IPriceRepository>();
            var authors = scope.ServiceProvider.GetRequiredService<IAuthorRepository>();

            /*while (!stoppingToken.IsCancellationRequested)
            {
                using var stream = File.Open(_config.ExcelFile, FileMode.Open, FileAccess.Read);
                using var reader = ExcelReaderFactory.CreateReader(stream);
                var counter = 0;
                /*var client = _factory.CreateClient();
                var str = client
                    .GetAsync("http://google.com")
                    .Result
                    .Content.ReadAsStringAsync().Result;*/
                /*
                do
                {
                    while (reader.Read())
                    {
                        if (counter == 0) { counter++; continue; }
                        var code = reader.GetString(0);
                        var description = reader.GetString(1);
                        var ruTranslaction = reader.GetString(2);
                        var kzTranslaction = reader.GetString(3);
                        var enTranslaction = reader.GetString(4);
                        var locale = (await locales.Get(p => p.Key == code)).FirstOrDefault();
                        if (locale is null)
                        {
                            await locales.Insert(new Locale { Key = code, Description = description });
                            locale = (await locales.Get(p => p.Key == code)).FirstOrDefault();
                        }
                        var ruExists = await translations.Get(p => p.Locale.Key == code && p.Lang == "ru", includeProperties: "Locale");
                        if (!ruExists.Any())
                            await translations.Insert(new Translation { Lang = "ru", Translate = ruTranslaction, Locale = locale });
                        //-
                        var kzExists = await translations.Get(p => p.Locale.Key == code && p.Lang == "kz", includeProperties: "Locale");
                        if (!kzExists.Any())
                            await translations.Insert(new Translation { Lang = "kz", Translate = kzTranslaction, Locale = locale });
                        //-
                        var enExists = await translations.Get(p => p.Locale.Key == code && p.Lang == "en", includeProperties: "Locale");
                        if (!enExists.Any())
                            await translations.Insert(new Translation { Lang = "en", Translate = enTranslaction, Locale = locale });
                    }
                } while (reader.NextResult());
                *
                Thread.Sleep(10 * 1000);
            }
            */
        }
    }
}
