using System.Security.Cryptography;

namespace Assignment3MVCLabHallBudget.Models
{
    public class Hall
    {
        public int id { get; set; }
        public string hallName { get; set; }
        public string ownerName { get; set; }
        public int costPerDay { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
    }
}
