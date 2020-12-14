namespace PrivateCloud.Practises.Executables
{
    public interface ICommand<in TModel>
        where TModel : class, new()
    {
        void Execute(TModel model);
    }

    public interface ICommand<in TModel, out TResult>
        where TModel : class, new()
        where TResult : class
    {
        TResult Execute(TModel model);
    }
}