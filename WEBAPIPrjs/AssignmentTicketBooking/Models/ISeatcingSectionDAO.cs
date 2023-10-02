using System;

namespace AssignmentTicketBooking.Models
{
    public interface ISeatcingSectionDAO
    {
        List<SeatingSection> GetAllSeatingSection();
        Boolean BookTicket(string seatingType, int ticketCount);
    }
}
