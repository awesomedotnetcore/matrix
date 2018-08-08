using Nancy;

namespace Matrix.Agent.Postman
{
    public class Web : NancyModule
    {
        public Web()
        {
            Get["/"] = _ => View["index"];
        }
    }
}