namespace PrivateCloud.Practises.Logging
{
    public static class LoggingExtensions
    {
        public static void Trace(
           this ILogger logger,
           string message)
        {
            logger.Log(
                LogLevels.Trace,
                message);
        }

        public static void Debug(
            this ILogger logger,
            string message)
        {
            logger.Log(
                LogLevels.Debug,
                message);
        }

        public static void Info(
            this ILogger logger,
            string message)
        {
            logger.Log(
                LogLevels.Info,
                message);
        }

        public static void Warn(
            this ILogger logger,
            string message)
        {
            logger.Log(
                LogLevels.Warning,
                message);
        }

        public static void Error(
            this ILogger logger,
            string message)
        {
            logger.Log(
                LogLevels.Error,
                message);
        }

        public static void Fatal(
            this ILogger logger,
            string message)
        {
            logger.Log(
                LogLevels.Fatal,
                message);
        }
    }
}
