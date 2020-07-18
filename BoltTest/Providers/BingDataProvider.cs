using System;
using System.Collections.Generic;

namespace BoltTest.Controllers
{
    public class BingDataProvider : DataProvider
    {
        protected override string BaseUrl => throw new NotImplementedException();

        protected override ProviderType ProviderType => ProviderType.Bing;

        protected override List<Item> ParseResult(string html)
        {
            throw new NotImplementedException();
        }
    }
}
