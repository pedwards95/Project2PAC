using System.ComponentModel.DataAnnotations;

namespace _2PAC.WebApp.WebAPIModel
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}