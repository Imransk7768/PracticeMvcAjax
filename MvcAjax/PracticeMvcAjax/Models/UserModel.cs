using System;
using System.ComponentModel.DataAnnotations;

namespace PracticeMvcAjax.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string WebSite { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string AltEmail { get; set; }
    }
}
