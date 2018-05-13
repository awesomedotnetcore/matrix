namespace Matrix.SDK.Middlewares
{
    public interface IMiddlewareContext
    {
        string Connection { get; }
    }

    public class MiddlewareContext : IMiddlewareContext
    {
        public string Connection { get; private set; }

        public MiddlewareContext(string connection)
        {
            Connection = connection;
        }
    }
}