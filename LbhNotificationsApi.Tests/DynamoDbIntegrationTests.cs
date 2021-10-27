using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace LbhNotificationsApi.Tests
{
    public class DynamoDbIntegrationTests<TStartup> where TStartup : class
    {
        private HttpClient Client { get; set; }
        private readonly DynamoDbMockWebApplicationFactory<TStartup> _factory;
        protected IDynamoDBContext DynamoDbContext => _factory?.DynamoDbContext;
        private List<Action> CleanupActions { get; }

        private readonly List<TableDef> _tables = new List<TableDef>
        {
            // TODO: Populate the list of table(s) and their key property details here, for example:
            new TableDef { Name = "notifications", KeyName = "target_id", KeyType = ScalarAttributeType.S }
        };

        protected DynamoDbIntegrationTests()
        {
            EnsureEnvVarConfigured("DynamoDb_LocalMode", "true");
            EnsureEnvVarConfigured("DynamoDb_LocalServiceUrl", "http://localhost:8000");
            _factory = new DynamoDbMockWebApplicationFactory<TStartup>(_tables);
            Client = _factory.CreateClient();
            CleanupActions = new List<Action>();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed) return;
            _factory?.Dispose();
            _disposed = true;
        }
        private static void EnsureEnvVarConfigured(string name, string defaultValue)
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(name)))
                Environment.SetEnvironmentVariable(name, defaultValue);
        }



    }

    public class TableDef
    {
        public string Name { get; set; }
        public string KeyName { get; set; }
        public ScalarAttributeType KeyType { get; set; }
    }
}
