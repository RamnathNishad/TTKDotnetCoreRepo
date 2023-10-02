using System.ComponentModel.DataAnnotations;

namespace ScaffFoldCRUDDemo.Models
{
    public class Employee
    {
        [Required]
        [Key]
        public int Ecode { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 5)]
        [RegularExpression("[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*", ErrorMessage = "employee name must be only alphabets, no special characters allowed")]
        public string Ename { get; set; }

        [Required]
        [Range(1000, 50000)]
        public int Salary { get; set; }

        [Required]
        [RegularExpression("[0-9]{3,3}", ErrorMessage = "department id must be exactly 3-digits")]
        public int Deptid { get; set; }
    }
}
