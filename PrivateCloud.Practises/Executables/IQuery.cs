namespace PrivateCloud.Practises.Executables
{
    public interface IQuery<in TModel, out TResult>
        where TModel : class, new()
        where TResult : class
    {
        TResult Execute(TModel model);
    }
}
