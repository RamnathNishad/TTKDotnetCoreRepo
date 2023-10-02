using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentTicketBooking.Models
{
    [Table("Tickets")]
    public class SeatingSection
    {
        public int Id { get; set; }

        [Column("seating_type")]
        public string SeatingType { get; set; }
       
        public double Cost { get; set; }

        [Column("no_of_tickets")]
        public int  NoOfTickets { get; set; }
    }
}
