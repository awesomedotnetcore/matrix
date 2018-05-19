using System;

namespace Matrix.Agent.Configurator.Model
{
    public class Application
    {
        public Guid Id { get; set; }

        public bool Enabled { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}