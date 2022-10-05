using Inforce.Repository;
using Microsoft.EntityFrameworkCore;

namespace Fragments.Test.Base
{
    internal static class ContextGenerator
    {
        internal static InforceContext GetContext()
        {
            var contextOptions = new DbContextOptionsBuilder<InforceContext>()
            .UseInMemoryDatabase(databaseName: "InforceDb")
            .Options;

            return new InforceContext(contextOptions);
        }
    }
}

