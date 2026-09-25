namespace GentlemansCode.Models;
public class BookingConfirmationViewModel
{
    public Booking Booking { get; set; } = null!;
    public string GoogleCalendarUrl { get; set; } = "";
    public string IcsUrl { get; set; } = "";
}
