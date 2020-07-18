using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using System.Xml;
using System.Xml.XPath;

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
                    response.Items.AddRange(DataProvider.GetSearchProvider(ProviderType.Google).Search(term));
                    //response.Items.AddRange(DataProvider.GetSearchProvider(ProviderType.Bing).Search(term));
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
