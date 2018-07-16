using System;
using System.ComponentModel.DataAnnotations;

namespace Matrix.Agent.Directory.Api.Model
{
    public class CreateUserModel
    {
        [Required]
        public Guid Application { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

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