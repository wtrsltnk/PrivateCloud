using PrivateCloud.Practises.Validation;

namespace PrivateCloud.Practises.Executables
{
    public abstract class AbstractQuery<TModel, TResult> :
         IQuery<TModel, TResult>
         where TModel : class, new()
         where TResult : class
    {
        public TResult Execute(TModel model)
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