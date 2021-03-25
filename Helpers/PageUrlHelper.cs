using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Warehouse.Helpers
{
    public class PageUrlHelper {
        public static String BuildPageQuery(String url, int page, IQueryCollection queries) {
            Dictionary<String, StringValues> query = new Dictionary<String, StringValues>();

            foreach (var pair in queries) {
                if (pair.Key != "page") {
                    query.Add(pair.Key, pair.Value);
                }
            }

            var pageQuery = new StringValues(page.ToString());
            query.Add("page", pageQuery);

            return QueryHelpers.AddQueryString(url, query);
        } 
    }
}