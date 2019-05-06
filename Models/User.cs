using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GYM.Models
{
    public class User
    {
        #region User properties

        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        #endregion
    }
}
