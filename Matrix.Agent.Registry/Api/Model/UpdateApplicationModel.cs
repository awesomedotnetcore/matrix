using System;
using System.ComponentModel.DataAnnotations;

namespace Matrix.Agent.Registry.Api.Model
{
    public class UpdateApplicationModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}