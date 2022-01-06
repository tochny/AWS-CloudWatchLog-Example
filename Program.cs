// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using NLog;
using NLog.Targets;
using NLog.Config;

using NLog.AWS.Logger;

namespace TestLog
{
    class CloudWatchTest
    {
        static void Main(string[] args)
        {
            ConfigureNLog();

            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Error("Check the AWS Console CloudWatch Logs console in us-west-2");
            logger.Info("to see messages in the log streams for the");
            logger.Trace("trace test");
            logger.Debug("trace test");
            logger.Warn("Warn test");
            logger.Fatal("Fatal test");
        }

        private static void ConfigureNLog()
        {
            var config = new LoggingConfiguration();

            var consoleTarget = new ColoredConsoleTarget{
                Layout = @"${date:format=yyyy-MM-dd HH\:mm\:ss.fff} [${level:uppercase=true}] ${message} ${exception:format=tostring}"
            };
            config.AddTarget("console", consoleTarget);

            var awsTarget = new AWSTarget()
            {
                LogGroup = "testGroup",
                Region = "us-west-2",
                Layout = @"${date:format=yyyy-MM-dd HH\:mm\:ss.fff} [${level:uppercase=true}] ${message} ${exception:format=tostring}",
                LogStreamNamePrefix = "",
                DisableLogGroupCreation = false
                // BatchPushInterval = 3 // the default is 3, configurable
            };
            config.AddTarget("aws", awsTarget);

            // Fatal, Error, Warn, Info, Trace
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, consoleTarget));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, awsTarget));

            LogManager.Configuration = config;
        }
    }
}
