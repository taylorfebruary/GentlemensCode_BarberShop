using System.ComponentModel.DataAnnotations;
namespace GentlemansCode.Models;
public class BookingViewModel
{
    [Required] public int ServiceId { get; set; }
    [Required] public int BarberId { get; set; }
    [Required, DataType(DataType.Date)] public DateTime AppointmentDate { get; set; } = DateTime.Today.AddDays(1);
    [Required] public string AppointmentTime { get; set; } = "09:00";
    [Required, StringLength(80)] public string CustomerName { get; set; } = "";
    [Required, EmailAddress, StringLength(160)] public string CustomerEmail { get; set; } = "";
    [Required, Phone, StringLength(30)] public string CustomerPhone { get; set; } = "";
    [StringLength(500)] public string? Notes { get; set; }
    public List<Service> Services { get; set; } = new();
    public List<Barber> Barbers { get; set; } = new();
}
