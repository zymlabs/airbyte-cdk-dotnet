using System;
using Airbyte.Cdk.Models;
using Type = Airbyte.Cdk.Models.Type;

namespace Airbyte.Cdk
{
    /// <summary>
    /// Airbyte Logger
    /// </summary>
    public class AirbyteLogger
    {
        public void Log(Level level, string message)
        {
            var airbyteLogMessage = new AirbyteLogMessage {Level = level, Message = message};
            var airbyteMessage = new AirbyteMessage {Type = Type.Log, Log = airbyteLogMessage};
            AirbyteEntrypoint.ToConsole(airbyteMessage);
        }

        public void Fatal(string message) => Log(Level.Fatal, message);

        public void Exception(Exception exception) => Log(Level.Error, $"{exception.Message}{Environment.NewLine}{exception.StackTrace}");

        public void Error(string message) => Log(Level.Error, message);

        public void Warn(string message) => Log(Level.Warn, message);

        public void Info(string message) => Log(Level.Info, message);

        public void Debug(string message) => Log(Level.Debug, message);

        public void Trace(string message) => Log(Level.Trace, message);
    }
}
