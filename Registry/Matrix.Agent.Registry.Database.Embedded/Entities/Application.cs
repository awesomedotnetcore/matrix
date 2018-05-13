using System;

namespace Matrix.Agent.Registry.Database.Embedded.Entities
{
    public class Application
    {
        public Guid Id { get; set; }

        public bool Enabled { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public bool Deleted { get; set; }
    }
}