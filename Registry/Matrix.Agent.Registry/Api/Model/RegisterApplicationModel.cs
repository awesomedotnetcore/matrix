using System.ComponentModel.DataAnnotations;

namespace Matrix.Agent.Registry.Api.Model
{
    public class RegisterApplicationModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}