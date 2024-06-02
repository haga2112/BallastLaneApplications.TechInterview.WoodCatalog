using Bogus;
using WoodCatalog.Domain.Models;

namespace WoodCatalog.Tests.Fakers
{
    public class WoodTestsFaker : IDisposable
    {
        public static Faker<Wood> GenerateRandom()
        {
            var scopeFaker = new Faker<Wood>();

            scopeFaker.RuleFor(u => u.Id, (f, u) => $"wood:{Guid.NewGuid}");
            scopeFaker.RuleFor(u => u.Name, (f, u) => f.Name.FirstName());

            return scopeFaker;
        }

        public void Dispose()
        {
        }
    }
}
