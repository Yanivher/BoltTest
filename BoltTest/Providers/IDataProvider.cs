using System.Collections.Generic;

namespace BoltTest.Controllers
{
    public interface IDataProvider
    {
        List<Item> Search(string term);
    }
}
