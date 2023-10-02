using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyFirstMVCoreApp.Models
{
    public class Register
    {
        public string uname { get; set; }
        public string password { get; set; }
        public string gender { get; set; }  
        public string city { get; set; }
        public List<SelectListItem> cities { get; set; }

        public List<string> hobbies { get; set; }
        public Dictionary<string, string> hobbiesForSelection { get;set; }
    }
}
