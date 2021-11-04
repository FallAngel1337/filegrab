using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FileGrab
{
	public class Logging
	{
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Setup(string path)
        {
            try
            {
                var config = new NLog.Config.LoggingConfiguration();

                var logfile = new NLog.Targets.FileTarget("logfile") { FileName = $"{path}/logs.txt" };

                config.AddRule(LogLevel.Info, LogLevel.Info, logfile);

                NLog.LogManager.Configuration = config;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Could not setup the log configuration!");
            }
        }

        public static void Log(string format, string filename, Regex regex = null)
        {
            try
            {
                if (regex != null)
                {
                    if (regex.IsMatch(filename))
                    {
                        Logger.Info(string.Format(format, filename));
                    }
                }
                else
                {
                    Logger.Info(string.Format(format, filename));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Some unexpected error occurred");
            }
        }
    }
}
