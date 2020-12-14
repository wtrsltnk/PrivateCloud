using System;

namespace PrivateCloud.Practises.Executables
{
    public static class ExecutablesExtensions
    {
        public static bool TryExecute<TModel, TResult>(
           this IQuery<TModel, TResult> query,
           TModel model,
           out TResult result)
            where TModel : class, new()
            where TResult : class
        {
            Exception exception;

            return TryExecute(
                query,
                model,
                out result,
                out exception);
        }

        public static bool TryExecute<TModel, TResult>(
            this IQuery<TModel, TResult> query,
            TModel model,
            out TResult result,
            out Exception exception)
            where TModel : class, new()
            where TResult : class
        {
            try
            {
                result = query.Execute(model);

                exception = null;

                return true;
            }
            catch (Exception ex)
            {
                result = default(TResult);

                exception = ex;

                return false;
            }
        }

        public static bool TryExecute<TModel, TResult>(
            this ICommand<TModel, TResult> command,
            TModel model,
            out TResult result)
            where TModel : class, new()
            where TResult : class
        {
            Exception exception;

            return TryExecute(
                command,
                model,
                out result,
                out exception);
        }

        public static bool TryExecute<TModel, TResult>(
            this ICommand<TModel, TResult> command,
            TModel model,
            out TResult result,
            out Exception exception)
            where TModel : class, new()
            where TResult : class
        {
            try
            {
                result = command.Execute(model);

                exception = null;

                return true;
            }
            catch (Exception ex)
            {
                result = default(TResult);

                exception = ex;

                return false;
            }
        }

        public static bool TryExecute<TModel>(
            this ICommand<TModel> command,
            TModel model)
            where TModel : class, new()
        {
            Exception exception;

            return TryExecute(
                command,
                model,
                out exception);
        }

        public static bool TryExecute<TModel>(
            this ICommand<TModel> command,
            TModel model,
            out Exception exception)
            where TModel : class, new()
        {
            try
            {
                command.Execute(model);

                exception = null;

                return true;
            }
            catch (Exception ex)
            {
                exception = ex;

                return false;
            }
        }
    }
}