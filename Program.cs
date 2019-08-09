using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using Elasticsearch.Net;
using Nest;

namespace TestNest
{
    class Program
    {

        static void Main(string[] args)
        {
            ElasticClient client;
            NameValueCollection configSection;

            configSection = (NameValueCollection)ConfigurationManager.GetSection("elasticsearch/core");

            var connectionSetting = new ConnectionSettings(new SingleNodeConnectionPool(new Uri(configSection.Get("url"))));
            connectionSetting.DisableDirectStreaming(Convert.ToBoolean(configSection.Get("view-raw-request")));

            //connectionSetting.ThrowExceptions();

            var username = configSection.Get("username");
            var password = configSection.Get("password");

            connectionSetting.BasicAuthentication(username, password);

            client = new ElasticClient(connectionSetting);
            var result = client.Indices.Exists("ms-core-dev");
        }
    }
}
