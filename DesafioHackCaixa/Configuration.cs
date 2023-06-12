using Azure.Messaging.EventHubs.Producer;

namespace MinhaApi
{
    public class Configuration
    {
        public static string sqlConnection;
        public static EventHubConfiguration eventConfig = new();

        public class EventHubConfiguration
        {
            public string connectionString { get; set; }
            public string eventHubName { get; set; }
        }
    }
}
