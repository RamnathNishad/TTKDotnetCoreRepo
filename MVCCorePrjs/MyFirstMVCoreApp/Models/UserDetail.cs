using Microsoft.Build.Framework;

namespace MyFirstMVCoreApp.Models
{
    public class UserDetail
    {
        [Required]
        public string Uname { get; set; }
        
        [Required]        
        public string Password { get; set; }
    }
}
