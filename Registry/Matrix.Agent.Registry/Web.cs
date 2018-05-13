using Nancy;

namespace Matrix.Agent.Registry
{
    public class Web : NancyModule
    {
        public Web()
        {
            Get["/"] = _ => View["index"];
        }
    }
}