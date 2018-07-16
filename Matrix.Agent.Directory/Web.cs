using Nancy;

namespace Matrix.Agent.Directory
{
    public class Web : NancyModule
    {
        public Web()
        {
            Get["/"] = _ => View["index"];
        }
    }
}