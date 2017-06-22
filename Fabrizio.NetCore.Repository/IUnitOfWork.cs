namespace Fabrizio.NetCore.GenericRepository
{
    public interface IUnitOfWork
    {
        void Commit();
        void Dispose();
    }
}