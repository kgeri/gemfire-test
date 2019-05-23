using System;
using System.Collections.Generic;
using Apache.Geode.Client;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            var properties = new Properties<string, string>();
            properties.Insert("security-client-auth-library", "client");
            properties.Insert("security-client-auth-factory", "client.DummyAuthInitialize.Create");

            var cacheFactory = CacheFactory.CreateCacheFactory(properties)
                .AddServer("localhost", 2000)
                .SetPdxReadSerialized(false)
                .SetSubscriptionEnabled(true)
                .SetMinConnections(1)
                .SetMaxConnections(2);
            var cache = cacheFactory.Create();

            string regionName = "Test";
            var region = cache.CreateRegionFactory(RegionShortcut.PROXY).Create<string, string>(regionName);

            var values = new Dictionary<string, string>();
            region.GetAll(region.Keys, values, new Dictionary<string, Exception>());
            foreach (var value in values)
            {
                Console.Out.WriteLine($"[get] {value}");
            }

            cache.Close();
        }
    }

    class DummyAuthInitialize : IAuthInitialize
    {
        public static IAuthInitialize Create()
        {
            Console.Out.WriteLine("[init] DummyAuthInitialize");
            return new DummyAuthInitialize();
        }

        public void Close() { }

        public Properties<string, object> GetCredentials(Properties<string, string> props, string server)
        {
            var creds = new Properties<string, object>();
            creds.Insert("secret", "MY SUPER SECRET");
            Console.Out.WriteLine("[init] GetCredentials");
            return creds;
        }
    }
}
