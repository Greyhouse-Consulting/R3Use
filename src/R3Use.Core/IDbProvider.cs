using NPoco;

namespace R3Use.Core
{
    public interface IDbProvider
    {
        IDatabase Create();
    }
}