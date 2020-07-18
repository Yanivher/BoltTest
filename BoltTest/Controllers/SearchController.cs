using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BoltTest.Controllers
{
    public class SearchController : ApiController
    {
        // GET api/values/5
        public IHttpActionResult Get(string term)
        {
            try
            {
                var response = new Response();
                response.Items = new List<Item>();

                if (!string.IsNullOrEmpty(term))
                {
                    var items = MemoryCacher.GetValue("results") as List<Item>;
                    if (items != null && items.Any(e => e.Term == term))
                    {
                        response.Items = items;
                    }
                    else
                    {
                        response.Items.AddRange(DataProvider.GetSearchProvider(ProviderType.Google).Search(term));
                        //response.Items.AddRange(DataProvider.GetSearchProvider(ProviderType.Bing).Search(term));

                        response.Items.ForEach(i => i.Term = term);

                        MemoryCacher.Add("results", response.Items, DateTimeOffset.UtcNow.AddYears(1));
                    }
                }

                return Json(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed getting result for term: {term}", ex);
            }
        }
    }
}
