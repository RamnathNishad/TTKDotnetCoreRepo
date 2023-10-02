using System.ComponentModel.DataAnnotations;

namespace Assignment2MVCLabRentCalculator.Models
{
    public class Hall
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string HallOwner { get; set; }

        [Required]
        public int CostPerDay { get; set; }

        [Required]
        [DataType("System.DateTime")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType("System.DateTime")]
        [DisplayFormat( ApplyFormatInEditMode =true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime EndDate { get; set; }
    }
}
