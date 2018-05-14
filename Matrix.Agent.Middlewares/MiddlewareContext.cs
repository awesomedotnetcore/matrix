namespace Matrix.Agent.Middlewares
{
    public class MiddlewareContext : IMiddlewareContext
    {
        public string Connection { get; }

        public MiddlewareContext(string connection)
        {
            Connection = connection;
        }
    }
}