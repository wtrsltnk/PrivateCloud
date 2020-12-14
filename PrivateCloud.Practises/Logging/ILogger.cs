using System;

namespace PrivateCloud.Practises.Logging
{
    public interface ILogger :
        IDisposable
    {
        ILogger WithField(
            string key,
            object value);

        ILogger WithException(
            Exception exception);

        void Log(
            LogLevels level,
            string message);
    }
}
