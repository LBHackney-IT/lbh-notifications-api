using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using LbhNotificationsApi.V1.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;

namespace LbhNotificationsApi.Tests.V1.Infrastructure
{
    [TestFixture]
    public class DynamoDbInitilisationExtensionsTests
    {
        [TestCase(null)]
        [TestCase("false")]
        [TestCase("true")]
        public void ConfigureDynamoDbTestNoLocalModeEnvVarUsesAwsService(string localModeEnvVar)
        {
            Environment.SetEnvironmentVariable("DynamoDb_LocalMode", localModeEnvVar);

            var services = new ServiceCollection();
            services.ConfigureDynamoDb();

            services.Any(x => x.ServiceType == typeof(IAmazonDynamoDB)).Should().BeTrue();
            services.Any(x => x.ServiceType == typeof(IDynamoDBContext)).Should().BeTrue();

            Environment.SetEnvironmentVariable("DynamoDb_LocalMode", null);
        }
    }
}
