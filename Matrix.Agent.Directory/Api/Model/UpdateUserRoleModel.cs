using System;
using System.ComponentModel.DataAnnotations;

namespace Matrix.Agent.Directory.Api.Model
{
    public class UpdateUserRoleModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}