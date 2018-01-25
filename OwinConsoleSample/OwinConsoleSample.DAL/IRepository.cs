namespace OwinConsoleSample.DAL
{
    public interface IRepository
    {
        object Save<T>(T tt);
    }
}
