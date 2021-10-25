namespace Store.Books.Localization.Services.Repositories
{
    using Infrastructure.Repos;
    using Model;
    using Services;
    using Interfaces;

    public class LocaleRepository : GenericRepository<Locale>, ILocaleRepository
    {
        public LocaleRepository(LocalizationDbContext context) : base(context) { }
    }
}
