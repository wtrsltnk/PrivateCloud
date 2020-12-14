using System;

namespace PrivateCloud.Practises.Logging
{
    public sealed class NullLogger :
        ILogger
    {
        public void Log(
            LogLevels level,
            string message)
        {
            // do nothing
        }

        public ILogger WithException(
            Exception exception)
        {
            return this;
        }

        public ILogger WithField(
            string key,
            object value)
        {
            return this;
        }

        #region IDisposable Implementation
        public void Dispose()
        {
            // do nothing
        }
        #endregion
    }
}
