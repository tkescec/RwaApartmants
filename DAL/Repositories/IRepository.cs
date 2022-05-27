namespace DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        string CS { get; }
    }
}
