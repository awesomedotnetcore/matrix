using System;
using System.ComponentModel.DataAnnotations;

namespace Matrix.Agent.Directory.Api.Model
{
    public class UpdateUserProfileModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}