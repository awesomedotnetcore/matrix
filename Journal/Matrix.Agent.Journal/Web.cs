using Nancy;

namespace Matrix.Agent.Journal
{
    public class Web : NancyModule
    {
        public Web()
        {
            Get["/"] = _ => View["index"];
        }
    }
}