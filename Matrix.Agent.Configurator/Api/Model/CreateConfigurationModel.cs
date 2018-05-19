using System;

namespace Matrix.Agent.Configurator.Api.Model
{
    public class CreateConfigurationModel
    {
        public Guid Application { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}