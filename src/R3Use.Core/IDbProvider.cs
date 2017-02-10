namespace NPoco.Core
{
    public interface IDbProvider
    {
        IDatabase Create();
    }
}