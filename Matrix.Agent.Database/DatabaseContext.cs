namespace Matrix.Agent.Database
{
    public class DatabaseContext : IDatabaseContext
    {
        public string Connection { get; private set; }

        public DatabaseContext(string connection)
        {
            Connection = connection;
        }
    }
}