namespace PrivateCloud.Practises.Mapping
{
    public interface IMapper<TSource, TTarget>
    {
        TTarget Map(
            TSource source);
    }
}
