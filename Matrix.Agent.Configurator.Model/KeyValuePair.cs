using System;

namespace Matrix.Agent.Configurator.Model
{
    public class KeyValuePair
    {
        public Guid Id { get; set; }

        public Guid Application { get; set; }

        public bool Enabled { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}