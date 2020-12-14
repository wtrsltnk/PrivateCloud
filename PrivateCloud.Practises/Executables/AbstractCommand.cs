using PrivateCloud.Practises.Validation;

namespace PrivateCloud.Practises.Executables
{
    public abstract class AbstractCommand<TModel> :
            ICommand<TModel>
            where TModel : class, new()
    {
        public void Execute(TModel model)
        {
            ExecutePreConditions(model);

            InternalExecute(model);
        }

        protected abstract void InternalExecute(
            TModel model);

        protected virtual void ExecutePreConditions(
            TModel model)
        {
            ValidationUtil.Validate(model);
        }
    }

    public abstract class AbstractCommand<TModel, TResult> :
        ICommand<TModel, TResult>
        where TModel : class, new()
        where TResult : class
    {
        public TResult Execute(
            TModel model)
        {
            ExecutePreConditions(model);

            var result = InternalExecute(model);

            ExecutePostConditions(result);

            return result;
        }

        protected abstract TResult InternalExecute(
            TModel model);

        protected virtual void ExecutePreConditions(
            TModel model)
        {
            ValidationUtil.Validate(model);
        }

        protected virtual void ExecutePostConditions(
            TResult result)
        {
            ValidationUtil.Validate(result);
        }
    }
}