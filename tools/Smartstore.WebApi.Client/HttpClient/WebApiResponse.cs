﻿using Newtonsoft.Json.Linq;
using Smartstore.WebApi.Client.Models;

namespace Smartstore.WebApi.Client
{
    public class WebApiResponse
    {
        public string RequestContent { get; set; }

        public string Status { get; set; }
        public string Headers { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }

        /// <seealso cref="http://weblog.west-wind.com/posts/2012/Aug/30/Using-JSONNET-for-dynamic-JSON-parsing" />
        /// <seealso cref="http://james.newtonking.com/json/help/index.html?topic=html/QueryJsonDynamic.htm" />
        /// <seealso cref="http://james.newtonking.com/json/help/index.html?topic=html/LINQtoJSON.htm" />
        public List<Customer> ParseCustomers()
        {
            if (string.IsNullOrWhiteSpace(Content))
            {
                return null;
            }

            //        dynamic dynamicJson = JObject.Parse(response.Content);

            //        foreach (dynamic customer in dynamicJson.value)
            //        {
            //            string str = string.Format("{0} {1} {2}", customer.Id, customer.CustomerGuid, customer.Email);
            //str.Dump();
            //        }

            var json = JObject.Parse(Content);
            string metadata = (string)json["@odata.context"];

            if (!string.IsNullOrWhiteSpace(metadata) && metadata.EndsWith("#Customers"))
            {
                var customers = json["value"].Select(x => x.ToObject<Customer>()).ToList();
                return customers;
            }

            return null;
        }
    }
}