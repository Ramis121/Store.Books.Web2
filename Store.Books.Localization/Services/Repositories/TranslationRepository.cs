namespace Store.Books.Localization.Services.Repositories
{
    using Infrastructure.Repos;
    using Model;
    using Services;
    using Interfaces;

    public class TranslationRepository : GenericRepository<Translation>, ITranslationRepository
    {
        public TranslationRepository(LocalizationDbContext context) : base(context) { }
    }
}
