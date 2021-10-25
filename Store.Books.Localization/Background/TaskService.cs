using ExcelDataReader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Store.Books.Localization.Model;
using Store.Books.Localization.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Books.Localization.Background
{
    public class TaskService : BackgroundService
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger _logger;
        public TaskService(IServiceProvider provider,
            ILogger<TaskService> logger)
        {
            _provider = provider;
            _logger = logger;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _provider.CreateScope();
            var locales = scope.ServiceProvider.GetRequiredService<ILocaleRepository>();
            var translations = scope.ServiceProvider.GetRequiredService<ITranslationRepository>();
            while (!stoppingToken.IsCancellationRequested)
            {
                using var stream = File.Open("localization.xlsx", FileMode.Open, FileAccess.Read);
                using var reader = ExcelReaderFactory.CreateReader(stream);
                var counter = 0;
                do
                {
                    while (reader.Read())
                    {
                        var ss = "a1|a2|a3".Split('|');
                        var s = "123.3";
                        decimal price = Convert.ToDecimal(s);
                        int df = Convert.ToInt32("23");
                        long sd =Convert.ToInt64("23");

                        var str1 = string.Format("{0}{1}", ss[0], s);
                        var xtr = $"{ss[0]}{s}";
                        
                        var sb = new StringBuilder()
                            .AppendLine("asdad").Append("asasdasd").Append("asdasd").ToString();
                        
                        var str = ss[0] + s;

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
                Thread.Sleep(10 * 1000);
            }
            ///return Task.CompletedTask;
        }
    }
}
