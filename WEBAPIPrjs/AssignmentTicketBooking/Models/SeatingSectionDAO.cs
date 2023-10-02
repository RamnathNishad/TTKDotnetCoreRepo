namespace AssignmentTicketBooking.Models
{
    public class SeatingSectionDAO : ISeatcingSectionDAO
    {
        private readonly TicketDbContext _ticketDbContext;
        public SeatingSectionDAO(TicketDbContext ticketDbContext)
        {
            _ticketDbContext = ticketDbContext;   
        }

        public bool BookTicket(string seatingType, int ticketCount)
        {
            //check the seats available for the seating type in database 
            //and then book accordingly
            var count = _ticketDbContext.Tickets
                                        .Where(o => o.Id.ToString() == seatingType && o.NoOfTickets >= ticketCount)
                                        .Count();

            if(count>0)
            {
                //book the ticket and return true
                //update the no. of ticket count
                var ticket = _ticketDbContext.Tickets
                                        .Where(o => o.Id.ToString() == seatingType && o.NoOfTickets >= ticketCount)
                                        .SingleOrDefault();

                ticket.NoOfTickets-= ticketCount;
                _ticketDbContext.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }

        }

        public List<SeatingSection> GetAllSeatingSection()
        {
            var lstSeatingSections=_ticketDbContext.Tickets.ToList();
            return lstSeatingSections;
        }
    }
}
