using System.ComponentModel.DataAnnotations;

namespace GentlemansCode.Models;

public class Booking
{
    public int BookingId { get; set; }

    [Required]
    public int ServiceId { get; set; }

    [Required]
    public int BarberId { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime AppointmentDate { get; set; }

    [Required]
    public TimeSpan AppointmentTime { get; set; }

    [Required, StringLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required, Phone]
    public string CustomerPhone { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Confirmed";

    public Service? Service { get; set; }
    public Barber? Barber { get; set; }
}
