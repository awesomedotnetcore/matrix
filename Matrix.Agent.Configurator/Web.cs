using Nancy;

namespace Matrix.Agent.Configurator
{
    public class Web : NancyModule
    {
        public Web()
        {
            Get["/"] = _ => View["index"];
        }
    }
}